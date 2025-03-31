using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSpikes : MonoBehaviour
{
    [SerializeField]bool isActive;

    [SerializeField] float timeToChangeState;
    [SerializeField] Sprite[] states;
    GameObject colliderDamage;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        colliderDamage = transform.GetChild(0).gameObject;
      
        StartCoroutine(ChangeState(!isActive));
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator ChangeState(bool state)
    {
        if (state) 
        {
            for(int i = 0; i < states.Length; i++) 
            {
               
                spriteRenderer.sprite = states[i];
                yield return new WaitForSeconds(0.1f);
               
            }

        }
        else 
        {
            for (int i = states.Length - 1; i > -1; i--)
            {
               
                spriteRenderer.sprite = states[i];
                
                yield return new WaitForSeconds(0.1f);

            }
            
        }
        colliderDamage.SetActive(state);

        yield return new WaitForSeconds(timeToChangeState);
        isActive = !isActive;
        StartCoroutine(ChangeState(!isActive));
    
    
    }
}
