using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMlemState : CreatureState
{

    private float mlemTimeElapsed;

    public CreatureMlemState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        mlemTimeElapsed = 0f;
        creature.SetCanPickupCreature(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityZero();

        mlemTimeElapsed += Time.deltaTime;

        if (mlemTimeElapsed >= creatureData.mlemDuration)
        {
            if (!isExitingState) 
            {
                if (creature.GetMood() >= 100f)
                {
                    creature.SetMood(90f);
                }

                stateMachine.ChangeState(creature.LayingEggState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
