using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetItem : MonoBehaviour
{
    [SerializeField] ItemType item;
    void OnEnable()
    {
        InventoryManager.type = item;
        

    }
}
