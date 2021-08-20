using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;


    enum State
    {
        Idle,
        Search,
        Chase,
        Attack
    }

    [SerializeField] private State currentState = State.Idle;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float chaseRange = 7f;
    [SerializeField] float turnSpeed = 15f;
    [SerializeField] float patrolRadius = 7f;
    [SerializeField] float patrolWaitTime = 2f;
    [SerializeField] float chaseSpeed = 4f;
    [SerializeField] float searchSpeed = 3f;

    private bool isSearching = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        
    }

    void Update()
    {
        //agent.SetDestination(player.position);
        CheckState();
        ExecuteState();
    }

    private void CheckState()
    {
        float distanceToTarget = Vector3.Distance(transform.position, player.position);

        if (distanceToTarget<=chaseRange && distanceToTarget>attackRange)
        {
            currentState = State.Chase;
        }
        else if (distanceToTarget<=attackRange)
        {
            currentState = State.Attack;
        }
        else
        {
            currentState = State.Search;
        }

    }

    private void ExecuteState()
    {
        switch (currentState)
        {
            case State.Idle:
                print("Idle");
                break;
            case State.Search:

                //agent'ýn hedefe vardýðýný ve kalan mesafenin 0.1'den küçük olduðunu garanti altýna almak için fazladan iþlem yapýldý.
                if (!isSearching && agent.remainingDistance <= 0.1f || !agent.hasPath && !isSearching)
                {
                    Vector3 agentTarget = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);
                    agent.enabled = false;
                    transform.position = agentTarget;
                    agent.enabled = true;
                    Invoke("Search", patrolWaitTime);
                    isSearching = true;
                }
                print("Search");
                break;
            case State.Chase:
                print("Chase");
                Chase();
                break;
            case State.Attack:
                print("Attack");
                break;
        }

    }

    void Search()
    {
        agent.isStopped = false;
        agent.speed = searchSpeed;
        isSearching = false;
        agent.SetDestination(GetRandomPosition());
    }

    void Chase()
    {
        if (player == null)
        {
            return;
        }
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);

    }

    void Attack()
    {
        if (player==null)
        {
            return;
        }
        agent.isStopped = true;
        LookTheTarget(player.position);
    }

    void LookTheTarget(Vector3 target)
    {
        Vector3 lookPos = new Vector3(target.x, transform.position.y, target.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos - transform.position), turnSpeed*Time.deltaTime);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
        return hit.position;
    }

}
