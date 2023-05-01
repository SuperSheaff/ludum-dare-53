using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    #region State Variables

        public PlayerStateMachine               StateMachine            { get; private set; }

        public PlayerIdleState                  IdleState               { get; private set; }
        public PlayerMoveState                  MoveState               { get; private set; }
        public PlayerCarryMelonIdleState        CarryMelonIdleState     { get; private set; }
        public PlayerCarryMelonMoveState        CarryMelonMoveState     { get; private set; }
        public PlayerCarryCreatureIdleState     CarryCreatureIdleState  { get; private set; }
        public PlayerCarryCreatureMoveState     CarryCreatureMoveState  { get; private set; }

        [SerializeField]
        private PlayerData playerData;

        [SerializeField]
        public GameController gameController;

        [SerializeField]
        public CameraController CameraController;

    #endregion

    #region Components

        [SerializeField]

        public Core                 Core                { get; private set; }
        public Animator             playerAnimator      { get; private set; }
        public PlayerInputHandler   InputHandler        { get; private set; }
        public Rigidbody2D          playerRigidBody     { get; private set; }
        public BoxCollider2D        playerBoxCollider   { get; private set; }
        public PlayerAudioManager   playerAudioManager  { get; private set; }

    #endregion

    #region Check Transforms

        [SerializeField]
        public Transform StartingSpawn;

        [SerializeField]
        public Transform RespawnPoint;

    #endregion

    #region Melon Related Variables

        private Melon touchedMelon;
        private Melon carriedMelon;
        private bool isTouchingMelon;
        private bool isCarryingMelon;

    #endregion

    #region Creature Related Variables

        private Creature touchedCreature;
        private Creature carriedCreature;
        private bool isTouchingCreature;
        private bool isCarryingCreature;

    #endregion

    #region Delivery Related Variables

        private bool isTouchingDelivery;

    #endregion

    #region Other Variables

        private float interactCooldownStartTime = 0f;

        private Vector2 workspace;
        private Vector2 referenceVelocity;

    #endregion

    #region Unity Callback Functions

        private void Awake() 
        {

            Core = GetComponentInChildren<Core>();

            StateMachine            = new PlayerStateMachine();

            IdleState               = new PlayerIdleState(this, StateMachine, playerData, "idle");
            MoveState               = new PlayerMoveState(this, StateMachine, playerData, "move");
            CarryMelonIdleState     = new PlayerCarryMelonIdleState(this, StateMachine, playerData, "carryMelonIdle");
            CarryMelonMoveState     = new PlayerCarryMelonMoveState(this, StateMachine, playerData, "carryMelonMove");
            CarryCreatureIdleState  = new PlayerCarryCreatureIdleState(this, StateMachine, playerData, "carryCreatureIdle");
            CarryCreatureMoveState  = new PlayerCarryCreatureMoveState(this, StateMachine, playerData, "carryCreatureMove");
        }

        private void Start() 
        {
            playerAnimator      = GetComponent<Animator>();
            InputHandler        = GetComponent<PlayerInputHandler>();
            playerRigidBody     = GetComponent<Rigidbody2D>();
            playerBoxCollider   = GetComponent<BoxCollider2D>();
            playerAudioManager  = GetComponent<PlayerAudioManager>();

            StateMachine.Initialize(IdleState);

            referenceVelocity       = Vector2.zero;
            Core.Movement.SetVelocityZero();
        }

        private void Update() 
        {
            Core.LogicUpdate();
            StateMachine.CurrentState.LogicUpdate();   
        }

        private void FixedUpdate() 
        {
            StateMachine.CurrentState.PhysicsUpdate();    
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Melon"))
            {
                isTouchingMelon = true;
                touchedMelon    = collision.gameObject.GetComponent<Melon>();
                Debug.Log("touching melon");
            }

            if (collision.CompareTag("Creature"))
            {
                isTouchingCreature = true;
                touchedCreature    = collision.gameObject.GetComponent<Creature>();
                Debug.Log("touching creature");
            }

            if (collision.CompareTag("Delivery"))
            {
                isTouchingDelivery = true;
                Debug.Log("touching Delivery");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Melon"))
            {
                isTouchingMelon = false;
                touchedMelon    = null;
            }

             if (collision.CompareTag("Creature"))
            {
                isTouchingCreature = false;
                touchedCreature    = null;
            }

            if (collision.CompareTag("Delivery"))
            {
                isTouchingDelivery = false;
            }
        }

    #endregion

    #region Set Functions

        public void SetCarriedMelon(Melon melon)
        {
            carriedMelon = melon;
        }

        public void SetIsCarryingMelon(bool value)
        {
            isCarryingMelon = value;
        }

        public void SetCarriedCreature(Creature creature)
        {
            carriedCreature = creature;
        }

        public void SetIsCarryingCreature(bool value)
        {
            isCarryingCreature = value;
        }

    #endregion

    #region Get Functions

        public bool GetIsTouchingMelon() 
        {
            return isTouchingMelon;
        }

        public Melon GetTouchedMelon() 
        {
            return touchedMelon;
        }

        public bool GetIsCarryingMelon() 
        {
            return isCarryingMelon;
        }

        public Melon GetCarriedMelon() 
        {
            return carriedMelon;
        }

        public bool GetIsCarryingCreature() 
        {
            return isCarryingCreature;
        }

        public Creature GetCarriedCreature() 
        {
            return carriedCreature;
        }

        public bool GetIsTouchingCreature() 
        {
            return isTouchingCreature;
        }           

        public Creature GetTouchedCreature() 
        {
            return touchedCreature;
        }

        public bool GetIsTouchingDelivery() 
        {
            return isTouchingDelivery;
        }

        public float GetTotalMoveSpeed()
        {
            return playerData.baseMoveSpeed;
        }

        public float GetTotalMoveSmoothing()
        {
            return playerData.baseMoveSmoothing;
        }

        public float GetInteractCooldownTime() 
        {
            return playerData.InteractCooldownTime;
        }

        public float GetInteractCooldownStartTime() 
        {
            return interactCooldownStartTime;
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

        // public void ResetGame() {
        //     Core.Movement.checkIfShouldFlip(1);
        //     SetSpawnPoint(StartingSpawn.position);
        // }

        public void StartInteractCooldown()
        {
            interactCooldownStartTime = Time.time;
        }

    #endregion

}
