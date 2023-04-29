using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{

    public Rigidbody2D rigidBody    { get; private set; }
    public int FacingDirection      { get; private set; }
    public Vector2 CurrentVelocity  { get; private set; }
    public bool CanSetVelocity      { get; set; }
    private Vector2 referenceVelocity;
    private Vector2 workspace;


    protected override void Awake() 
    {
        base.Awake();

        rigidBody = GetComponentInParent<Rigidbody2D>();

        workspace = new Vector2(0,0);
        FacingDirection = 1;
        CanSetVelocity = true;
    }

    public void LogicUpdate() 
    {
        CurrentVelocity = rigidBody.velocity;
    }

    public void PhysicsUpdate() 
    {
    }
    
    public void SetVelocityZero() 
    {
        workspace = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetVelocity(float xVelocity, float yVelocity, int direction) 
    {
        workspace.Set(xVelocity * direction, yVelocity);
        SetFinalVelocity();
    }

    public void SmoothDampVelocityX(float velocity, float movementSmoothing) 
    {

        workspace.Set(velocity, CurrentVelocity.y);
        SetFinalSmoothDampVelocity(movementSmoothing);
    }

    public void SmoothDampVelocityY(float velocity, float movementSmoothing) 
    {
        workspace.Set(CurrentVelocity.x, velocity);
        SetFinalSmoothDampVelocity(movementSmoothing);
    }

    public void SmoothDampVelocityXY(float velocityX, float velocityY, float movementSmoothing) 
    {

        workspace.Set(velocityX, velocityY);
        SetFinalSmoothDampVelocity(movementSmoothing);
    }

    // public void SetVelocityX(float velocity) 
    // {
    //     workspace.Set(velocity, CurrentVelocity.y);
    //     SetFinalVelocity();
    // }

    // public void SetVelocityY(float velocity) 
    // {
    //     workspace.Set(CurrentVelocity.x, velocity);
    //     SetFinalVelocity();
    // }

    private void SetFinalVelocity() 
    {
        if (CanSetVelocity) 
        {
            rigidBody.velocity = workspace;
            CurrentVelocity = workspace;
        }
    }

    private void SetFinalSmoothDampVelocity(float movementSmoothing) 
    {
        if (CanSetVelocity) 
        {

            rigidBody.velocity = Vector2.SmoothDamp(CurrentVelocity, workspace, ref referenceVelocity, movementSmoothing);
            CurrentVelocity = workspace;
        }
    }

    public void checkIfShouldFlip(int xInput) {
        if (xInput != 0 && xInput != FacingDirection) {
            Flip();
        }
    }

    public void Flip() {
        FacingDirection *= -1;
        rigidBody.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

}
