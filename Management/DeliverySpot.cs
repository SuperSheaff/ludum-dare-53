using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySpot : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator animator;

    void Start()
    {
        animator   = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDelivered(int value)
    {
        animator.SetInteger("creatures", value);
    }
}
