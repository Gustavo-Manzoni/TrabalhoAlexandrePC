using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiItensManager : MonoBehaviour
{
    public List<GameObject> uiItens;
    int whatItem;
    // Start is called before the first frame update
    void Start()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            ChangeItem();

        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            ChangeItem();

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ChangeItem()
    {


    }
}
