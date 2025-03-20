using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out IDamageable target) && collision.gameObject != transform.parent) 
        {
        
        
        
        
        
        }
        if (collision.gameObject.TryGetComponent(out IKnockbackable target2) && collision.gameObject != transform.parent)
        {





        }
    }
}
