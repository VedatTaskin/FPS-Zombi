using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
 

    [Header("Move Settings")]
    [SerializeField] float chaseRange = 7f;
    [SerializeField] float turnSpeed = 15f;
    [SerializeField] float patrolRadius = 7f;
    [SerializeField] float patrolWaitTime = 2f;
    [SerializeField] float chaseSpeed = 4f;
    [SerializeField] float searchSpeed = 3f;    
    [Header("Attack Settings")]
    [SerializeField] int damage = 2;
    [SerializeField] float attackRate = 2f;
    [SerializeField] float attackRange = 1f;

    private bool isSearching = false;
    private bool isAttacking = false;

    private Animator anim;
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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (player!=null)
        {
            CheckState();
            ExecuteState();
        }     
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        switch (currentState)
        {
            case State.Search:
                Gizmos.color = Color.blue;
                Vector3 targetPos = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);
                Gizmos.DrawLine(transform.position, targetPos);
                break;
            case State.Chase:
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, player.position);
                break;
            case State.Attack:
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, player.position);
                break;

        }
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
                //print("Idle");
                break;
            case State.Search:

                //agent'?n hedefe vard???n? ve kalan mesafenin 0.1'den k???k oldu?unu garanti alt?na almak i?in fazladan i?lem yap?ld?.
                if (!isSearching && agent.remainingDistance <= 0.1f || !agent.hasPath && !isSearching)
                {
                    Vector3 agentTarget = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);
                    agent.enabled = false;
                    transform.position = agentTarget;
                    agent.enabled = true;
                    Invoke("Search", patrolWaitTime);
                    anim.SetBool("Walk", false);
                    isSearching = true;
                }
                //print("Search");
                break;
            case State.Chase:
                //print("Chase");
                Chase();
                break;
            case State.Attack:
                //print("Attack");
                Attack();
                break;
        }

    }

    void Search()
    {
        agent.isStopped = false;
        agent.speed = searchSpeed;
        isSearching = false;
        anim.SetBool("Walk", true);
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
        anim.SetBool("Walk", true);
        agent.SetDestination(player.position);

    }

    void Attack()
    {
        if (player==null)
        {
            return;
        }
        if (!isAttacking)
        {
            StartCoroutine(AttackRoutine());
        }
        anim.SetBool("Walk", false);        
        agent.isStopped = true;  // takibi b?rakmak i?in
        agent.velocity = Vector3.zero; // h?z?n? tamamen kesmek i?in
        LookTheTarget(player.position);
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackRate);
        anim.SetTrigger("Attack");
        yield return new WaitUntil(() => IsAttackAnimationFinished("Attack"));
        isAttacking = false;
    }

    private bool IsAttackAnimationFinished(string animationName)
    {
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f)
        {
            return true;
        }
        else
        {
            return false;
        }
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


    public int GetDamage()
    {
        return damage;
    }

}
