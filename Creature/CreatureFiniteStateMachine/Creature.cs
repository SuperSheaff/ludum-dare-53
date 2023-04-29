using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{

    #region State Variables

        public CreatureStateMachine         StateMachine        { get; private set; }

        public CreatureIdleState            IdleState           { get; private set; }
        public CreatureMoveState            MoveState           { get; private set; }
        public CreaturePreScremState        PreScremState       { get; private set; }
        public CreatureScremState           ScremState          { get; private set; }
        public CreatureEatState             EatState            { get; private set; }
        public CreatureEggState             EggState            { get; private set; }
        public CreatureLayingEggState       LayingEggState      { get; private set; }

        [SerializeField]
        private CreatureData creatureData;

        [SerializeField]
        public GameController gameController;

        [SerializeField]
        public CameraController CameraController;

    #endregion

    #region Components

        [SerializeField]

        public Core                     Core                    { get; private set; }
        public Animator                 creatureAnimator        { get; private set; }
        public Rigidbody2D              creatureRigidBody       { get; private set; }
        public BoxCollider2D            creatureBoxCollider     { get; private set; }
        public CreatureAudioManager     creatureAudioManager    { get; private set; }

    #endregion

    #region Movement Related Variables

        private Vector2 randomMoveLocation;

    #endregion

    #region Mood Related Variables

        private float mood;

    #endregion

    #region Other Variables

        public GameObject CreaturePrefab;
        public GameObject CreaturesContainer;
        private float layingEggCooldownStartTime = -20f;
        private Vector2 workspace;
        private Vector2 referenceVelocity;

    #endregion

    #region Unity Callback Functions

        private void Awake() {

            Core = GetComponentInChildren<Core>();

            StateMachine        = new CreatureStateMachine();

            IdleState           = new CreatureIdleState(this, StateMachine, creatureData, "creature: idle");
            MoveState           = new CreatureMoveState(this, StateMachine, creatureData, "creature: move");
            PreScremState       = new CreaturePreScremState(this, StateMachine, creatureData, "creature: pre-screm");
            ScremState          = new CreatureScremState(this, StateMachine, creatureData, "creature: screm");
            EatState            = new CreatureEatState(this, StateMachine, creatureData, "creature: eat");
            EggState            = new CreatureEggState(this, StateMachine, creatureData, "creature: egg");
            LayingEggState      = new CreatureLayingEggState(this, StateMachine, creatureData, "creature: laying-egg");
        }

        private void Start() {
            creatureAnimator      = GetComponent<Animator>();
            creatureRigidBody     = GetComponent<Rigidbody2D>();
            creatureBoxCollider   = GetComponent<BoxCollider2D>();
            creatureAudioManager  = GetComponent<CreatureAudioManager>();

            StateMachine.Initialize(IdleState);
            SetMood(60f);

            referenceVelocity       = Vector2.zero;
            Core.Movement.SetVelocityZero();
        }

        private void Update() {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();   

            if (Time.time >= GetLayingEggCooldownStartTime() + creatureData.layingEggCooldownTime)
            {
                if ((Random.value < creatureData.eggLayChance && mood > 50f)) // Check if the creature lays an egg
                {
                    StateMachine.ChangeState(LayingEggState);
                }
            }

            Debug.Log("Creature mood:" + mood);
            if (mood == 0 && StateMachine.CurrentState != PreScremState && StateMachine.CurrentState != ScremState) 
            {
                StateMachine.ChangeState(PreScremState);
            }

            // Decrease the mood by moodDrainRate per second
            mood -= creatureData.moodDrainRate * Time.deltaTime;

            // Ensure mood doesn't go negative
            mood = Mathf.Max(0f, mood);
        }

        private void FixedUpdate() {
            StateMachine.CurrentState.PhysicsUpdate();    
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
          
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
           
        }

    #endregion

    #region Set Functions

        public void SetRandomMoveLocation() 
        {
            float xPos = Random.Range(transform.position.x - GetMoveRange(), transform.position.x + GetMoveRange());
            float yPos = Random.Range(transform.position.y - GetMoveRange(), transform.position.y + GetMoveRange());
            randomMoveLocation = new Vector2(xPos, yPos);
        }

        public void SetMood(float value) 
        {
            mood = value;
        }

    #endregion

    #region Get Functions

        public float GetMood()
        {
            return mood;
        }

        public float GetLayingEggCooldownStartTime()
        {
            return layingEggCooldownStartTime;
        }

        public float GetTotalMoveSpeed()
        {
            return creatureData.baseMoveSpeed;
        }

        public float GetTotalMoveSmoothing()
        {
            return creatureData.baseMoveSmoothing;
        }

        public float GetMoveRange()
        {
            return creatureData.baseMoveRange;
        }

        public Vector2 GetRandomMoveLocation()
        {
            return randomMoveLocation;
        }

    #endregion

    #region Trigger Functions

        private void AnimationTrigger()                 => StateMachine.CurrentState.AnimationTrigger();
        private void AnimationFinishedTrigger()         => StateMachine.CurrentState.AnimationFinishedTrigger();
        private void AnimationStartMovementTrigger()    => StateMachine.CurrentState.AnimationStartMovementTrigger();
        private void AnimationStopMovementTrigger()     => StateMachine.CurrentState.AnimationStopMovementTrigger();
        private void AnimationTurnOffFlip()             => StateMachine.CurrentState.AnimationTurnOffFlip();
        private void AnimationTurnOnFlip()              => StateMachine.CurrentState.AnimationTurnOnFlip();
        private void AnimationActionTrigger()           => StateMachine.CurrentState.AnimationActionTrigger();

    #endregion
    
    
    #region Other Functions

        public void StartLayingEggCooldown()
        {
            layingEggCooldownStartTime = Time.time;
        }

        public void FeedCreature()
        {
            StateMachine.ChangeState(EatState);
        }

        public void LayEgg()
        {
            Debug.Log("Reach 1");
            GameObject eggObject = Instantiate(CreaturePrefab, transform.position, Quaternion.identity, CreaturesContainer.transform);
            Debug.Log("Reach 2");
            Creature eggCreature = eggObject.GetComponent<Creature>();
            Debug.Log("Reach 3");
        }

    #endregion

}
