using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiItensManager : MonoBehaviour
{
    public List<GameObject> uiItens;
    int whatItem;
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            ChangeItem(true);

        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            ChangeItem(false);

        }
    }

      void ChangeItem(bool more)
    {
       
        if(more)
        {
            
            if(whatItem < uiItens.Count - 1 )
            {

                uiItens[whatItem].SetActive(false);
                whatItem++;
                uiItens[whatItem].SetActive(true);
                
            }else
            {

                uiItens[whatItem].SetActive(false);
                whatItem = 0;
                uiItens[whatItem].SetActive(true);
                
            }
        }else
        {
             if(whatItem > 0)
            {
                uiItens[whatItem].SetActive(false);
                whatItem--;
                uiItens[whatItem].SetActive(true);
                
            }else
            {

                uiItens[whatItem].SetActive(false);
                whatItem = uiItens.Count - 1;
                uiItens[whatItem].SetActive(true);

            }


        }

    }
}
