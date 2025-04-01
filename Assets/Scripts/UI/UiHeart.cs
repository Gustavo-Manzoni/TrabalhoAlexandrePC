using System.Collections;
using UnityEngine;

public class UiHeart : MonoBehaviour
{
    
    public IEnumerator Fade(float fadeDuration) 
    {
        float elapsed = 0;
        Vector3 starterScale = transform.localScale;
        while (elapsed < fadeDuration) 
        {   
            transform.localScale = Vector3.Lerp(starterScale, Vector3.zero, elapsed/fadeDuration);
            elapsed += Time.deltaTime;  
            yield return null;
        }
        Destroy(gameObject);
    
    }
}
