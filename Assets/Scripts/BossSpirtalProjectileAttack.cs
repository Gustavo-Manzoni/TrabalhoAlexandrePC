using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpirtalProjectileAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float timePerInstance;
    [SerializeField] float projectileSpeed;
    IEnumerator Start()
    {
        Destroy(gameObject, 7);
        StartCoroutine(GameJuiceSize());
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Attack());
        


    }
   
    IEnumerator GameJuiceSize() 
    {
        float elapsed = 0;
        Vector3 targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
        while (elapsed < 0.2f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsed / 0.2f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
       

    }
    IEnumerator Attack()
    {
        GameObject instance = Instantiate(projectilePrefab,transform.position,Quaternion.identity);
        instance.transform.localScale = transform.localScale;
        instance.GetComponent<Rigidbody2D>().velocity = -(transform.parent.position - transform.position) * projectileSpeed ;
        yield return new WaitForSeconds(timePerInstance);
        StartCoroutine(Attack());


    }

    
}
