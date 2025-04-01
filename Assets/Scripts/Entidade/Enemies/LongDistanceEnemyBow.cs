using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistanceEnemyBow : MonoBehaviour
{
    Transform orbitCenter, targetToAim;
    [SerializeField] float orbitRadius;
    // Start is called before the first frame update
    void Start()
    {
        orbitCenter = transform.parent;
        targetToAim = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
      
        Orbit();

    }
    void Orbit()
    {

        Vector3 target = targetToAim.position - orbitCenter.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 42);

        float currentAngle = Mathf.Atan2(target.y, target.x);
        float x = orbitRadius * Mathf.Cos(currentAngle) + orbitCenter.position.x;
        float y = orbitRadius * Mathf.Sin(currentAngle) + orbitCenter.position.y;
        transform.position = new Vector3(x, y, 0);

    }
}
