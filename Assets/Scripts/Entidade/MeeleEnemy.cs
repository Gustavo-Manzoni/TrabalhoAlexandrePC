using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MeeleEnemy : MonoBehaviour, IMovable, IDamageable, IKnockbackable
{
    [SerializeField] BaseEnemy baseEnemy;
    [SerializeField] GameObject hurtParticleSystem;
    [SerializeField] GameObject dieParticleSystem;

    DamageIndicatorPool damageIndicator;

    NavMeshAgent agent;
    Rigidbody2D rb;
    Transform target;
    Animator anim;
    SpriteRenderer spriteRenderer;

    float life;

    bool lookingRight;
    bool canTakeDamage;
    bool canTakeKnokback;
    void Start()
    {
        damageIndicator = FindObjectOfType<DamageIndicatorPool>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        StartCoroutine(ChangeColorWhenHit());
        StartCoroutine(ResetTakeDamageCd());

       
        GameObject obj = damageIndicator.GetFromPool();
        obj.transform.position = transform.position;
        obj.SetActive(true);
        obj.GetComponent<TMP_Text>().text = damage.ToString();

        Instantiate(hurtParticleSystem, transform.position, Quaternion.identity);

        life -= damage;

        if(life <= 0) 
        {
            Destroy(gameObject);
            Instantiate(dieParticleSystem, transform.position, Quaternion.identity);

        }



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
        canTakeDamage = false;
        yield return new WaitForSeconds(0.2f);
        canTakeDamage = true;
    
    }
    IEnumerator ResetTakeKnockbackCd()
    {
        canTakeKnokback = false;
        yield return new WaitForSeconds(0.2f);
        canTakeKnokback = true;

    }
    IEnumerator ChangeColorWhenHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;

    }

}
