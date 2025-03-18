using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static ItemType type;
    public static Dictionary<ItemType, Item> inventory = new Dictionary<ItemType, Item>();
    [SerializeField] public Item[] items;
    public static InventoryManager instance;

    void Awake()
    {
      instance = this;
        type = ItemType.Sword;
        foreach (ItemType itemType in System.Enum.GetValues(typeof(ItemType)))
        {
          
            ItemType type;
            for(int i = 0; i < items.Length; i++)
            {
               
               
                type = items[i].type;
                if(type == itemType) 
                {
                    inventory.Add(itemType, items[i]);
                    
                    

                   
                }
            
            }




        }

    }
    public static void Use()
    {
        
        switch (type)
        {
           case ItemType.SpeedPotion:
             
              PlayerInteractionsManager.instance.StartCoroutine(PlayerInteractionsManager.instance.SpeedPotion());
                
                

           break;

          














        }
        inventory[type].UseItem();
        inventory[type].CheckItemAmount();
    


    }
   
    
}
