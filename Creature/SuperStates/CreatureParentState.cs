using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureParentState : CreatureState
{

    public CreatureParentState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        


        // core.Movement.checkIfShouldFlip(xInput);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
