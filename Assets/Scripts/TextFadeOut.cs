using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextFadeOut : MonoBehaviour
{   
   
    [SerializeField] float animationDuration;
    DamageIndicatorPool damageIndicator;
    TMP_Text text;

    void OnEnable()
    {
        text = GetComponent<TMP_Text>();
        Color color = text.color;
        color.a = 1;
        text.color = color;
        Vector3 finalPos = transform.position + new Vector3(0.3f, 1.5f, 0);
         Vector3 starterPos = transform.position;
        StartCoroutine(TextAnimation(finalPos, starterPos));

    }
    IEnumerator TextAnimation(Vector3 finalPos, Vector3 starterPos)
    {
         damageIndicator = FindObjectOfType<DamageIndicatorPool>();
        
        
        float elapsed = 0;
        while(elapsed < animationDuration)
        {

            transform.position = Vector3.Lerp(starterPos, finalPos, elapsed / animationDuration);
            Color color = text.color;
            color.a = Mathf.Lerp(1,0, elapsed/animationDuration);
            text.color = color;
            
            elapsed += Time.deltaTime;
            yield return null;
        }

        damageIndicator.BackToPool(gameObject);
    }
    
}
