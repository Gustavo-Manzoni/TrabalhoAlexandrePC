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
    [SerializeField] GameObject shootArrowParticle;
    [SerializeField] float arrowSpeed;
  


    
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
       

        if (playerMovement.GetHorVer().magnitude > 0.2f)
        {
            GameObject arrowInstance = Instantiate(arrow, playerMovement.GetHorVer() * 1.1f + playerMovement.transform.position, Quaternion.identity);
            arrowInstance.GetComponent<Rigidbody2D>().velocity = playerMovement.GetHorVer().normalized * arrowSpeed;
            Instantiate(shootArrowParticle, arrowInstance.transform.position, Quaternion.identity);
            Vector3 target = playerMovement.GetHorVer().normalized ;
            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            arrowInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else 
        {
            GameObject arrowInstance = Instantiate(arrow, _playerAnimation.GetLastDirections() * 1.1f + playerMovement.transform.position, Quaternion.identity);
            arrowInstance.GetComponent<Rigidbody2D>().velocity = _playerAnimation.GetLastDirections().normalized * arrowSpeed   ;
            Instantiate(shootArrowParticle, arrowInstance.transform.position, Quaternion.identity);
            Vector3 target = _playerAnimation.GetLastDirections().normalized;
            float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
            arrowInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        
    }
   
}
