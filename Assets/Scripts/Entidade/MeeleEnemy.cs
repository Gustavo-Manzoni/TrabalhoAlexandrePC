using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeeleEnemy : MonoBehaviour, IMovable
{
    [SerializeField] BaseEnemy baseEnemy;
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    bool lookingRight;
    float life;
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseEnemy.speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        life = baseEnemy.maxLife;

        lookingRight = true;
    }

    
    void Update()
    { 
        if(target == null) return;  
        agent.SetDestination(target.position);
        UpdateAnimator();

    }
    void UpdateAnimator() 
    {
        Vector2 direction = -(transform.position - target.position).normalized;
        float horizontal = Mathf.Round(direction.x);
        float vertical = Mathf.Round(direction.y);
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        if(lookingRight && horizontal < 0 || !lookingRight && horizontal > 0) 
        {
            lookingRight = !lookingRight;
            Vector2 newScale = (Vector2)transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        
        
        
        }
    }
    public void Move()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim.SetBool("Running", true);
    }
}
