using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    ICollectable itemToCollect;
    IInteractable itemToInteract;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            itemToInteract?.Interact();
            itemToCollect?.Interact();
           
        
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<ICollectable>( out ICollectable item)) 
        {
            itemToCollect = item;



        }
        if (collision.gameObject.TryGetComponent<IInteractable>(out IInteractable itemInt))
        {
            itemToInteract = itemInt;



        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ICollectable>(out ICollectable item))
        {
            itemToCollect = null;



        }
        if (collision.gameObject.TryGetComponent<IInteractable>(out IInteractable itemInt))
        {
            itemToInteract = null;



        }
    }
}
