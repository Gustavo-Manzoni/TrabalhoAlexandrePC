using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dummy : MonoBehaviour, IDamageable
{
    Rigidbody2D rb;
   DamageIndicatorPool damageIndicator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        damageIndicator = FindObjectOfType<DamageIndicatorPool>();
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
        GameObject obj = damageIndicator.GetFromPool();
        Transform tr = obj.GetComponent<Transform>();
        tr.position = transform.position - Vector3.up * 3;
        obj.SetActive(true);
        obj.GetComponent<TMP_Text>().text = damage.ToString();

    }
}
