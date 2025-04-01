using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnergyFireOrbit : MonoBehaviour
{
   [SerializeField] float rotationSpeed;
   [SerializeField] float ballsSpeed;
   bool _canRotate;
    IEnumerator Start()
    {
        _canRotate = true;
        yield return new WaitForSeconds(Random.Range(1f,3f));
        _canRotate = false;
        yield return new WaitForSeconds(0.8f);
        foreach (Transform child in transform)
        {
            if(child.gameObject.TryGetComponent(out BossEnergyBall energyBall))
            {
                energyBall.Move(ballsSpeed);


            }
            
           
        }






    }

    // Update is called once per frame
    void Update()
    {
        if(_canRotate)
        {
            transform.rotation *= Quaternion.Euler(0,0, rotationSpeed * Time.deltaTime);

        }
    }
}
