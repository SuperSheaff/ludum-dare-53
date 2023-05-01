using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarryCreatureIdleState : PlayerParentState
{
    private Creature creature;

    public PlayerCarryCreatureIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        creature = player.GetCarriedCreature();
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
                stateMachine.ChangeState(player.CarryCreatureMoveState);
            } 
        }

        if (Time.time >= player.GetInteractCooldownStartTime() + player.GetInteractCooldownTime())
        {
            if (interactButtonPressed && creature != null)
            {

                if (player.GetIsTouchingDelivery())
                {
                    Debug.Log("Deliver Creature");

                    if (creature != null)
                    {
                        player.gameController.CreatureDelivered();
                        creature.DestroySelf();
                        player.SetCarriedCreature(null);
                        player.SetIsCarryingCreature(false);
                        player.StartInteractCooldown();
                        stateMachine.ChangeState(player.IdleState);
                    }
                }
                else
                {
                    creature.DropCreature();
                    player.SetCarriedCreature(null);
                    player.SetIsCarryingCreature(false);
                    creature.transform.position = player.transform.position + new Vector3(0, 1, 0);
                    player.StartInteractCooldown();
                    stateMachine.ChangeState(player.IdleState);
                }
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
