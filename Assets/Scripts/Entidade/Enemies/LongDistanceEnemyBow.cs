using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistanceEnemyBow : MonoBehaviour
{
    Transform orbitCenter;
    [SerializeField] float orbitRadius;
    [SerializeField] float cooldownPerShoot;
    [SerializeField]  float delayToShootArrow;
    [SerializeField] GameObject arrow;
    [SerializeField] float arrowSpeed;
    bool _canShoot;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        orbitCenter = transform.parent;
        anim = GetComponent<Animator>();
        _canShoot = true;
      
    }

    // Update is called once per frame
    void Update()
    {
      
        Orbit();

    }
    void Orbit()
    {

        Vector3 targetToAim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
        Vector3 target = targetToAim - orbitCenter.position;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;  
        transform.rotation = Quaternion.Euler(0, 0, angle + 42);

        float currentAngle = Mathf.Atan2(target.y, target.x);
        float x = orbitRadius * Mathf.Cos(currentAngle) + orbitCenter.position.x;
        float y = orbitRadius * Mathf.Sin(currentAngle) + orbitCenter.position.y;
        transform.position = new Vector3(x, y, 0);

        if(Input.GetMouseButtonDown(0) && _canShoot)
        {
           
            StartCoroutine(ShootArrow(angle, target));
            StartCoroutine(ResetShootCooldown());
        }

    }
    IEnumerator ShootArrow(float arrowAngle, Vector3 direction)
    {
                anim.SetTrigger("Shoot");  
                yield return new WaitForSeconds(delayToShootArrow);
        GameObject arrowInstance = Instantiate(arrow, transform.position, Quaternion.Euler(0,0, arrowAngle));
    arrowInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * arrowSpeed;
 

    }
    IEnumerator ResetShootCooldown()
    {
        _canShoot = false;
        yield return new WaitForSeconds(cooldownPerShoot);
        _canShoot = true;


    }
}
