using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerParentState
{
    private Vector3             referenceVelocity;

    private Melon melon;
    private Creature creature;

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animatorBoolName) : base(player, stateMachine, playerData, animatorBoolName)
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

        melon       = null;
        creature    = null;

        player.playerAudioManager.PlaySound("move");
    }

    public override void Exit()
    {
        base.Exit();

        player.playerAudioManager.StopSound("move");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        core.Movement.SmoothDampVelocityXY(player.GetTotalMoveSpeed() * xInput, player.GetTotalMoveSpeed() * yInput, player.GetTotalMoveSmoothing());

        if (!isExitingState) 
        {
            if (xInput == 0 && yInput == 0) 
            {
                stateMachine.ChangeState(player.IdleState);
            } 

            if (Time.time >= player.GetInteractCooldownStartTime() + player.GetInteractCooldownTime())
            {
                if (interactButtonPressed)
                {
                    if (player.GetIsTouchingCreature() && creature == null)
                    {
                        creature = player.GetTouchedCreature();

                        if (creature != null && creature.GetCanPickupCreature())
                        {
                            creature.PickupCreature();
                            player.SetCarriedCreature(creature);
                            player.SetIsCarryingCreature(true);
                            player.StartInteractCooldown();
                            player.playerAudioManager.PlaySound("pickup");
                            stateMachine.ChangeState(player.CarryCreatureMoveState);
                        }
                    }
                }
            }
            if (Time.time >= player.GetInteractCooldownStartTime() + player.GetInteractCooldownTime())
            {
                if (interactButtonPressed)
                {
                    if (player.GetIsTouchingMelon() && melon == null)
                    {
                        melon = player.GetTouchedMelon();

                        if (melon != null)
                        {
                            melon.DisableMelon();
                            player.SetCarriedMelon(melon);
                            player.SetIsCarryingMelon(true);
                            player.StartInteractCooldown();
                            player.playerAudioManager.PlaySound("pickup");
                            stateMachine.ChangeState(player.CarryMelonMoveState);
                        }
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
