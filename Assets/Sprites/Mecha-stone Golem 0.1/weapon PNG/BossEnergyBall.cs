using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnergyBall : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        StartCoroutine(GameJuiceSize());
        rb = GetComponent<Rigidbody2D>();

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
    public void Move(float speed)
    {
        rb.velocity = -     (transform.parent.position - transform.position).normalized * speed ;


    }
}
