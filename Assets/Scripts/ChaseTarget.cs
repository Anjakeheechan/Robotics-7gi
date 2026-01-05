using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 버튼을 누르면 Agent가 네비게이션매시의 특정 위치로 이동한다.
/// 속성: 타겟 transform, NavMeshAgent
/// </summary>
public class ChaseTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            agent.SetDestination(target.position);
        }
    }
}
