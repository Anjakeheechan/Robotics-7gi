using Firebase.Auth;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

/// <summary>
/// Firebase Auth를 사용하여 이메일 회원가입, 인증(verification), 로그인을 한다.
/// 속성: 회원가입패널(ui들), 로그인패널(ui들), 인증확인패널
/// </summary>
public class FirebaseAuthManager : MonoBehaviour
{
    [Header("회원가입패널")]
    public GameObject signupPanel;
    public TMP_InputField nameInput;
    public TMP_InputField emailInput;
    public TMP_InputField pWInput;
    public TMP_InputField pWCheckInput;

    [Header("로그인패널")]
    public GameObject loginPanel;
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPWInput;

    [Header("인증팝업패널")]
    public GameObject popupPanel;
    public TMP_Text popupText;

    FirebaseAuth auth; // Firebase Authentication의 인스턴스에 접근, 기능이 포함되어있는 클래스
    FirebaseUser user; // 현재 로그인한 유저 정보를 담는 클래스

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += OnAuthStateChanged; // 이벤트 핸들러
    }

    // 로그인 상태 변경감지 매서드
    void OnAuthStateChanged(object sender, System.EventArgs e)
    {
        if (auth.CurrentUser != user)
        {
            bool isSignedIn = (user != auth.CurrentUser) && (auth.CurrentUser != null);

            user = auth.CurrentUser;

            if (isSignedIn)
            {
                Debug.Log($"로그인 되었습니다.: {user.UserId}");
            }
            else if(!isSignedIn && user != null)
            {
                Debug.Log($"로그아웃 되었습니다.: {user.UserId}");
            }
        }
    }

    // 회원가입 패널에서 회원가입 버튼 클릭 비동기 이벤트
    public async void OnSignUpBtnClkEvent()
    {
        string name = nameInput.text;
        string email = emailInput.text;
        string pw = pWInput.text;
        string pwCheck = pWCheckInput.text;

        if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(pw) || string.IsNullOrEmpty(pwCheck)) 
        {
            Debug.LogWarning("입력창이 비어있는지 확인해 주세요.");
            return;
        }

        if(pw != pwCheck)
        {
            Debug.LogWarning("비밀번호가 일치하지 않습니다.");
            return;
        }

        // 계정생성
        await auth.CreateUserWithEmailAndPasswordAsync(email, pw).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogWarning(task.Exception);
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.LogWarning(task.Exception);
                return;
            }
            else if(task.IsCompleted)
            {
                user = task.Result.User;

                Debug.Log($"회원가입 성공: {user.Email}");
            }
        });

        SendVerificationEmailAsync(user);

        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
    }

    public async void SendVerificationEmailAsync(FirebaseUser user)
    {
        await user.SendEmailVerificationAsync().ContinueWith(task =>
        {
            if(task.IsCanceled)
            {
                Debug.LogWarning(task.Exception);
                return;
            }
            else if(task.IsFaulted)
            {
                Debug.LogWarning(task.Exception);
                return;
            }
            else if(task.IsCompleted)
            {
                Debug.Log($"{user.Email}에서 인증 확인을 눌러주세요.");
            }
        });

        StartCoroutine(CoTurnOnPopupPanel($"Check {user.Email} and click the verification link."));
    }

    IEnumerator CoTurnOnPopupPanel(string msg)
    {
        popupPanel.SetActive(true);
        popupText.text = msg;

        yield return new WaitForSeconds(3);

        popupPanel.SetActive(false);
    }

    // 로그인패널 -> 회원가입 패널
    public void OnSignUpMBtnClkEvent()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
    }

    // 회원가입패널 -> 로그인패널
    public void OnCancelBtnClkEvent()
    {
        loginPanel.SetActive(true);
        signupPanel.SetActive(false);
    }

    // 종료 버튼
    public void OnExitBtnClkEvent()
    {
        Application.Quit();
    }
}
