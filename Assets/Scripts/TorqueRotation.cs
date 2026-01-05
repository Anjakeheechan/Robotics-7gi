using UnityEngine;

namespace unity_251212
{
    /// <summary>
    /// 버튼을 눌러서 rigidbody에 토크를 전달한다.
    /// 토크: 물체를 회전시키는 정도를 나타내는 물리량, 회전력
    /// 힘에 회전축에서 힘이 작용하는 지점까지의 거리를 곱한 값, 단위(NM)
    /// </summary>
    public class TorqueRotation : MonoBehaviour
    {
        public float torqueAmount = 1;
        Rigidbody body;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            body = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKey(KeyCode.A))
            {
                body.AddTorque(transform.forward *  torqueAmount);
            }

            if (Input.GetKey(KeyCode.D))
            {
                body.AddTorque(transform.forward * -torqueAmount);
            }
        }
    }

}