using UnityEngine;

namespace unity_251212
{
    /// <summary>
    /// 시계바늘을 Lerp 기능을 사용하여 회전시킨다.
    /// 속성: 시작 각도, 끝 각도, 시간
    /// </summary>
    public class LerpRotation : MonoBehaviour
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

            Quaternion q = Quaternion.Lerp(startQ, endQ, time / duration);

            transform.rotation = q;
        }
    }

}