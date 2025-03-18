using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CollectableItem : MonoBehaviour, ICollectable  
{
    [SerializeField]ItemType type;
    Item myItem;
    [SerializeField]int amountToCollect;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Item item in InventoryManager.instance.items) 
        {
            if (item.type == type)
            {
                myItem = item;
                return;
            
            }
            
        
        
        }
    }

    public void Interact()
    {
        myItem.GetItem(amountToCollect);
        Destroy(gameObject);
    }
}
