using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionsManager : MonoBehaviour
{
    public static PlayerInteractionsManager instance;
    PlayerMovement playerMovement;
    [SerializeField] float speedPotionDuration;
    
   void Awake()
   {
        instance = this;
        playerMovement = GetComponent<PlayerMovement>();

   }
   public IEnumerator SpeedPotion()
   {
        
        playerMovement.playerSpeed += 2.3f;
        yield return new WaitForSeconds(speedPotionDuration);
        playerMovement.playerSpeed = playerMovement.Speed;

   }

}
