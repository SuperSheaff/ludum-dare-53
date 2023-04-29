using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerParentState
{
    private Melon melon;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        melon = null;
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
                stateMachine.ChangeState(player.MoveState);
            } 

            if (Time.time >= player.GetInteractCooldownStartTime() + player.GetInteractCooldownTime())
            {
                if (interactButtonPressed && player.GetIsTouchingMelon() && melon == null)
                {
                    melon = player.GetTouchedMelon();

                    if (melon != null)
                    {
                        melon.DisableMelon();
                        player.SetCarriedMelon(melon);
                        player.SetIsCarryingMelon(true);
                        player.StartInteractCooldown();
                        stateMachine.ChangeState(player.CarryMelonIdleState);
                    }
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
