using System;
using UnityEngine;

namespace unity_251211
{
    /// <summary>
    /// 사용자 입력을 받아, 캡슐을 이동시킨다.
    /// W: 플레이어가 앞쪽으로 이동
    /// A: 플레이어가 왼쪽으로 이동
    /// S: 플레이어가 뒤쪽으로 이동
    /// D: 플레이어가 오른쪽으로 이동
    /// 속성: 방향, 사용자 인풋
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        public Vector3 dir;
        public float speed = 0.5f;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //InifiniteMove();

            JoyStickMove();
        }

        /// <summary>
        /// 키보드의 방향키 입력으로 조이스틱의 입력을 모방하여 플레이어 이동
        /// (WASD, 방향키 사용)
        /// </summary>
        private void JoyStickMove()
        {
            // GetAxis: -1 ~ 1 사이의 값
            // Intput System -> Old input system -> New input system -> Active input handling
            float horizontalInput = Input.GetAxis("Horizontal"); // 왼쪽, 오른쪽, A, D
            float verticalInput = Input.GetAxis("Vertical");     // 위, 아래, W, S

            //print($"h: {horizontalInput}, v: {verticalInput}");

            // 기존: 절대좌표, 월드좌표 Vector3.forward
            // Vector3 dir = new Vector3(horizontalInput, 0, verticalInput);

            // 수정: 내 앞방향에 verticalInput을, 내 옆방향에 horizontalInput을 더하기
            Vector3 dir = (transform.right * horizontalInput) + (transform.forward * verticalInput);

            transform.position += dir * speed * Time.deltaTime;
            // P = P0 + vt
        }

        private void InifiniteMove()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                dir = Vector3.forward;
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                dir = Vector3.left;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                dir = Vector3.back;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                dir = Vector3.right;
            }

            transform.position += dir * speed;
        }
    }
}