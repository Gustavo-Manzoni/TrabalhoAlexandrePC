using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    SpeedPotion
        

}
[System.Serializable]
public class Item
{
    [SerializeField] public ItemType type;
    [SerializeField] int uses;
    [SerializeField] GameObject prefab;
    [SerializeField] ItemAmount amount;
    
    public void UseItem()
    {
        uses--;
        if(uses <= 0) 
        {
            UiItensManager.instance.ChangeItem(true);
            UiItensManager.instance.uiItens.Remove(prefab);
            prefab.SetActive(false);
        }
    }
    public int Uses() 
    {
        
        return  uses;
    
    }
    
    public void GetItem(int amount)
    {
        uses += amount;
        if (!UiItensManager.instance.uiItens.Contains(prefab))
        {
            UiItensManager.instance.uiItens.Add(prefab);
        }
     
        CheckItemAmount();
    }
    public bool CanUse()
    {
        if(uses > 0) 
        {
            return true;
        }
        else 
        {

            return false;
        }
     
    }
    public void CheckItemAmount()
    {
        amount.CheckItems();
    }

}