using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PressurePlate : MonoBehaviour
{
    public UnityEvent OnPressed;
    [SerializeField] float cameraFinalSize;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            {
            OnPressed.Invoke();
            StartCoroutine(CameraZoomOut());    
        }
    }
    IEnumerator CameraZoomOut() 
    {
        while(Camera.main.orthographicSize < cameraFinalSize) 
        {
            Camera.main.orthographicSize += 0.2f;
            yield return new WaitForSeconds(0.01f);
        
        
        }
    
    }
}
