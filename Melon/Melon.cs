using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider     = GetComponent<BoxCollider2D>();
        spriteRenderer   = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableMelon()
    {
        spriteRenderer.enabled  = false;
        boxCollider.enabled     = false;
    }
}
