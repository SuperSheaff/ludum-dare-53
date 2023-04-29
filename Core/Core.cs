using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{


    public Movement Movement { get; private set; }
    // {
    //     get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name);
    //     private set => movement = value;
    // }

    public CollisionSenses CollisionSenses { get; private set; }
    // {
    //     get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
    //     private set => collisionSenses = value;
    // }

    private CollisionSenses collisionSenses;
    private Movement movement;

    private void Awake() 
    {
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Movement        = GetComponentInChildren<Movement>();
    }

    public void LogicUpdate() 
    {
        Movement.LogicUpdate();
    }

    public void FixedUpdate() 
    {
        Movement.PhysicsUpdate();
    }
}
