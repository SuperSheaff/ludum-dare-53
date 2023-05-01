using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturePreScremState : CreatureState
{

    private float preScremTimeElapsed;

    public CreaturePreScremState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        creature.SetCanBeFed(true);
        preScremTimeElapsed = 0f;
        creature.creatureAudioManager.PlayAudio("CreaturePreSCREMv1");
    }

    public override void Exit()
    {
        base.Exit();

        creature.SetCanBeFed(false);
        creature.creatureAudioManager.StopAudio();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // core.Movement.checkIfShouldFlip(xInput);

        core.Movement.SetVelocityZero();

        preScremTimeElapsed += Time.deltaTime;

        if (preScremTimeElapsed >= creatureData.preScremDuration)
        {
            if (!isExitingState) 
            {
                stateMachine.ChangeState(creature.ScremState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
