using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureIdleState : CreatureParentState
{
    private float idleDuration;
    private float idleTimeElapsed;

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

        idleDuration = Random.Range(1f, 3f);
        idleTimeElapsed = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityZero();
        
        idleTimeElapsed += Time.deltaTime;

        if (idleTimeElapsed >= idleDuration)
        {
            if (!isExitingState) 
            {
                stateMachine.ChangeState(creature.MoveState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
