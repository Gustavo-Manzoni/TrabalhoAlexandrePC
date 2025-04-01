using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]float timeToAttack;
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





    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(StartAttacking());
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator StartAttacking() 
    {
        yield return new WaitForSeconds(timeToAttack);
        StartCoroutine(OrbitAttack());
    
    
    }
    IEnumerator ProjectileHandAttack() 
    {
        anim.Play("ProjectileHandAttack");
        yield return new WaitForSeconds(timeToInstantiateArm);
        Instantiate(ArmProjectile, transform.position, Quaternion.identity);
        
    }
     IEnumerator OrbitAttack() 
    {
        anim.Play("OrbitAttackEntry");
        yield return new WaitForSeconds(timeToStartOrbitAttack);
        for(int i = 0; i < orbitAttackAmount ; i++)
        {
           Instantiate(orbitBalls, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeForEachOrbit);
        }
           anim.Play("OrbitAttackLeave");
           yield return new WaitForSeconds(timeToStartOrbitAttack);
           anim.Play("Idle");
        
    }
}
