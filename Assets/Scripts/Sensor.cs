using UnityEngine;

namespace unity_251211
{
    /// <summary>
    /// Sensing Area에 들어오는 물체를 정보를 트리거 한다.
    /// </summary>
    public class Sensor : MonoBehaviour
    {
        /// <summary>
        /// Sensing Area에 들어오는 물체를 정보를 트리거 한다.
        /// 나 자신은 충돌체(Collider)만 있으면 됨
        /// </summary>
        /// <param name="other">접촉 물체의 콜라이더 정보</param>
        private void OnTriggerEnter(Collider other)
        {
            print("감지 시작: " + other.name);
        }

        private void OnTriggerStay(Collider other)
        {
            print("감지중: " + other.name);
        }

        private void OnTriggerExit(Collider other)
        {
            print("감지 종료: " + other.name);
        }
    }
}
