using UnityEngine;

namespace unity_251211
{
    /// <summary>
    /// Rigidbody의 Physics 속성을 이용하여 물체에 힘을 주고 싶다.
    /// 속성: rigidbody, 방향, forceMode
    /// </summary>
    public class PhysicsStudy : MonoBehaviour
    {
        public Rigidbody rb;
        public Transform target;
        public float power;
        Vector3 dir;
        private Vector3 originPos;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            originPos = transform.position; // 처음 위치 저장
        }

        // 스페이스 버튼을 누르면, 나에게 Target 방향으로 힘을 준다.
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dir = target.position - transform.position; // 크기와 방향이 있는 벡터
                dir = dir.normalized * power;  // 크기가 1인 벡터 * power

                rb.AddForce(dir, ForceMode.Impulse);
            }

            // 공 위치, 에너지 초기화
            if (Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.Space))
            {
                rb.angularVelocity = Vector3.zero; // 각속도(회전) 초기화
                rb.linearVelocity = Vector3.zero;  // 속도 초기화

                transform.position = originPos;
            }
        }

        /// <summary>
        /// Collider와 Rigidbody 필수 -> 다른 콜라이더가 있는 물체와 접촉확인
        /// </summary>
        /// <param name="collision">충돌된 물체의 정보</param>
        private void OnCollisionEnter(Collision collision)
        {
            print("충돌시작: " + collision.collider.name);
            print(collision.contacts[0].point);
        }

        private void OnCollisionStay(Collision collision)
        {
            print("충돌중: " + collision.collider.name);
        }

        private void OnCollisionExit(Collision collision)
        {
            print("충돌종료: " + collision.collider.name);
        }
    }
}