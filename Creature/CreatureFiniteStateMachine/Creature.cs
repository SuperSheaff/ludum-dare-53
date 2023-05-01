using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public CreatureDeathState           DeathState          { get; private set; }
        public CreatureMlemState            MlemState           { get; private set; }
        public CreatureCarriedState         CarriedState        { get; private set; }

        [SerializeField]
        private CreatureData creatureData;

        // [SerializeField]
        // public CameraController CameraController;

    #endregion

    #region Components

        [SerializeField]

        public Core                     Core                    { get; private set; }
        public Animator                 creatureAnimator        { get; private set; }
        public Rigidbody2D              creatureRigidBody       { get; private set; }
        public BoxCollider2D            creatureBoxCollider     { get; private set; }
        public CreatureAudioManager     creatureAudioManager    { get; private set; }
        public SpriteRenderer           spriteRenderer          { get; private set; }
        public GameController           gameController          { get; private set; }

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
        public GameObject eggObject;

        private float layingEggCooldownStartTime = -20f;
        private bool canPickupCreature;
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
            DeathState          = new CreatureDeathState(this, StateMachine, creatureData, "creature: death");
            MlemState           = new CreatureMlemState(this, StateMachine, creatureData, "creature: mlem");
            CarriedState        = new CreatureCarriedState(this, StateMachine, creatureData, "creature: mlem");
        }

        private void Start() {
            creatureAnimator        = GetComponent<Animator>();
            creatureRigidBody       = GetComponent<Rigidbody2D>();
            creatureBoxCollider     = GetComponent<BoxCollider2D>();
            creatureAudioManager    = GetComponent<CreatureAudioManager>();
            spriteRenderer          = GetComponent<SpriteRenderer>();
            gameController          = GameObject.FindWithTag("GameController").GetComponent<GameController>();

            SetMood(60f);
            StateMachine.Initialize(EggState);

            referenceVelocity       = Vector2.zero;
            Core.Movement.SetVelocityZero();
            canPickupCreature       = false;
        }

        private void Update() {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();   
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

        public void SetCanPickupCreature(bool value) 
        {
            canPickupCreature = value;
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

        public bool GetCanPickupCreature() 
        {
            return canPickupCreature;
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
            eggObject = Instantiate(CreaturePrefab, transform.position, transform.rotation);
        }

        public void PickupCreature()
        {
            spriteRenderer.enabled  = false;
            creatureBoxCollider.enabled     = false;

            StateMachine.ChangeState(CarriedState);
        }

        public void DropCreature()
        {
            spriteRenderer.enabled = true;
            creatureBoxCollider.enabled = true;

            StateMachine.ChangeState(IdleState);
        }

        public void DestroySelf()
        {
            Destroy(this.gameObject);
        }

    #endregion

}
