using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureScremState : CreatureState
{

    private float scremTimeElapsed;

    public CreatureScremState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        scremTimeElapsed = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityZero();

        scremTimeElapsed += Time.deltaTime;

        if (scremTimeElapsed >= creatureData.scremDuration)
        {
            if (!isExitingState) 
            {
                stateMachine.ChangeState(creature.DeathState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
