using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class LongDistanceEnemy : MonoBehaviour, IDamageable, IKnockbackable, IMovable
{
    Transform player;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject particleEffectWhenDie;
    public GameObject arrowPrefab;
    public Transform arrowSpawn;

    [SerializeField]BaseEnemy baseEnemy;
    DamageIndicatorPool damageIndicator;

    float life;
    float speed;

    [SerializeField] float fleeDistance = 2f;
    [SerializeField] float stopDistance = 6f;
    [SerializeField] float arrowSpeed = 5f;
    [SerializeField] float shootCooldown = 2f;

    bool canShoot;
    bool canMove;
    bool lookingRight;
    void Start()
    {
        life = baseEnemy.maxLife;
        speed = baseEnemy.speed;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent <SpriteRenderer>();

        damageIndicator = FindObjectOfType<DamageIndicatorPool>();

        canShoot = true;
        lookingRight = true;

        arrowSpawn = transform;
    }
    public void Move() 
    {
         canMove = true;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            HandleMovement();
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
    IEnumerator ChangeColor()
    {
   
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;



    }
    
    
    void HandleMovement()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= fleeDistance)
        {
            FleeFromPlayer();
            
        }
        else if (distanceToPlayer <= stopDistance)
        {
            StopAndAttack();
           
        }
        else
        {
            MoveTowardsPlayer();
           
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        UpdateAnimator(direction);
    }

    void StopAndAttack()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("Running", false);

        if (canShoot)
        {
            StartCoroutine(ShootArrow());
          
        }
    }

    void FleeFromPlayer()
    {
        Vector3 direction = (transform.position - player.position).normalized;
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        UpdateAnimator(direction);
    }

    IEnumerator ShootArrow()
    {
        canShoot = false;

        yield return new WaitForSeconds(0.5f);

        GameObject arrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);
        arrowRb.velocity = direction * arrowSpeed;

        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    void UpdateAnimator(Vector3 direction)
    {
        float horizontal = Mathf.Round(direction.x);
        float vertical = Mathf.Round(direction.y);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("Running", horizontal != 0 || vertical != 0);
        if (lookingRight && horizontal < 0 || !lookingRight && horizontal > 0)
        {
            lookingRight = !lookingRight;
            Vector2 newScale = (Vector2)transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;



        }
    }
    public void TakeDamage(float damage) 
    {
        life -= damage;
        StartCoroutine(ChangeColor());
        

        if (life <= 0)
        {
            Instantiate(particleEffectWhenDie, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public IEnumerator TakeKnockback(float knockbackDuration, float knockbackForce, Vector2 direction)
    {
        if (rb == null) yield break;
        //if (!canTakeKnockback) yield break;
        //canTakeKnockback = false;

        //StartCoroutine(ResetTakeKnockbackCd());
        float elapsed = 0;
        while (elapsed < knockbackDuration)
        {

            rb.AddForce(direction * knockbackForce);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
       
    }

}
