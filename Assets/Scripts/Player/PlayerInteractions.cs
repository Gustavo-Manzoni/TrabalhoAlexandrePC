using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
   
    List<ICollectable> itemsToCollect = new List<ICollectable>();
    List<IInteractable> itemsToInteract = new List<IInteractable>();
    [SerializeField] GameObject interactionPrompt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (itemsToInteract.Count > 0)
            {
                itemsToInteract[0].Interact();


            }
            if (itemsToCollect.Count > 0) 
            {
                itemsToCollect[0].Interact();
              
            
            }
            
           
        
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<ICollectable>( out ICollectable item)) 
        {
           
            itemsToCollect.Add(item);
            interactionPrompt.SetActive(true);


        }
        if (collision.gameObject.TryGetComponent<IInteractable>(out IInteractable itemInt))
        {
            itemsToInteract.Add(itemInt);
            interactionPrompt.SetActive(true);


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ICollectable>(out ICollectable item))
        {
            itemsToCollect.Remove(item);
            if (itemsToCollect.Count <= 0)
            {
                interactionPrompt.SetActive(false);

            }

        }
        if (collision.gameObject.TryGetComponent<IInteractable>(out IInteractable itemInt))
        {
            itemsToInteract.Remove(itemInt);
            if (itemsToCollect.Count <= 0)
            {
                interactionPrompt.SetActive(false);

            }


        }
    }
}
