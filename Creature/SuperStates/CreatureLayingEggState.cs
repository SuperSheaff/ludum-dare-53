using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureLayingEggState : CreatureState
{

    private GameObject eggObject;

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

        eggObject = null;
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

        eggObject = Instantiate(creature.CreaturePrefab, creature.transform.position, Quaternion.identity);
        Creature eggCreature = eggObject.GetComponent<Creature>();
        eggCreature.StateMachine.ChangeState(eggCreature.EggState);
        stateMachine.ChangeState(creature.IdleState);

    }
}
