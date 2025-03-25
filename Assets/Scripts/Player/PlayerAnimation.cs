using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [HideInInspector]public Animator anim;
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
        else if (playerMov.horizontal != 0)
        {
            anim.SetFloat("LastHorizontal", playerMov.horizontal);
            anim.SetFloat("LastVertical", 0);
        }

       
        anim.SetInteger("Horizontal", (int)playerMov.horizontal);
        anim.SetInteger("Vertical", (int)playerMov.vertical);

   
        anim.SetBool("Running", playerMov.rb.velocity.magnitude > 0);


    }
    public void AttackAnimation()
    {
        anim.SetTrigger("Attack");

    }
    public Vector3 GetLastDirections() 
    {
    
        return new Vector3(anim.GetFloat("LastHorizontal"), anim.GetFloat("LastVertical"));

    }
}
