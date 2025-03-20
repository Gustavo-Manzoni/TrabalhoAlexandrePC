using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    DamageIndicatorPool damageIndicator;
    bool canTakeDamage;
    bool canTakeKnokback;
    void Start()
    {
        damageIndicator = FindObjectOfType<DamageIndicatorPool>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseEnemy.speed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        life = baseEnemy.maxLife;

        lookingRight = true;
        canTakeDamage = true;
        canTakeKnokback = true;
    }
    public void TakeDamage(float damage) 
    {
        if (!canTakeDamage) return;
        StartCoroutine(ResetTakeDamageCd());
        canTakeDamage = false;
        GameObject obj = damageIndicator.GetFromPool();
        obj.transform.position = transform.position;

        obj.SetActive(true);
        obj.GetComponent<TMP_Text>().text = damage.ToString();


    }
    public IEnumerator TakeKnockback(float knockbackDuration, float knockbackForce, Vector2 direction) 
    {
        if (!canTakeKnokback) yield break;
        canTakeKnokback = false;
        StartCoroutine (ResetTakeKnockbackCd());
        Transform realTarget = target;
        target = null;
        float elapsed = 0;
        while(elapsed < knockbackDuration)
             {

               rb.AddForce(direction * knockbackForce);
              elapsed += Time.deltaTime;
                yield return null;
            }
      
        rb.velocity = Vector2.zero;
        target = realTarget;
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
    IEnumerator ResetTakeDamageCd() 
    {
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
    
    }
    IEnumerator ResetTakeKnockbackCd()
    {
        yield return new WaitForSeconds(0.2f);
        canTakeKnokback = true;

    }

}
