using Script.Animation;
using UnityEngine;

namespace Script.ActionSystem
{
    public class Jump : MonoBehaviour
    {
        // player jump management
        [SerializeField] private float jumpForce;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Vector3 groundCheckOffset = new Vector3(0, -0.5f, 0);
        [SerializeField] private float groundCheckRadius = 0.5f;

        private Rigidbody2D rb2d;

        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircleAll(transform.position + groundCheckOffset, groundCheckRadius, groundMask).Length > 0;
        }

        public void JumpRB()
        {
            if (IsGrounded())
            {
                rb2d.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
                HandleJumpAnimation();
            }
        }

        public void HandleJumpAnimation()
        {
            // TODO: Add jump animation!
        }
    }
}
