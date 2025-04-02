using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileHand : MonoBehaviour, IDamageable
{
    
    Transform target;
    [SerializeField]float speed;
    Rigidbody2D rb;
    int life;
    [SerializeField] int maxLife;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        life = maxLife;
    }
    public void TakeDamage(float damage)
    {
        life--;
        StartCoroutine(ChangeColorWhenHit());
        if(life <= 0 )
        {
            Destroy(gameObject);

        }

    }
    IEnumerator ChangeColorWhenHit()
    {
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 MoveDirection = -(transform.position - target.position); 
        rb.velocity = new Vector2(MoveDirection.x, MoveDirection.y).normalized *speed;
      
        Vector3 direction = target.position - transform.position;
        direction.z = 0; 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           transform.rotation  = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0, angle), Time.fixedDeltaTime * 5);
            
        
        
      

    }
}
