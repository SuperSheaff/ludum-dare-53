using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureEggState : CreatureState
{
    private float eggTimeElapsed;

    public CreatureEggState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        eggTimeElapsed = 0f;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityZero();
        
        eggTimeElapsed += Time.deltaTime;

        if (eggTimeElapsed >= creatureData.eggDuration)
        {
            if (!isExitingState) 
            {
                stateMachine.ChangeState(creature.IdleState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
    }
}
