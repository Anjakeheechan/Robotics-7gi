using System;
using UnityEngine;

namespace unity_251211
{
    /// <summary>
    /// 마우스 입력을 받아서 고개를 위, 아래로 돌린다.
    /// 속성: 회전속도
    /// 
    /// 마우스 X 입력으로 몸통을 좌, 우로 돌린다.
    /// 속성: 플레이어 몸통 게임오브젝트(transform)
    /// </summary>
    public class CameraLook : MonoBehaviour
    {
        public float rotSpeed;
        private float xRot;
        public Transform player;
        private float yRot;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            player = transform.parent;
        }

        // Update is called once per frame
        void Update()
        {
            TiltCamera();

            RotateBody();
        }

        /// <summary>
        /// Mouse X 입력을 받아, Player의 Y축을 회전시킨다.
        /// </summary>
        private void RotateBody()
        {
            float mouseX = Input.GetAxis("Mouse X");
            yRot = mouseX * rotSpeed * Time.deltaTime;

            player.localRotation = player.localRotation * Quaternion.Euler(0, yRot, 0);
        }

        /// <summary>
        /// Mouse Y 입력을 받아, 카메라의 X축을 회전시킨다.
        /// </summary>
        private void TiltCamera()
        {
            // -1 ~ 1
            float mouseY = Input.GetAxis("Mouse Y");
            xRot += (-mouseY * rotSpeed * Time.deltaTime);
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRot, 0, 0);
        }
    }
}