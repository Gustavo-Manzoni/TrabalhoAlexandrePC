using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSnake : MonoBehaviour, IInteractable
{
    bool _canInteract = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    public void Interact() 
    {
        if(!_canInteract) { return; }
    
    
    
    }
}
