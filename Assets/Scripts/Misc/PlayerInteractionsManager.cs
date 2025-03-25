using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionsManager : MonoBehaviour
{
    public static PlayerInteractionsManager instance;

    PlayerMovement playerMovement;
    PlayerAnimation _playerAnimation;
    
    [SerializeField] float speedPotionDuration;
    [Space]
    [SerializeField] GameObject arrow;


    
   void Awake()
   {
        instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();


    }
   public IEnumerator SpeedPotion()
   {
       
           playerMovement.playerSpeed += 2.3f;
            yield return new WaitForSeconds(speedPotionDuration);
            playerMovement.playerSpeed = playerMovement.Speed;

        
      
   }
    public void Bow() 
    {
        GameObject arrowInstance = Instantiate(arrow, _playerAnimation.GetLastDirections() + playerMovement.transform.position, Quaternion.identity);
        arrowInstance.GetComponent<Rigidbody2D>().velocity = _playerAnimation.GetLastDirections();


    }

}
