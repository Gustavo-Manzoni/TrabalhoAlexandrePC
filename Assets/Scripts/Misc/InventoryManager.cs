using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static ItemType itemToUse;

    void Awake()
    {
        itemToUse = ItemType.Sword;

    }
    public static void UseItem()
    {
        switch(itemToUse)
        {
           case ItemType.SpeedPotion:
             
              PlayerInteractionsManager.instance.StartCoroutine(PlayerInteractionsManager.instance.SpeedPotion());

           break;

          














        }


    }
}
