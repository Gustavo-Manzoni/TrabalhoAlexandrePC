using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent(out IDamageable target) && collision.gameObject.tag != "Player")
        {
            target.TakeDamage(4);
            Destroy(gameObject);

        }
        if (collision.gameObject.TryGetComponent(out IKnockbackable targetToKnockback) && collision.gameObject.tag != "Player")
        {
            Vector2 direction = (Vector2)transform.position - (Vector2)collision.gameObject.transform.position;
            InventoryManager.instance.StartCoroutine(targetToKnockback.TakeKnockback(0.25f, 19, -direction.normalized));
            Destroy(gameObject);
            //Professor voc� pode julgar meus metodos mas n�o meus resultados kkkkkk
            //eu so passei a responsabilidade de chamar a
            //corroutina para uma instancia que eu ja criei para n�o precisar
            //criar uma nova e considerando que o invcentario sempre vai existir


        }
    

    }
}
