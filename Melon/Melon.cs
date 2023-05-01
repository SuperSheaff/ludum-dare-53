using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private MelonPlant melonPlant;
    private bool melonPickedOffPlant;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider     = GetComponent<BoxCollider2D>();
        spriteRenderer   = GetComponent<SpriteRenderer>();

        melonPickedOffPlant = false;

        melonPlant = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMelonPlant(MelonPlant melonPlant)
    {
        this.melonPlant = melonPlant;
    }

    public void DisableMelon()
    {
        if (!melonPickedOffPlant) 
        {
            melonPickedOffPlant = true;
            melonPlant.MelonTaken();
        }

        spriteRenderer.enabled  = false;
        boxCollider.enabled     = false;
    }

    public void EnableMelon()
    {
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
    }

    public void DestroyMelon()
    {
        Destroy(this.gameObject);
    }


}
