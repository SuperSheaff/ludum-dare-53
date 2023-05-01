using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureLayingEggState : CreatureState
{

    private GameObject creaturePrefab;
    private float layingEggTimeElapsed;

    public CreatureLayingEggState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        layingEggTimeElapsed = 0f;
        creature.creatureAudioManager.PlayAudio("CreatureLayingEggv1");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityZero();

        layingEggTimeElapsed += Time.deltaTime;

        if (layingEggTimeElapsed >= creatureData.layingEggDuration)
        {
            if (!isExitingState) 
            {
                creature.LayEgg();
                creature.StartLayingEggCooldown();
                stateMachine.ChangeState(creature.IdleState);
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
