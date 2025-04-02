using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpiralOrbitAttack : MonoBehaviour
{
    public float attackDuration;
    [SerializeField] float rotationSpeed;
    void Start()
    {
    
    for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            GameObject childObj = child.gameObject;
            childObj.SetActive(true);
        }

    }

    void Update()
    {

        transform.rotation *= Quaternion.Euler(0,0,rotationSpeed * Time.deltaTime);



    }

}
