using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeeleEnemy : MonoBehaviour, IMovable, IDamageable, IKnockbackable
{
    [SerializeField] BaseEnemy baseEnemy;
    Transform target;
    NavMeshAgent agent;
    Animator anim;
    bool lookingRight;
    float life;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseEnemy.speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        life = baseEnemy.maxLife;

        lookingRight = true;
    }
    public void TakeDamage(float damage) 
    {
    
    }
    public IEnumerator TakeKnockback(float knockbackDuration, float knockbackForce, Vector2 direction) 
    {
        float elapsed = 0;
        while(elapsed < knockbackDuration)
             {

               rb.AddForce(direction * knockbackForce);
              elapsed += Time.deltaTime;
                yield return null;
            }
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
