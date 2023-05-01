using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMlemState : CreatureState
{

    private float mlemTimeElapsed;
    private bool mlemStartFlag;

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

        if (mlemStartFlag != true)
        {
            mlemTimeElapsed = 0f;
            mlemStartFlag = true;
        }

        creature.SetCanPickupCreature(true);
        creature.creatureAudioManager.PlayAudio("CreatureMLEMv1");
    }

    public override void Exit()
    {
        base.Exit();
        
        creature.SetCanPickupCreature(false);
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

                mlemStartFlag = false;
                stateMachine.ChangeState(creature.LayingEggState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
