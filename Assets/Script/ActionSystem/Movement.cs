/*
 * This class implement the character move, running, and jumping
 * with corresponding animation
 */

using Script.Animation;
using UnityEngine;

namespace Script.ActionSystem
{
    public class Movement : MonoBehaviour
    {
        // player movement management
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float runSpeed = 4f;

        // player jump management
        [SerializeField] private float jumpForce = 13f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -0.5f, 0);
        [SerializeField] private float groundCheckRadius = 0.5f;

        // movement animation state
        [SerializeField] private Transform body;
        [SerializeField] private AnimationStateChanger animationStateChanger;
        [SerializeField] private string animationDefaultStateName = "null";
        [SerializeField] private string animationMoveStateName = "null";
        [SerializeField] private string animationRunStateName = "null";

        private Rigidbody2D rb2d;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        public void MoveRb(Vector3 v3, bool enableAnimation, bool isRunning)
        {
            if (isRunning)
                v3 *= runSpeed;
            else
                v3 *= moveSpeed;

            v3.y = rb2d.velocity.y;
            rb2d.velocity = v3;

            // Flip body based on movement direction
            if (v3.x != 0)
            {
                float direction = Mathf.Sign(v3.x);
                body.localScale = new Vector3(2 * direction, 2, 0);
            }

            // Handle move and run animations
            HandleMoveAnimations(v3, enableAnimation, isRunning);
        }

        private void HandleMoveAnimations(Vector3 v3, bool enableAnimation, bool isRunning)
        {
            if (!enableAnimation)
            {
                animationStateChanger.ChangeAnimationState(animationDefaultStateName);
                return;
            }

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

        public void JumpRB()
        {
            if (IsGrounded())
            {
                rb2d.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircleAll(transform.position + groundCheckOffset, groundCheckRadius, groundMask).Length > 0;
        }
    }
}
