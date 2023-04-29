using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureIdleState : CreatureParentState
{

    public CreatureIdleState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
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
        
        if (!isExitingState) 
        {

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
