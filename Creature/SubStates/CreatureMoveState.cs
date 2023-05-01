using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMoveState : CreatureParentState
{
    private Vector3 referenceVelocity;
    private float distanceToDestination;

    public CreatureMoveState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        creature.SetRandomMoveLocation();
    }

    public override void Exit()
    {
        base.Exit();

        core.Movement.SetVelocityZero();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        distanceToDestination = Vector2.Distance(creature.transform.position, creature.GetRandomMoveLocation());

        if (distanceToDestination <= creatureData.destinationThreshold)
        {
            // Transition back to idle state
            if (!isExitingState) 
            {
                stateMachine.ChangeState(creature.IdleState);
            }
        }

        creature.transform.position = Vector3.MoveTowards(creature.transform.position, creature.GetRandomMoveLocation(), creature.GetTotalMoveSpeed() * Time.deltaTime);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void AnimationFinishedTrigger()
    {
        base.AnimationFinishedTrigger();

        creature.creatureAudioManager.PlayAudio("CreatureMove");
    }
}
