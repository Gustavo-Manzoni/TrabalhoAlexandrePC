using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorPool : MonoBehaviour
{
    [SerializeField] GameObject damageText;
    [SerializeField]Canvas canva;
    Queue<GameObject> damageTextPool = new Queue<GameObject>();
    [SerializeField] int poolStarterSize;
    void Start()
    {
        for(int i = 0; i < poolStarterSize; i++)
        {

             GameObject obj = Instantiate(damageText, canva.transform);
            BackToPool(obj);


        }
    }

    public void BackToPool(GameObject obj)
    {
        damageTextPool.Enqueue(obj);
        obj.SetActive(false);
        
    }
    public GameObject GetFromPool()
    {
        if(damageTextPool.Count > 0){
         GameObject obj = damageTextPool.Dequeue();
         
         return obj;
         }else
         {
      GameObject obj = Instantiate(damageText, canva.transform);
return obj;
         }



    }
}
