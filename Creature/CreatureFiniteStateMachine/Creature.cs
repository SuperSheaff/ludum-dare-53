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

    #region Movement Related Variables

        private Vector2 randomMoveLocation;

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

            IdleState           = new CreatureIdleState(this, StateMachine, creatureData, "creature: idle");
            MoveState           = new CreatureMoveState(this, StateMachine, creatureData, "creature: move");
        }

        private void Start() {
            creatureAnimator      = GetComponent<Animator>();
            creatureRigidBody     = GetComponent<Rigidbody2D>();
            creatureBoxCollider   = GetComponent<BoxCollider2D>();
            creatureAudioManager  = GetComponent<CreatureAudioManager>();

            StateMachine.Initialize(IdleState);

            referenceVelocity       = Vector2.zero;
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

    #endregion

    #region Get Functions

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
