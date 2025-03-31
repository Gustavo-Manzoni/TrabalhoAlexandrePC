using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class Dummy : MonoBehaviour, IDamageable
{

   DamageIndicatorPool damageIndicator;
    Animator anim;
    UnityEvent WhenDestroy;

    void Start()
    {
        damageIndicator = FindObjectOfType<DamageIndicatorPool>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // public IEnumerator TakeKnockback(float knockbackDuration, float knockbackForce, Vector2 direction)
    // {
    //     float elapsed = 0;
    //     while(elapsed < knockbackDuration)
    //     {

    //         rb.AddForce(direction * knockbackForce);
    //         elapsed += Time.deltaTime;
    //         yield return null;
    //     }
       


    // }
    public void TakeDamage(float damage)
    {
        anim.SetTrigger("GetHurt");
        GameObject obj = damageIndicator.GetFromPool();
        obj.transform.position = transform.position;
    
        obj.SetActive(true);
        obj.GetComponent<TMP_Text>().text = damage.ToString();

    }
    void OnDestroy() 
    {
        WhenDestroy?.Invoke();
    
    }
}
