using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField]float timeToAttack;
    [SerializeField] float timeToInstantiateArm;
    [SerializeField] GameObject ArmProjectile;
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
        StartCoroutine(ProjectileHandAttack());
    
    
    }
    IEnumerator ProjectileHandAttack() 
    {
        anim.Play("ProjectileHandAttack");
        yield return new WaitForSeconds(timeToInstantiateArm);
        Instantiate(ArmProjectile, transform.position, Quaternion.identity);
        
    }
}
