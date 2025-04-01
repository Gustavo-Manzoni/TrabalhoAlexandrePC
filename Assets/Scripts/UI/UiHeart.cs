using System.Collections;
using UnityEngine;

public class UiHeart : MonoBehaviour
{
    IEnumerator Start() 
    {
        float elapsed = 0;
        Vector3 targetScale = transform.localScale;
        transform.localScale = Vector3.zero;
        while (elapsed < 0.2f)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsed / 0.2f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
       

    }
    public IEnumerator Fade(float fadeDuration) 
    {
        StopAllCoroutines();
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
