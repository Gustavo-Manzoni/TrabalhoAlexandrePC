using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnergyBall : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    public void Move(float speed)
    {
        rb.velocity = - (transform.parent.position - transform.position).normalized * speed ;


    }
}
