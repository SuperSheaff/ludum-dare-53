using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryMelonIdleState : PlayerParentState
{
    public PlayerCarryMelonIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
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
        
        core.Movement.SmoothDampVelocityXY(player.GetTotalMoveSpeed() * xInput, player.GetTotalMoveSpeed() * yInput, player.GetTotalMoveSmoothing());

        if (!isExitingState) 
        {
            if (xInput != 0 || yInput != 0) 
            {
                stateMachine.ChangeState(player.CarryMelonMoveState);
            } 
        }

        // if (interactButtonPressed && player.GetIsTouchingMelon() && melon == null)
        // {
        //     if (melon != null)
        //     {
        //         melon.EnableMelon();
        //         melon.transform.position = transform.position + new Vector3(0, 1, 0);
        //         this.melon = null;
        //     }

        //     stateMachine.ChangeState(player.CarryMelonIdleState);
        // }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}