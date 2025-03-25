using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class ItemAmount : MonoBehaviour
{
    [SerializeField] ItemType type;
    TMP_Text text;
    // Start is called before the first frame update
    void Awake() 
    {
       text = GetComponent<TMP_Text>();
    }
    void OnEnable()
    {
       
        CheckItems();
    }
    public void CheckItems() 
    {
            
            text.text = InventoryManager.inventory[type].Uses().ToString() + "x";
          

    }

    
}
