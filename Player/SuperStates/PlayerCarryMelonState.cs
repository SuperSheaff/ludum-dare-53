using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryMelonState : PlayerState
{

    protected Vector2 input;
    protected int xInput;
    protected int yInput;
    protected bool interactButtonPressed;

    public PlayerCarryMelonState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
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
        
        xInput                  = player.InputHandler.NormInputX;
        yInput                  = player.InputHandler.NormInputY;
        interactButtonPressed   = player.InputHandler.InteractButtonPressed;

        core.Movement.checkIfShouldFlip(xInput);

        player.playerAnimator.SetFloat("xInput", xInput);
        player.playerAnimator.SetFloat("yInput", yInput);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
