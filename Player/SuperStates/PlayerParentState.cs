using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParentState : PlayerState
{

    protected Vector2 input;
    protected int xInput;
    protected int yInput;

    public PlayerParentState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
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
        
        xInput          = player.InputHandler.NormInputX;
        yInput          = player.InputHandler.NormInputY;

        core.Movement.checkIfShouldFlip(xInput);

        player.playerAnimator.SetFloat("xInput", xInput);
        player.playerAnimator.SetFloat("yInput", yInput);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
