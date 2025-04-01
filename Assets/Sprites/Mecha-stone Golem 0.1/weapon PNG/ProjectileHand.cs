using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileHand : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

        agent.SetDestination(target.position);
      
        Vector3 direction = target.position - transform.position;
        direction.z = 0; 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, angle), Time.deltaTime * 5);
            
        
        
      

    }
}
