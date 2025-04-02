using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamageable
{
    [SerializeField]float timeToAttack;
    [SerializeField]bool _canTakeDamage;
    int life;
    [Header("Projectile Arm")]
    [SerializeField] float timeToInstantiateArm;
    [SerializeField] GameObject ArmProjectile;
    [Space]
    [Header("OrbitAttack")]
    [SerializeField] GameObject orbitBalls;
    [SerializeField] float timeToStartOrbitAttack;
    [SerializeField] float timeToLeaveOrbitAttack;
    [SerializeField]  int orbitAttackAmount;
    [SerializeField] float timeForEachOrbit;
    [Space]
    [Header("Minions Instance Attacks")]
    [SerializeField] GameObject minionPrefab;
    [SerializeField] Transform instancePosition;
    [SerializeField] int minionAmount;
    [SerializeField] float delayForEachMinion;
    [SerializeField] int maxLife;

    [Header("Espiral Orbit Attack")]
    [SerializeField] GameObject espiralOrbitPrefab;





    Animator anim;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _canTakeDamage = true;

        life = maxLife;

        StartCoroutine(StartAttacking());
        
    }
    IEnumerator StartAttacking() 
    {
        yield return new WaitForSeconds(timeToAttack);
        StartCoroutine(ProjectileHandAttack());
    
    
    }
    IEnumerator ProjectileHandAttack() 
    {
        anim.Play("ProjectileHandAttack");
        yield return new WaitForSeconds(timeToInstantiateArm);
        Instantiate(ArmProjectile, transform.position, Quaternion.identity);
        
    }
     IEnumerator MinionsAttack() 
    {
        anim.Play("MinionInstanceAttack");
       
        for(int i = 0; i < minionAmount ; i++)
        {
             yield return new WaitForSeconds(delayForEachMinion + 0.1f);
           GameObject minionInstance = Instantiate(minionPrefab, instancePosition.position, Quaternion.identity);
           minionInstance.GetComponent<IMovable>().Move();
           anim.Play("MinionInstanceAttack");
          
        } anim.Play("Idle");
       
        

        
    }
     IEnumerator OrbitAttack() 
    {
        anim.Play("OrbitAttackEntry");
        yield return new WaitForSeconds(timeToStartOrbitAttack);
        _canTakeDamage = false;
        for(int i = 0; i < orbitAttackAmount ; i++)
        {
           Instantiate(orbitBalls, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeForEachOrbit);
        }
            
yield return new WaitForSeconds(timeToLeaveOrbitAttack);
           anim.Play("OrbitAttackLeave");
           yield return new WaitForSeconds(timeToStartOrbitAttack);
             _canTakeDamage = true;
           anim.Play("Idle");
        
    }
    IEnumerator EspiralOrbitAttack() 
    {
        anim.Play("EspiralAttack");
       GameObject espiralOrbitInstance = Instantiate(espiralOrbitPrefab, transform.position, transform.rotation);
        float duration = espiralOrbitInstance.GetComponent<BossSpiralOrbitAttack>().attackDuration;
    yield return new WaitForSeconds(duration);
    Destroy(espiralOrbitInstance);

           anim.Play("Idle");
        
    }
    public void TakeDamage(float damage)
    {
        if(!_canTakeDamage) return;
        StartCoroutine(ChangeColorWhenHit());
        life--;
        if(life <= 0)
        {
            StopAllCoroutines();
            anim.Play("Death");
            spriteRenderer.color = Color.white;

        }

    }
    IEnumerator ChangeColorWhenHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        
    }
}
