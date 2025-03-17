using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform player;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, speed * Time.deltaTime);


    }
}
