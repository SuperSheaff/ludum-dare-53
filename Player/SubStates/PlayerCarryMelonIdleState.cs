using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryMelonIdleState : PlayerParentState
{
    private Melon melon;

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

        melon = player.GetCarriedMelon();
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

        if (interactButtonPressed)
        {
            if (melon != null)
            {
                melon.DisableMelon();
                player.SetCarriedMelon(melon);
                player.SetIsCarryingMelon(true);
                stateMachine.ChangeState(player.CarryMelonIdleState);
            }

            if (melon != null)
            {
                melon.EnableMelon();
                player.SetCarriedMelon(null);
                player.SetIsCarryingMelon(false);
                melon.transform.position = player.transform.position + new Vector3(0, 1, 0);
                stateMachine.ChangeState(player.IdleState);
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
