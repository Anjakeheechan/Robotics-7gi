using UnityEngine;

namespace unity_251212
{
    public class SlerpRotation : MonoBehaviour
    {
        public float startAngle;
        public float endAngle;
        public float duration = 2;
        Quaternion startQ, endQ;
        float time;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // x, y, z축 중 회전할 축과 각도를 설정
            // 앞 방향 기준의 초기 회전각도
            startQ = Quaternion.AngleAxis(startAngle, transform.forward);
            endQ = Quaternion.AngleAxis(endAngle, transform.forward);
        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;

            if (time > duration)
                time = 0;

            Quaternion q = Quaternion.Slerp(startQ, endQ, time / duration);

            transform.rotation = q;
        }
    }
}