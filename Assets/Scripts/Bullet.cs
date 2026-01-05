using UnityEngine;

namespace unity_251211
{
    /// <summary>
    /// 끝없이 내 앞쪽 방향으로 특정 속도로 날아간다.
    /// 속성: 방향, 속도
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        public Vector3 dir;
        public float speed = 10;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            // dir = transform.forward;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += dir * speed * Time.deltaTime;
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    print(other.name);
        //    // Obstacle tag가 있는 물체와 부딪히면 나 자신을 파괴
        //    if(other.gameObject.tag == "Obstacle")
        //    {
        //        Destroy(this.gameObject);
        //    }
        //}
    }
}