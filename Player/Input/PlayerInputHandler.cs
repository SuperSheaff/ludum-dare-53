using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputHandler : MonoBehaviour
{

    public Vector2 RawMovementInput     { get; private set; }
    public int NormInputX               { get; private set; }
    public int NormInputY               { get; private set; }

    public bool InteractButtonPressed   { get; private set; }
    public float InteractCooldown = 0.25f; // The cooldown period for the interact button

    private float lastInteractTime = 0f; // The time when the interact button was last pressed


    [SerializeField]

    private void Start() 
    {
    }

    private void Update() 
    {
        lastInteractTime += Time.deltaTime;
    }

    public void OnMoveInput(InputAction.CallbackContext context) 
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnInteractInput(InputAction.CallbackContext context) 
    {

        if (!InteractButtonPressed && context.started && lastInteractTime >= InteractCooldown)
        {
            InteractButtonPressed = true;
            lastInteractTime = 0f;
            // handle initial key press down here
        }
        else if (InteractButtonPressed && context.canceled)
        {
            InteractButtonPressed = false;
            // handle key release here, if needed
        }
    }
}
