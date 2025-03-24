using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterStatus, IDamageablePlayer, IKnockbackablePlayer
{
    [HideInInspector]public float horizontal, vertical;
    [SerializeField] float _cooldownToAttack;

    [HideInInspector]public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    PlayerAnimation _playerAnimation;

    [SerializeField] GameObject _attackHitbox;
    [SerializeField] GameObject _hurtParticleEffect;

    [HideInInspector]public float playerSpeed;
    

    bool _canMove;
    bool _canAttack;
    bool canTakeKnockback;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimation = GetComponent<PlayerAnimation>();

        playerSpeed = Speed;

        _canMove = true;
        _canAttack = true;
        canTakeKnockback = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Attack"))
         {
            if (_canAttack && InventoryManager.instance.playerWithSword)
            {
                Attack();
                return;
            }
            else if(!InventoryManager.instance.playerWithSword)
            {

                InventoryManager.Use();
            }

         }
        if(!_canMove) return;

         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Vertical");
         
        
    }
    private void FixedUpdate()
    {
         rb.velocity = new Vector2(horizontal, vertical).normalized * playerSpeed;


    }
    void Attack()
    {
        _playerAnimation.AttackAnimation();
        Instantiate(_attackHitbox, transform.position + 
            new Vector3(_playerAnimation.anim.GetFloat("LastHorizontal"), _playerAnimation.anim.GetFloat("LastVertical")) * 1.3f, transform.rotation);
        StartCoroutine(ResetAttackCooldown());
        
    }   
    public void TakeDamage(float damage)
    {
        print("aa");
        StartCoroutine(ChangeColorWhenHit());
        Instantiate(_hurtParticleEffect, transform.position, transform.rotation);
    }
    public IEnumerator TakeKnockback(float knockbackDuration, float knockbackForce, Vector2 direction)
    {
        if (rb == null) yield break;
        if (!canTakeKnockback) yield break;
        canTakeKnockback = false;
        _canMove = false;
       


        StartCoroutine(ResetTakeKnockbackCd());

       
        float elapsed = 0;
        while (elapsed < knockbackDuration)
        {

            rb.AddForce(direction * knockbackForce);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _canMove = true ;
      
        rb.velocity = Vector2.zero;
       
    }
    #region Coroutines
    IEnumerator ResetAttackCooldown()
    {
        _canAttack = false;
        _canMove = false;

        horizontal = 0;
        vertical = 0;

        yield return new WaitForSeconds(_cooldownToAttack);
        _canMove = true;
        _canAttack = true;
        
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
    #endregion
}
