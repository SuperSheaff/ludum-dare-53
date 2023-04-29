using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{

    protected Core core;
    protected Player player;
    protected PlayerData playerData;
    protected PlayerStateMachine stateMachine;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animatorBoolName;


    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animatorBoolName = animatorBoolName;

        core = player.Core;
    }

    public virtual void Enter() {
        DoChecks();
        player.playerAnimator.SetBool(animatorBoolName, true);
        startTime = Time.time;
        Debug.Log(animatorBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit() {
        player.playerAnimator.SetBool(animatorBoolName, false);
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
