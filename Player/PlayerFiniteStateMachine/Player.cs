using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    #region State Variables

        public PlayerStateMachine       StateMachine        { get; private set; }

        public PlayerIdleState          IdleState           { get; private set; }
        public PlayerMoveState          MoveState           { get; private set; }

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

    #region Other Variables

        private Vector2 workspace;
        private Vector2 referenceVelocity;

    #endregion

    #region Unity Callback Functions

        private void Awake() {

            Core = GetComponentInChildren<Core>();

            StateMachine        = new PlayerStateMachine();

            IdleState           = new PlayerIdleState(this, StateMachine, playerData, "idle");
            MoveState           = new PlayerMoveState(this, StateMachine, playerData, "move");

        }

        private void Start() {
            playerAnimator      = GetComponent<Animator>();
            InputHandler        = GetComponent<PlayerInputHandler>();
            playerRigidBody     = GetComponent<Rigidbody2D>();
            playerBoxCollider   = GetComponent<BoxCollider2D>();
            playerAudioManager  = GetComponent<PlayerAudioManager>();

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

        private void OnCollisionEnter2D(Collision2D collision) 
        {
            // if (collision.gameObject.tag == "MegaJug")
            // {
            //     if (!caughtMegaJug && hasNet)
            //     {
            //         catchMegaJug();
            //     }
            // }

            // if (collision.gameObject.tag == "Ground")
            // {
            //     string soundName = "oof" + Random.Range(1, 3).ToString();
            //     gameController.AudioManager.PlaySound(soundName);
            // }
        }

        private void OnTriggerStay2D(Collider2D collision) {
        }

        private void OnTriggerExit2D(Collider2D collision) {
        }

    #endregion

    #region Set Functions

        // public void SetDrownFlag(bool value)
        // {
        //     startedDrowning = value;
        // }

    #endregion

    #region Get Functions

        public float GetTotalMoveSpeed()
        {
            return playerData.baseMoveSpeed;
        }

        public float GetTotalMoveSmoothing()
        {
            return playerData.baseMoveSmoothing;
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

        // public void StartOxygenTimer()
        // {
        //     oxygenStartTime = Time.time;
        // }

    #endregion

}
