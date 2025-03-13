using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    PlayerMovement playerMov;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMov = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerMov.vertical != 0)
        {
            anim.SetFloat("LastVertical", playerMov.vertical);
            anim.SetFloat("LastHorizontal", 0);
        }
        if (playerMov.horizontal != 0)
        {
            anim.SetFloat("LastHorizontal", playerMov.horizontal);
            anim.SetFloat("LastVertical", 0);
        }
      


        anim.SetFloat("Horizontal", playerMov.horizontal);
            anim.SetFloat("Vertical", playerMov.vertical);

        
            anim.SetBool("Running", playerMov.rb.velocity.magnitude != 0 ? true : false);
    
        
    }
}
