using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileDamage : MonoBehaviour
{
    [SerializeField] bool isDestroyable;
    [SerializeField] GameObject hitParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.TryGetComponent(out IDamageablePlayer target))
        {

            target.TakeDamage(4);
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);
            if (isDestroyable)
            {
                Destroy(gameObject);
            }



        }
        if (collision.gameObject.TryGetComponent(out IKnockbackablePlayer target2))
        {

            Vector2 direction = collision.gameObject.transform.position - transform.position;
            InventoryManager.instance.StartCoroutine(target2.TakeKnockback(0.25f, 45, direction.normalized));


        }
    }
}
