using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionRange : MonoBehaviour
{
    
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player")) 
        {
            
            if (gameObject.transform.parent.TryGetComponent(out IMovable enemy))
            {
                enemy.Move();
                
            
            }
        
        }

    }
}
