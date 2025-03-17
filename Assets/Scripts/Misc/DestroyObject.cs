using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
   
    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);

    }
}
