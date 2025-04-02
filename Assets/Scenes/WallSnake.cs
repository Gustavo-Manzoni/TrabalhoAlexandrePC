using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WallSnake : MonoBehaviour, IInteractable
{
    bool _canInteract = true;
    public UnityEvent OnInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

  
    public void Interact() 
    {
        if(!_canInteract) { return; }
        _canInteract = false;
        OnInteract.Invoke();
    
    }
}
