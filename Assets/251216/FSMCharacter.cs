using System;
using unity_251211;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// 캐릭터(객체)에 FSM를 만들고, 애니메이션을 연결한다.
/// 속성: 상태, 애니메이터
/// 상태: 대기, 걷기, 뛰기, 공격, 클릭
/// </summary>
public class FSMCharacter : MonoBehaviour
{
    enum State { Idle, Walk, Run, Attack, Click }
    [SerializeField] State curState = State.Idle;
    Transform player;
    Animator controller;
    [SerializeField] private float moveRange = 5;
    [SerializeField] private float attackRange = 1;
    [SerializeField] private float speed = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMove>().transform;
        
        if (player == null)
        {
            print("플레이어가 Scene에 없습니다.");
        }

        controller = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        switch (curState)
        {
            case State.Idle:
                // 대기 애니메이션 실행
                CheckDistance();
                Idle();
                break;
            case State.Walk:
                CheckDistance();
                Move();
                break;
            case State.Attack:
                CheckDistance();
                Attack();
                break;
            case State.Click:
                Click();
                break;
        }
    }

    private void Click()
    {

    }

    private void Attack()
    {

    }

    private void Idle()
    {

    }

    /// <summary>
    /// 플레이어와의 거리를 확인하는 메서드
    /// </summary>
    private void CheckDistance()
    {
        float distance = (player.position - transform.position).magnitude;

        if (distance < moveRange && attackRange < distance)
        {
            print("현재: " + curState);

            curState = State.Walk;
            controller.SetTrigger("Walk");

            print("변경: " + curState);
        }
        else if (distance > moveRange)
        {
            print("현재: " + curState);
            curState = State.Idle;
            controller.SetTrigger("Idle");
            print("변경: " + curState);
        }
        else if(distance < attackRange)
        {
            print("현재: " + curState);
            curState = State.Attack;
            controller.SetTrigger("Attack");

            print("변경: " + curState);
        }
    }

    /// <summary>
    /// 플레이어가 5m 내에 있을 때 플레이어를 따라간다.
    /// </summary>
    private void Move()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        dir = new Vector3(dir.x, 0, dir.z); // y축 0으로
        transform.forward = dir; // 바라보는 방향은 플레이어 방향으로

        transform.position += dir * Time.deltaTime * speed;
    }
}
