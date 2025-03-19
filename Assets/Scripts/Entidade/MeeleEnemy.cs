using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeeleEnemy : MonoBehaviour, IMovable
{
    [SerializeField] BaseEnemy baseEnemy;
    NavMeshAgent agent;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void Move()
    {
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}
