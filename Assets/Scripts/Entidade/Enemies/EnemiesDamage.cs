using UnityEngine;

public class EnemiesDamage : MonoBehaviour
{
    [SerializeField] bool isDestroyable;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.TryGetComponent(out IDamageablePlayer target)) 
        {

            target.TakeDamage(4);
            if (isDestroyable) 
            {
                Destroy(gameObject);
            }   
           
        
        
        }
        if (collision.gameObject.TryGetComponent(out IKnockbackablePlayer target2))
        {

            Vector2 direction = collision.gameObject.transform.position - transform.position;
            InventoryManager.instance.StartCoroutine(target2.TakeKnockback(0.25f, 45, direction.normalized))    ;
           

        }
    }
}
