using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openChest;
    [SerializeField] GameObject openingParticle;
    bool _canInteract;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        if (!_canInteract) { return; }
        _canInteract = false;
        spriteRenderer.sprite = openChest;
        Instantiate(openingParticle, transform.position, Quaternion.identity);

    }
}
