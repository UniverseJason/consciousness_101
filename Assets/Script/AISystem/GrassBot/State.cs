using System;
using System.Collections;
using Script.Animation;
using Script.Music;
using Script.TokenAction;
using UnityEngine;

namespace Script.AISystem.GrassBot
{
    public class State : MonoBehaviour
    {
        public enum AIState
        {
            Idle,
            Attack1,
            Attack2,
            Movement,
            WarningUp,
            WarningDown,
        }

        [Header("AI Management")]
        public AIState currentState = AIState.Idle;
        public ParticleManagement ParticleManagement;

        [Header("Animation Control")]
        public AnimationStateChanger AnimationStateChanger;
        [SerializeField] private string idleStateName = "Boss Idle";
        [SerializeField] private string warningStateName = "Boss Warning";
        [SerializeField] private string readyBloomStateName = "Boss Attack Ready toBloom";
        [SerializeField] private string bloomStateName = "Boss Attack Bloom";

        [Header("Camera Adjustment")]
        public Camera cam;
        public float defaultCameraSize;
        public float cameraZoomSpeed = 1f;

        [Header("Movement")]
        public Transform CurrentTarget;
        public FollowObject followObject;

        [Header("Attack 1")]
        [SerializeField] private float attack1Interval = 15f;
        private bool isAttack1RoutineStart = false;

        [Header("Attack 2")]
        [SerializeField] private float attack2Interval = 15f;
        private bool isAttack2RoutineStart = false;

        // ready to bloom can only run once
        private bool isReadyRun = false;
        private bool isReadyFinish = false;

        // Coroutine Management
        private Coroutine attack1Routine = null;
        private Coroutine attack2PrepRoutine = null;
        private Coroutine attack2Routine = null;

        // Background Music
        private AudioManager audioManager;

        private void Start()
        {
            cam = Camera.main;
            defaultCameraSize = cam.orthographicSize;
            AnimationStateChanger = GetComponent<AnimationStateChanger>();
            ParticleManagement = GetComponent<ParticleManagement>();
            followObject = GetComponent<FollowObject>();
            audioManager = GetComponent<AudioManager>();
            ParticleManagement.StopAllParticle();
            ParticleManagement.IdleParticle.Play();
        }

        private void Update()
        {
            switch (currentState)
            {
                case AIState.Idle:
                    Idle();
                    break;
                case AIState.Attack1:
                    audioManager.SmoothChangeTo("FightV");
                    if (!isAttack1RoutineStart) attack1Routine = StartCoroutine(Attack1Routine());
                    break;
                case AIState.Attack2:
                    audioManager.SmoothChangeTo("FightV");
                    StopCoroutine(attack1Routine);
                    if (!isReadyRun) attack2PrepRoutine = StartCoroutine(Attack2PrepRoutine());
                    if (!isAttack2RoutineStart && isReadyFinish)
                    {
                        StopCoroutine(attack2PrepRoutine);
                        attack2Routine = StartCoroutine(Attack2Routine());
                    }
                    break;
                case AIState.Movement:
                    Movement();
                    break;
                case AIState.WarningUp:
                    WarningUp();
                    break;
                case AIState.WarningDown:
                    WarningDown();
                    break;
                default:
                    Idle();
                    break;
            }
        }

        private IEnumerator Attack1Routine()
        {
            isAttack1RoutineStart = true;
            yield return new WaitForSeconds(attack1Interval);
            ParticleManagement.SwitchToParticle(ParticleManagement.Attack1Particle);
            isAttack1RoutineStart = false;
        }

        private IEnumerator Attack2PrepRoutine()
        {
            isReadyRun = true;
            ParticleManagement.SwitchToParticle(ParticleManagement.IdleParticle);
            AnimationStateChanger.ChangeAnimationState(readyBloomStateName);
            yield return new WaitForSeconds(5f);
            isReadyFinish = true;
        }

        private IEnumerator Attack2Routine()
        {
            isAttack2RoutineStart = true;
            AnimationStateChanger.ChangeAnimationState(bloomStateName);
            yield return new WaitForSeconds(attack2Interval);
            isAttack2RoutineStart = false;
        }

        // Called in the animation
        public void PlayAttack2Particle()
        {
            ParticleManagement.SwitchToParticle(ParticleManagement.Attack2Particle);
        }

        private void Idle()
        {
            // Camera adjustment to zoom in
            CameraZoomBetween(cam.orthographicSize, defaultCameraSize, cameraZoomSpeed);

            ParticleManagement.PlayParticle(ParticleManagement.IdleParticle);
            AnimationStateChanger.ChangeAnimationState(idleStateName);
        }

        private void Movement()
        {
            followObject.Target = CurrentTarget;
            followObject.FollowTarget();
        }

        private void WarningUp()
        {
            AnimationStateChanger.ChangeAnimationState(warningStateName);

            ParticleManagement.SwitchToParticle(ParticleManagement.MoveParticle);

            // Camera adjustment to zoom out
            CameraZoomBetween(cam.orthographicSize, defaultCameraSize + 0.5f, cameraZoomSpeed);

            audioManager.SmoothChangeTo("DarkV");
        }

        private void WarningDown()
        {
            AnimationStateChanger.ChangeAnimationState(idleStateName);

            ParticleManagement.SwitchToParticle(ParticleManagement.IdleParticle);

            // Camera adjustment to zoom in
            CameraZoomBetween(cam.orthographicSize, defaultCameraSize, cameraZoomSpeed);

            audioManager.SmoothChangeTo("BasicV");
        }

        private void CameraZoomBetween(float from, float to, float speed)
        {
            // Check if the difference is within the allowed threshold
            if (Mathf.Abs(cam.orthographicSize - to) < 0.01f) return;

            cam.orthographicSize = Mathf.Lerp(from, to, Time.deltaTime * speed);
        }
    }
}