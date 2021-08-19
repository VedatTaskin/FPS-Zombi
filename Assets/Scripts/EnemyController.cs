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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        
    }

    void Update()
    {
        //agent.SetDestination(player.position);
        ExecuteState();
    }

    private void ExecuteState()
    {
        switch (currentState)
        {
            case State.Idle:
                print("Idle");
                break;
            case State.Search:
                print("Search");
                break;
            case State.Chase:
                print("Chase");
                break;
            case State.Attack:
                print("Attack");
                break;
        }

    }
}
