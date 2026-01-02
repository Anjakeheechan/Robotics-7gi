using Firebase;
using Firebase.Database;
using Firebase.Extensions; // ContinueWithOnMainThread 사용 권장
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FirebaseEx
{
    [System.Serializable]
    public class PlayerState
    {
        public float x;
        public float y;
        public float z;

        public PlayerState(Vector3 pos)
        {
            this.x = pos.x;
            this.y = pos.y;
            this.z = pos.z;
        }
    }

    public class FirebaseDBManager : MonoBehaviour
    {
        [Header("Firebase Settings")]
        [SerializeField] string dbUrl;
        DatabaseReference dbRef;
        bool isFirebaseReady = false;

        [Header("UI & Object Settings")]
        [SerializeField] TMP_InputField idInput;
        [SerializeField] GameObject playerPrefab;

        [Header("Runtime Variables")]
        [SerializeField] string myId;
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] bool isConnected = false;

        private GameObject myPlayerObj;
        private Dictionary<string, GameObject> otherPlayers = new Dictionary<string, GameObject>();

        // [변경점 1] 백그라운드 스레드에서는 JSON 문자열만 저장 (파싱 X)
        // Key: PlayerID, Value: JSON String
        private Dictionary<string, string> pendingJsonData = new Dictionary<string, string>();
        private object dataLock = new object();

        void Start()
        {
            // [변경점 2] Firebase 의존성 확인 (크래시 방지 필수 단계)
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    InitializeFirebase();
                }
                else
                {
                    Debug.LogError("Firebase 의존성 오류: " + dependencyStatus);
                }
            });
        }

        void InitializeFirebase()
        {
            FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri(dbUrl);
            dbRef = FirebaseDatabase.DefaultInstance.RootReference;
            isFirebaseReady = true;
            Debug.Log("Firebase 초기화 완료");
        }

        public void OnStartBtnClkEvent()
        {
            if (!isFirebaseReady)
            {
                Debug.LogError("Firebase가 아직 준비되지 않았습니다.");
                return;
            }

            if (string.IsNullOrEmpty(idInput.text))
            {
                Debug.LogError("ID를 입력하세요.");
                return;
            }

            myId = idInput.text.Trim();
            isConnected = true;

            SpawnLocalPlayer();

            // 리스너 등록
            dbRef.ValueChanged += HandleValueChanged;
        }

        void Update()
        {
            if (!isConnected) return;

            HandleLocalPlayerMovement();

            // 메인 스레드에서 데이터 처리
            ProcessPendingData();
        }

        void SpawnLocalPlayer()
        {
            myPlayerObj = Instantiate(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            myPlayerObj.name = myId;
            // 로컬 플레이어는 초록색
            var rend = myPlayerObj.GetComponent<Renderer>();
            if (rend) rend.material.color = Color.green;
        }

        void HandleLocalPlayerMovement()
        {
            if (myPlayerObj == null) return;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (h != 0 || v != 0)
            {
                Vector3 moveDir = new Vector3(h, 0, v).normalized;
                myPlayerObj.transform.Translate(moveDir * moveSpeed * Time.deltaTime);

                UpdateMyPosToDB(myPlayerObj.transform.position);
            }
        }

        void UpdateMyPosToDB(Vector3 pos)
        {
            PlayerState state = new PlayerState(pos);
            string json = JsonUtility.ToJson(state);
            // 내 데이터 전송
            dbRef.Child(myId).SetRawJsonValueAsync(json);
        }

        /// <summary>
        /// 백그라운드 스레드에서 실행됨.
        /// 절대 여기서 Unity API(Instantiate, JsonUtility 등)를 호출하면 안 됨.
        /// 문자열 데이터만 받아두는 역할.
        /// </summary>
        void HandleValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null) return;

            DataSnapshot snapshot = args.Snapshot;

            lock (dataLock)
            {
                foreach (DataSnapshot child in snapshot.Children)
                {
                    string playerId = child.Key;

                    // 내 아이디는 무시
                    if (playerId == myId) continue;

                    string json = child.GetRawJsonValue();

                    // 유효성 검사
                    if (string.IsNullOrEmpty(json) || !json.Trim().StartsWith("{")) continue;

                    // 딕셔너리에 JSON 문자열 갱신 (파싱은 나중에)
                    if (pendingJsonData.ContainsKey(playerId))
                        pendingJsonData[playerId] = json;
                    else
                        pendingJsonData.Add(playerId, json);
                }
            }
        }

        /// <summary>
        /// 메인 스레드(Update)에서 실행됨.
        /// 여기서 JSON 파싱과 오브젝트 생성/이동을 수행함 -> 크래시 해결
        /// </summary>
        void ProcessPendingData()
        {
            lock (dataLock)
            {
                // 처리할 데이터가 없으면 리턴
                if (pendingJsonData.Count == 0) return;

                foreach (var kvp in pendingJsonData)
                {
                    string playerId = kvp.Key;
                    string json = kvp.Value;

                    try
                    {
                        // 1. JSON 파싱 (메인 스레드이므로 안전)
                        PlayerState state = JsonUtility.FromJson<PlayerState>(json);
                        Vector3 targetPos = new Vector3(state.x, state.y, state.z);

                        // 2. 플레이어 존재 여부 확인 및 생성
                        if (!otherPlayers.ContainsKey(playerId))
                        {
                            GameObject newObj = Instantiate(playerPrefab);
                            newObj.name = playerId;

                            // 다른 플레이어는 빨간색
                            var rend = newObj.GetComponent<Renderer>();
                            if (rend) rend.material.color = Color.red;

                            otherPlayers.Add(playerId, newObj);
                            Debug.Log($"[접속] {playerId}");
                        }

                        // 3. 위치 이동
                        GameObject obj = otherPlayers[playerId];
                        // 부드럽게 이동 (Lerp)
                        obj.transform.position = Vector3.Lerp(obj.transform.position, targetPos, Time.deltaTime * 10f);
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogWarning($"데이터 처리 중 오류 ({playerId}): {e.Message}");
                    }
                }
                // 데이터를 매 프레임 다 비울 필요는 없지만, 
                // 위치 보간을 위해 최신 데이터가 유지되어야 하므로 Clear하지 않고 덮어쓰기 방식으로 유지
            }
        }

        private void OnDestroy()
        {
            if (dbRef != null)
            {
                dbRef.ValueChanged -= HandleValueChanged;
            }
        }
    }
}