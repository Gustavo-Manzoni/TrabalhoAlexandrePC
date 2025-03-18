using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensManager : MonoBehaviour
{
   public static ItensManager instance;

    [Header("Amounts")]
    int speedPotion;




    void OnAwake()
    {
        instance = this;
    }

    
}
