using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeeleEnemy : MonoBehaviour, IMovable
{
    [SerializeField] BaseEnemy baseEnemy;
    Transform target;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    
    void Update()
    { 
        if(target == null) return;  
        agent.SetDestination(target.position);


    }
    public void Move()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
