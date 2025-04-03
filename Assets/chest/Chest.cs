using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openChest;
    [SerializeField] GameObject openingParticle;
    bool _canInteract;
    [SerializeField] bool negativeX;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _canInteract = true;
    }
    public void Interact()
    {
        if (!_canInteract) { return; }
        _canInteract = false;
        spriteRenderer.sprite = openChest;
        Instantiate(openingParticle, transform.position, Quaternion.identity);
        Instantiate(openingParticle, transform.position, Quaternion.identity);
        StartCoroutine(SpawnItem(30, 100, 2));

    }
    IEnumerator SpawnItem(float minX, float maxX, int amount) 
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject item = Instantiate(InventoryManager.instance.ItemsPrefab[Random.Range(0, InventoryManager.instance.ItemsPrefab.Length)],transform.position,Quaternion.identity);
            Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
            if (negativeX) 
            {
                minX *= -1;
                maxX *= -1;
            }
            rb.AddForce(new Vector2(Random.Range(minX, maxX), Random.Range(350, 400)));
            rb.gravityScale = 3f;
            StartCoroutine(ResetGravity(rb, 0.7f));
            yield return new WaitForSeconds(0.3f);
        }
    
    
    }
    IEnumerator ResetGravity(Rigidbody2D rb, float delay) 
    {
        yield return new WaitForSeconds(delay);
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;
      
    
    }
}
