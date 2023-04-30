using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureParentState : CreatureState
{
    private float localMood;

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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        localMood = creature.GetMood();

        if (Time.time >= creature.GetLayingEggCooldownStartTime() + creatureData.layingEggCooldownTime)
        {
            if ((Random.value < creatureData.eggLayChance && localMood > 50f)) // Check if the creature lays an egg
            {
                stateMachine.ChangeState(creature.LayingEggState);
            }
        }

        if (localMood == 0) 
        {
            stateMachine.ChangeState(creature.PreScremState);
        }

        // Decrease the mood by moodDrainRate per second
        localMood -= creatureData.moodDrainRate * Time.deltaTime;

        // Ensure mood doesn't go negative
        localMood = Mathf.Max(0f, localMood);

        creature.SetMood(localMood);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
