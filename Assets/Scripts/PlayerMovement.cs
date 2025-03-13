using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : CharacterStatus
{
    [HideInInspector] public float horizontal, vertical;
    [HideInInspector]public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Vertical");
        
    }
    private void FixedUpdate()
    {
         rb.velocity = new Vector2(horizontal, vertical).normalized * Speed;


    }

}
