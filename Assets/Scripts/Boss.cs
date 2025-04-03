using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BossAttacks
{
    ProjectileHand,
    MinionsInstance,
    NormalOrbit,
    SpiralOrbit
}
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
    float waitDuration;
    


    List<GameObject> attackObjects = new List<GameObject>();
    Animator anim;
    SpriteRenderer spriteRenderer;
    [SerializeField] AudioClip hit;
    AudioSource audioSource;
    [SerializeField] GameObject winMessage;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        _canTakeDamage = true;

        life = maxLife;

        StartCoroutine(StartAttacking());
        
    }
    IEnumerator StartAttacking() 
    {
        yield return new WaitForSeconds(timeToAttack);
       
        Array valores = Enum.GetValues(typeof(BossAttacks));
        System.Random random = new System.Random();
       BossAttacks attackType = (BossAttacks)valores.GetValue(random.Next(valores.Length));
        switch (attackType)
        {       
            case BossAttacks.ProjectileHand:
                StartCoroutine(ProjectileHandAttack());
                yield return new WaitForSeconds(timeToInstantiateArm);
                yield return new WaitForSeconds(0.5f);
                break;
            case BossAttacks.MinionsInstance:
                StartCoroutine(MinionsAttack());
                for (int i = 0; i < minionAmount; i++)
                {
                    yield return new WaitForSeconds(delayForEachMinion + 0.1f);
                }
                break;
            case BossAttacks.NormalOrbit:
                StartCoroutine(OrbitAttack());

              
                yield return new WaitForSeconds(timeToStartOrbitAttack);
                for (int i = 0; i < orbitAttackAmount; i++)
                {yield return new WaitForSeconds(timeForEachOrbit);}
                yield return new WaitForSeconds(timeToLeaveOrbitAttack);
                yield return new WaitForSeconds(timeToStartOrbitAttack);
                break;
            case BossAttacks.SpiralOrbit:
                StartCoroutine(EspiralOrbitAttack());
              
                yield return new WaitForSeconds (waitDuration);
                break;
        
        }
        anim.Play("Idle");
        StartCoroutine(StartAttacking());   
    }
    IEnumerator ProjectileHandAttack() 
    {
        anim.Play("ProjectileHandAttack");
        yield return new WaitForSeconds(timeToInstantiateArm);
        GameObject attackPrefabInstance = Instantiate(ArmProjectile, transform.position, Quaternion.identity);
        attackObjects.Add(attackPrefabInstance);    
        yield return new WaitForSeconds(0.5f);
        anim.Play("Idle");
        
    }
     IEnumerator MinionsAttack() 
    {
        anim.Play("MinionInstanceAttack");
       
        for(int i = 0; i < minionAmount ; i++)
        {
             yield return new WaitForSeconds(delayForEachMinion + 0.1f);
           GameObject minionInstance = Instantiate(minionPrefab, instancePosition.position, Quaternion.identity);
            attackObjects.Add(minionInstance);
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
           GameObject orbitInstance = Instantiate(orbitBalls, transform.position, Quaternion.identity);
            attackObjects.Add(orbitInstance);
            yield return new WaitForSeconds(timeForEachOrbit);
        }
            
            yield return new WaitForSeconds(timeToLeaveOrbitAttack);
           anim.Play("OrbitAttackLeave");
           yield return new WaitForSeconds(timeToStartOrbitAttack);
             _canTakeDamage = true;
      
        
    }
    IEnumerator EspiralOrbitAttack() 
    {
        anim.Play("EspiralAttack");
       GameObject espiralOrbitInstance = Instantiate(espiralOrbitPrefab, transform.position, transform.rotation);
        float duration = espiralOrbitInstance.GetComponent<BossSpiralOrbitAttack>().attackDuration;
        waitDuration = duration;
        attackObjects.Add(espiralOrbitInstance);
        yield return new WaitForSeconds(duration);
    Destroy(espiralOrbitInstance);

       
        
    }
    public void TakeDamage(float damage)
    {
        if(!_canTakeDamage) return;
        StartCoroutine(ChangeColorWhenHit());
        life--;

        if(life <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            StopAllCoroutines();
            anim.Play("Death");
            winMessage.SetActive(true);
            spriteRenderer.color = Color.white;
            for (int i = 0; i < attackObjects.Count; i++)
            {
                if (attackObjects[i] != null)
                Destroy(attackObjects[i]);
            }
        }

    }
    IEnumerator ChangeColorWhenHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        
    }
}
