using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterStatus, IDamageablePlayer, IKnockbackablePlayer
{
    /*
    Professor eu vou escrever tudo aqui mesmo, o meu planejamento era ter feito um jogo maior
     mas acabou que mesmo trabalhando em casa saiu isso mesmo e ficou bem ruinzinho, mas eu fiz o melhor que pude
    eu nao consegui fazer o uso otimizado de varios recursos por causa do tempo
     eu queria usar uma maquina de estado para fazer o boss mas acabou que nao deu tempo
    */
    [HideInInspector]public float horizontal, vertical;
    [SerializeField] float _cooldownToAttack;

    [HideInInspector]public Rigidbody2D rb;
    SpriteRenderer _spriteRenderer;
    PlayerAnimation _playerAnimation;
    

    [SerializeField] GameObject _attackHitbox;
    [SerializeField] GameObject _hurtParticleEffect;

    [HideInInspector]public float playerSpeed;
    [HideInInspector]public float life;
    

    bool _canMove;
    bool _canAttack;
    bool _canTakeKnockback;
    bool _canTakeDamage;
    [SerializeField]bool _bossBattle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        life = base.LifeMax;
        playerSpeed = Speed;

        _canMove = true;
        _canAttack = true;
        _canTakeKnockback = true;
        _canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {

    if(_canMove) {

         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Vertical");
        }


if(_bossBattle )return;
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
        
         
        
    }
    private void FixedUpdate()
    {
         rb.velocity = new Vector2(horizontal, vertical).normalized * playerSpeed;


    }
    void Attack()
    {
        _playerAnimation.AttackAnimation();
        Instantiate(_attackHitbox, transform.position +
         new Vector3(_playerAnimation.anim.GetFloat("LastHorizontal"), 
         _playerAnimation.anim.GetFloat("LastVertical")) * 1.3f, transform.rotation);
        StartCoroutine(ResetAttackCooldown());
        
    }   
    public void TakeDamage(float damage)
    {
        if(!_canTakeDamage) return;
        StartCoroutine(ResetTakeDamageCd());
        StartCoroutine(ChangeColorWhenHit());
        Instantiate(_hurtParticleEffect, transform.position, transform.rotation);
        life -= damage;
        for(int i = 0; i < damage; i ++)
        {
                GameManager.instance.LessHeart();
            
        }

        if(life <= 0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        }
        
    }
    public IEnumerator TakeKnockback(float knockbackDuration, float knockbackForce, Vector2 direction)
    {
        if (rb == null) yield break;
        if (!_canTakeKnockback) yield break;
        _canTakeKnockback = false;
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
    public Vector3 GetHorVer()
    {

        return new Vector3(horizontal,vertical);

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
        _canTakeKnockback = false;
        yield return new WaitForSeconds(0.2f);
        _canTakeKnockback = true;

    }
    IEnumerator ResetTakeDamageCd()
    {
        _canTakeDamage = false;
        yield return new WaitForSeconds(0.2f);
        _canTakeDamage = true;

    }
    IEnumerator ChangeColorWhenHit()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = Color.white;

    }
    #endregion
}
