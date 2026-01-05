using System.Collections.Generic;
using UnityEngine;

namespace unity_251211
{
    /// <summary>
    /// 사용자가 Fire 버튼(Mouse 0)을 누르면 총알을 생성한다. -> 발사
    /// 속성: 총알, 총구
    /// </summary>
    public class Gun : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform firePos;
        float elapsedTime = 0;
        public List<GameObject> bullets = new List<GameObject> ();
        public int maxBulletNum = 10;

        private void Start()
        {
            for(int i = 0; i < maxBulletNum; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.SetParent(transform);

                bullet.SetActive(false);    // Bullet Off
                bullet.transform.position = firePos.position;

                bullets.Add(bullet);
            }
        }

        int btnClkCnt = 0;
        // Update is called once per frame
        void Update()
        {
            // 0(왼쪽), 1(마우스휠클릭), 2(오른쪽)
            if (Input.GetMouseButtonDown(0))
            {
                if(btnClkCnt ==  maxBulletNum)
                {
                    btnClkCnt = 0;
                }

                // 총알을 생성한다.
                //GameObject bullet = Instantiate(bulletPrefab);
                //bullet.transform.position = firePos.position;

                bullets[btnClkCnt].transform.SetParent(null);       // 부모로 부터 해방
                bullets[btnClkCnt].transform.position = firePos.position;  // 포지션 재지정

                bullets[btnClkCnt].SetActive(true);
                bullets[btnClkCnt].GetComponent<Bullet>().dir = firePos.forward;

                btnClkCnt++;
            }

            // Reload(장전) 날아갔던 총알을 초기화
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                print("Reload");

                for(int i = 0; i < maxBulletNum; i++)
                {
                    bullets[btnClkCnt].transform.SetParent(transform);  // 다시 부모를 지정
                    bullets[i].SetActive(false);
                    bullets[i].transform.localPosition = Vector3.zero;
                }
            }

        }
    }
}