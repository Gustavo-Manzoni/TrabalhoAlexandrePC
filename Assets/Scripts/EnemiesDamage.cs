using UnityEngine;

public class EnemiesDamage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.TryGetComponent(out IDamageablePlayer target)) 
        {

            target.TakeDamage(4);
           
        
        
        }
        if (collision.gameObject.TryGetComponent(out IKnockbackablePlayer target2))
        {

            Vector2 direction = collision.gameObject.transform.position - transform.position;
            InventoryManager.instance.StartCoroutine(target2.TakeKnockback(0.25f, 45, direction.normalized))    ;
           

        }
    }
}
