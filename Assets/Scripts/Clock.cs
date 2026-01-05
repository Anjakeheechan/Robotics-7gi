using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

namespace unity_251212
{

    public class Clock : MonoBehaviour
    {
        public enum 옵션
        {
            시간24표현,
            시간12표현
        }
        public 옵션 option = 옵션.시간24표현;
        public Transform 시침, 분침, 초침;
        public int 시, 분, 초;
        public float timeScale = 1;
        public TMP_Text text;
        float elapsedTime = 0;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            시 = DateTime.Now.Hour;
            분 = DateTime.Now.Minute;
            초 = DateTime.Now.Second;

            현재시간표현(시, 분, 초);
        }

        private void 현재시간표현(int 시, int 분, int 초)
        {
            string new초, new분 = "";
            if (초 < 10)
                new초 = "0" + 초;
            else
                new초 = 초.ToString();

            if (분 < 10)
                new분 = "0" + 분;
            else
                new분 = 분.ToString();

            print($"현재시간: {시}:{new분}:{new초}");

            text.text = $"{시}:{new분}:{new초}";
            시계침회전(시, 분, 초);
        }

        private void 시계침회전(int 시, int 분, int 초)
        {
            int 시간각도 = 시 * 15; // 360 / 24 = 15
            int 분각도 = 분 * 6;    // 360 / 60 = 6
            int 초각도 = 초 * 6;

            시침.rotation = Quaternion.AngleAxis(시간각도, -transform.forward);
            분침.rotation = Quaternion.AngleAxis(분각도, -transform.forward);
            초침.rotation = Quaternion.AngleAxis(초각도, -transform.forward);
        }

        // Update is called once per frame
        void Update()
        {
            elapsedTime += Time.deltaTime * timeScale;

            if (elapsedTime >= 1)
            {
                초++;

                if (초 > 59)
                {
                    분++;
                    초 = 0;
                }

                if (분 > 59)
                {
                    시++;
                    분 = 0;
                }

                switch (option)
                {
                    case 옵션.시간24표현:
                        if (시 > 23)
                            시 = 분 = 초 = 0;
                        break;
                    case 옵션.시간12표현:
                        if (시 > 11)
                            시 = 분 = 초 = 0;
                        break;
                }

                현재시간표현(시, 분, 초);

                elapsedTime = 0;
            }
        }
    }

}