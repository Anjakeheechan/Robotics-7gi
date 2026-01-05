using System;
using System.Collections;
using UnityEngine;

namespace unity_251212
{
    /// <summary>
    /// Vector를 더해서 이동하는 방식 vs Lerp 방식 비교
    /// </summary>
    public class MoveSequnce : MonoBehaviour
    {
        public Transform target;
        public Transform target1;
        public Transform target2;
        public Transform target3;

        Vector3 originPos;
        public float speed = 3;

        [Range(0f, 1f)] // Attribute: 속성을 창에 띄워주는 기능
        public float ratio;
        public float duration = 2; // 2초 동안 이동
        float elapsedTime;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            originPos = transform.position;

            StartCoroutine(SpawnRoutine()); // 시작시 3초 기다린 후 스폰하는 코루틴 함수 시작
        }

        // Update is called once per frame
        void Update()
        {
            // MoveByVectorAdding();

            // MoveByLerp();

            elapsedTime += Time.deltaTime;

            // 3초 후에 실린더 A작동
            if (elapsedTime > duration)
            {
                // 특정 시간이 되면 어떤 일한다.
                print("Update에서 스폰됨");
                Spawn();
            }

            // 5초 후에 실린더 B작동
            if (elapsedTime > duration)
            {
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(CoSeuquence());
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(FadeIn());
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                StartCoroutine(FadeOut());
            }
        }

        private void Spawn()
        {
            print("스폰됨");
        }

        // 코루틴 함수
        IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(duration);

            print("Coroutine에서 스폰됨");
            Spawn();

            yield return new WaitForSeconds(2);

            print("Coroutine에서 스폰됨");
            Spawn();
        }

        private void MoveByLerp()
        {
            // ratio -> 시간 / duration(2s)
            elapsedTime += Time.deltaTime;

            if (elapsedTime > duration)
                elapsedTime = 0;

            transform.position = Vector3.Lerp(originPos, target.position, elapsedTime / duration);
        }

        private void MoveByVectorAdding()
        {
            Vector3 dir = (target.position - transform.position).normalized;

            transform.position += dir * speed * Time.deltaTime;
        }

        // 버튼을 눌렀을 때 Player가 Target으로 duration동안 이동
        IEnumerator CoMove()
        {
            float elapsedTime = 0;

            while (true)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime > duration)
                {
                    break;
                }

                transform.position = Vector3.Lerp(originPos, target.position, elapsedTime / duration);

                yield return null; // 한 프레임 대기
            }

            yield return SpawnRoutine();
        }

        IEnumerator CoMove(Vector3 from, Vector3 to, float time)
        {
            float elapsedTime = 0;

            while (true)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime > time)
                {
                    break;
                }

                transform.position = Vector3.Lerp(from, to, elapsedTime / time);

                yield return null; // 한 프레임 대기
            }
        }

        IEnumerator CoSeuquence()
        {
            while (true)
            {
                yield return CoMove(originPos, target.position, 2);

                yield return CoMove(target.position, target1.position, 3);

                yield return CoMove(target1.position, target2.position, 0.5f);

                yield return CoMove(target2.position, target3.position, 1);

                yield return CoMove(target3.position, originPos, 1);
            }
        }

        // 내 게임오브젝트의 Renderer 속성을 가져와서 색상을 Fade In / Fade Out
        // Renderer 속성, Mathf.Lerp 
        IEnumerator FadeOut()
        {
            Renderer renderer = GetComponent<Renderer>();
            float elapsedTime = 0;
            float duration = 1;
            Color color = new Color(renderer.material.color.r,
                                    renderer.material.color.g,
                                    renderer.material.color.b,
                                    renderer.material.color.a);

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(1, 0, elapsedTime / duration);

                renderer.material.color = color;

                yield return null; // 한 프레임 대기
            }
        }

        IEnumerator FadeIn()
        {
            Renderer renderer = GetComponent<Renderer>();
            float elapsedTime = 0;
            float duration = 1;
            Color color = new Color(renderer.material.color.r,
                                    renderer.material.color.g,
                                    renderer.material.color.b,
                                    renderer.material.color.a);

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(0, 1, elapsedTime / duration);

                renderer.material.color = color;

                yield return null; // 한 프레임 대기
            }
        }
    }
}