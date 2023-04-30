using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureLayingEggState : CreatureState
{

    private GameObject creaturePrefab;

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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SetVelocityZero();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();
        
        creature.LayEgg();
        creature.StartLayingEggCooldown();
        stateMachine.ChangeState(creature.IdleState);
    }
}
