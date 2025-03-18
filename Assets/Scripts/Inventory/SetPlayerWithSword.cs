using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerWithSword : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitToTurnTrue());
    }
    IEnumerator WaitToTurnTrue() 
    {
        yield return new WaitForSeconds(0.03f);
        InventoryManager.instance.playerWithSword = true;
    }
}
