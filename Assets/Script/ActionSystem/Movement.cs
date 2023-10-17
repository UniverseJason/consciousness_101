using Script.Animation;
using Script.InputControl;
using UnityEngine;

namespace Script.ActionSystem
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

        [SerializeField] private float runSpeed;
        public float RunSpeed
        {
            get { return runSpeed; }
            set { moveSpeed = value; }
        }

        // movement animation state
        public bool EnableAnimation = true;
        [SerializeField] private AnimationStateChanger animationStateChanger;
        [SerializeField] private string animationDefaultStateName = "null";
        [SerializeField] private string animationMoveStateName = "null";
        [SerializeField] private string animationRunStateName = "null";

        // control the Rigidbody2D movement
        private Rigidbody2D rb2d;

        // used to flip the body based on movement direction
        private Vector3 originalLocalScale;

        private TransformInput _input;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            _input = GetComponent<TransformInput>();
            originalLocalScale = transform.localScale;
        }

        // move the Rigidbody2D
        public void MoveRb(Vector3 v3, bool isRunning)
        {
            if (isRunning)
                v3 *= runSpeed;
            else
                v3 *= moveSpeed;

            if (!_input.EnableVerticalMovement) v3.y = rb2d.velocity.y;

            rb2d.velocity = v3;

            // Flip body based on movement direction
            if (v3.x != 0)
            {
                float direction = Mathf.Sign(v3.x);
                Vector3 newScale = new Vector3(originalLocalScale.x * direction, originalLocalScale.y, originalLocalScale.z);
                transform.localScale = newScale;
            }

            // Handle move and run animations
            HandleMoveAnimations(v3, isRunning);
        }

        private void HandleMoveAnimations(Vector3 v3, bool isRunning)
        {
            if (!EnableAnimation) return;

            if (isRunning)
            {
                animationStateChanger.ChangeAnimationState(animationRunStateName);
            }
            else if (v3.magnitude > 0)
            {
                animationStateChanger.ChangeAnimationState(animationMoveStateName);
            }
            else
            {
                animationStateChanger.ChangeAnimationState(animationDefaultStateName);
            }
        }
    }
}
