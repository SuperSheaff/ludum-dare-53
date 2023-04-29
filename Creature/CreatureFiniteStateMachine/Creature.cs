using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{

    #region State Variables

        public CreatureStateMachine           StateMachine        { get; private set; }

        public CreatureIdleState              IdleState           { get; private set; }
        public CreatureMoveState              MoveState           { get; private set; }

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

    #region Other Variables

        private float interactCooldownStartTime = 0f;
        private Vector2 workspace;
        private Vector2 referenceVelocity;

    #endregion

    #region Unity Callback Functions

        private void Awake() {

            Core = GetComponentInChildren<Core>();

            StateMachine        = new CreatureStateMachine();

            IdleState           = new CreatureIdleState(this, StateMachine, creatureData, "idle");
            MoveState           = new CreatureMoveState(this, StateMachine, creatureData, "move");
        }

        private void Start() {
            creatureAnimator      = GetComponent<Animator>();
            creatureRigidBody     = GetComponent<Rigidbody2D>();
            creatureBoxCollider   = GetComponent<BoxCollider2D>();
            creatureAudioManager  = GetComponent<CreatureAudioManager>();

            StateMachine.Initialize(IdleState);

            referenceVelocity       = Vector2.zero;
            RespawnPoint.position   = StartingSpawn.position;
            Core.Movement.SetVelocityZero();
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
            if (collision.CompareTag("Melon"))
            {
                isTouchingMelon = true;
                touchedMelon    = collision.gameObject.GetComponent<Melon>();
                Debug.Log("touching melon");
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Melon"))
            {
                isTouchingMelon = false;
                touchedMelon    = null;
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

        public float GetTotalMoveSpeed()
        {
            return creatureData.baseMoveSpeed;
        }

        public float GetTotalMoveSmoothing()
        {
            return creatureData.baseMoveSmoothing;
        }

        public float GetInteractCooldownTime() 
        {
            return creatureData.InteractCooldownTime;
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
