using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureParentState : CreatureState
{
    private float localMood;
    private float eggLayChanceIntervalStartTime;

    public CreatureParentState(Creature creature, CreatureStateMachine stateMachine, CreatureData creatureData, string animatorBoolName) : base(creature, stateMachine, creatureData, animatorBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        
        creature.SetCanBeFed(true);
    }

    public override void Exit()
    {
        base.Exit();

        creature.SetCanBeFed(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        localMood = creature.GetMood();

        if (Time.time >= creature.GetLayingEggCooldownStartTime() + creatureData.layingEggCooldownTime)
        {
            if (Time.time >= eggLayChanceIntervalStartTime + creatureData.eggLayChanceInterval)
            {
                TryLayEgg();
            }
        }

        if (!isExitingState) 
        {
            if (localMood == 0) 
            {
                stateMachine.ChangeState(creature.PreScremState);
            }
            else if (localMood >= 100) 
            {
                stateMachine.ChangeState(creature.MlemState);
            }
        }


        // Decrease the mood by moodDrainRate per second
        localMood -= creatureData.moodDrainRate * Time.deltaTime;

        // Ensure mood doesn't go negative
        localMood = Mathf.Max(0f, localMood);

        creature.SetMood(localMood);
        creature.creatureAnimator.SetFloat("mood", localMood / 100);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void TryLayEgg()
    {
        if ((Random.value < creatureData.eggLayChance && localMood > 50f)) // Check if the creature lays an egg
        {
            stateMachine.ChangeState(creature.LayingEggState);
        }

        StartEggLayChanceInterval();
    }

    public void StartEggLayChanceInterval()
    {
        eggLayChanceIntervalStartTime = Time.time;
    }
}
