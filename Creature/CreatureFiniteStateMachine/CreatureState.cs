using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureState
{

    protected Core core;
    protected Creature creature;
    protected CreatureData creatureData;
    protected CreatureStateMachine stateMachine;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animatorBoolName;


    public CreatureState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) {
        this.creature = creature;
        this.stateMachine = stateMachine;
        this.creatureData = creatureData;
        this.animatorBoolName = animatorBoolName;

        core = creature.Core;
    }

    public virtual void Enter() {
        DoChecks();
        creature.creatureAnimator.SetBool(animatorBoolName, true);
        startTime = Time.time;
        Debug.Log(animatorBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit() {
        creature.creatureAnimator.SetBool(animatorBoolName, false);
        isExitingState = true;
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() {
        DoChecks();
    }

    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishedTrigger() => isAnimationFinished = true;

    public virtual void AnimationStartMovementTrigger() { }

    public virtual void AnimationStopMovementTrigger() { }

    public virtual void AnimationTurnOffFlip() { }

    public virtual void AnimationTurnOnFlip() { }

    public virtual void AnimationActionTrigger() { }
}
