using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.TryGetComponent(out IDamageable target) && collision.gameObject.tag != "Player")
        {
            target.TakeDamage(4);


        }
         if(collision.gameObject.TryGetComponent(out IKnockbackable targetToKnockback) && collision.gameObject.tag != "Player")
        {
            Vector2 direction = (Vector2)FindObjectOfType<PlayerMovement>().gameObject.transform.position - (Vector2)collision.gameObject.transform.position;
            StartCoroutine(targetToKnockback.TakeKnockback(0.25f, 10, -direction.normalized));
 

        }

    }
}
