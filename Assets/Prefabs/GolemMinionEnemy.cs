using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GolemMinionEnemy : MonoBehaviour, IMovable, IDamageable, IKnockbackable
{
    [SerializeField] BaseEnemy baseEnemy;
    [SerializeField] GameObject hurtParticleSystem;
    [SerializeField] GameObject dieParticleSystem;

    DamageIndicatorPool damageIndicator;

    Rigidbody2D rb;
    Transform target;
    Animator anim;
    SpriteRenderer spriteRenderer;

    float life;

    bool lookingRight;
    bool canTakeDamage;
    bool canTakeKnockback;
    void Start()
    {
        damageIndicator = FindObjectOfType<DamageIndicatorPool>();

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        life = baseEnemy.maxLife;

        lookingRight = true;
        canTakeDamage = true;
        canTakeKnockback = true;
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

        if (life <= 0)
        {
            Destroy(gameObject);
            Instantiate(dieParticleSystem, transform.position, Quaternion.identity);

        }



    }
    public IEnumerator TakeKnockback(float knockbackDuration, float knockbackForce, Vector2 direction)
    {
        if (rb == null) yield break;
        if (!canTakeKnockback) yield break;
        canTakeKnockback = false;

        StartCoroutine(ResetTakeKnockbackCd());

        Transform realTarget = target;
        target = null;
        float elapsed = 0;
        while (elapsed < knockbackDuration)
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

        if (target == null) return;
        Vector3 MoveDirection = -(transform.position - target.position);
        rb.velocity = new Vector2(MoveDirection.x, MoveDirection.y).normalized * baseEnemy.speed;
        UpdateAnimator();

    }
    void UpdateAnimator()
    {
        Vector2 direction = -(transform.position - target.position).normalized;
        float horizontal = Mathf.Round(direction.x);
        float vertical = Mathf.Round(direction.y);
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        if (lookingRight && horizontal < 0 || !lookingRight && horizontal > 0)
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
        if (anim != null)
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
        canTakeKnockback = false;
        yield return new WaitForSeconds(0.2f);
        canTakeKnockback = true;

    }
    IEnumerator ChangeColorWhenHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;

    }
}
