using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryMelonMoveState : PlayerParentState
{
    private Vector3             referenceVelocity;

    private Melon melon;
    private Creature creature;

    public PlayerCarryMelonMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        referenceVelocity = Vector3.zero;

        melon       = player.GetCarriedMelon();
        creature    = null;
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
            if (xInput == 0 && yInput == 0) 
            {
                stateMachine.ChangeState(player.CarryMelonIdleState);
            } 
        }

        if (Time.time >= player.GetInteractCooldownStartTime() + player.GetInteractCooldownTime())
        {
            if (interactButtonPressed && melon != null)
            {

                if (player.GetIsTouchingCreature())
                {
                    creature = player.GetTouchedCreature();

                    if (creature != null && creature.GetCanBeFed())
                    {
                        creature.FeedCreature();
                        melon.DestroyMelon();
                        player.SetCarriedMelon(null);
                        player.SetIsCarryingMelon(false);
                        player.StartInteractCooldown();
                        stateMachine.ChangeState(player.MoveState);
                    }
                }
                else
                {
                    melon.EnableMelon();
                    player.SetCarriedMelon(null);
                    player.SetIsCarryingMelon(false);
                    melon.transform.position = player.transform.position + new Vector3(0, 1, 0);
                    player.StartInteractCooldown();
                    stateMachine.ChangeState(player.MoveState);
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
