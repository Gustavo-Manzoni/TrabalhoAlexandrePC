using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterStatus
{
    [HideInInspector]public float horizontal, vertical;
    [SerializeField] float _cooldownToAttack;

    [HideInInspector]public Rigidbody2D rb;
    PlayerAnimation _playerAnimation;

    [SerializeField] GameObject _attackHitbox;

    bool _canMove;
    bool _canAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();

        _canMove = true;
        _canAttack = true;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("Attack") && _canAttack)
         {
            Attack();

         }
        if(!_canMove) return;

         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Vertical");
         
        
    }
    private void FixedUpdate()
    {
         rb.velocity = new Vector2(horizontal, vertical).normalized * Speed;


    }
    void Attack()
    {
        _playerAnimation.AttackAnimation();
        Instantiate(_attackHitbox, transform.position + 
            new Vector3(_playerAnimation.anim.GetFloat("LastHorizontal"), _playerAnimation.anim.GetFloat("LastVertical")) * 1.3f, transform.rotation);
        StartCoroutine(ResetAttackCooldown());
        
    }   
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

}
