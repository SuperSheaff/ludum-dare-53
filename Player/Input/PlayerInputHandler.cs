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

    [SerializeField]

    private void Start() {
    }

    private void Update() {
    }

    public void OnMoveInput(InputAction.CallbackContext context) {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
}
