using Script.Animation;
using UnityEngine;

namespace Script.ActionSystem
{
    public class Movement : MonoBehaviour
    {
        public float moveSpeed { get; set; }
        public float jumpForce { get; set; }
        public LayerMask groundMask;
        public Transform body;
        public AnimationStateChanger animationStateChanger;
        public string animationDefaultStateName = "null";
        public string animationMoveStateName = "null";
        public string animationRunStateName = "null";
        private Rigidbody2D rb2d;

        public void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        public void MoveRb(Vector3 v3, bool enableAnimation = true)
        {
            v3 *= moveSpeed;
            v3.y = rb2d.velocity.y;
            rb2d.velocity = v3;

            // flip body when going back
            if (v3.x > 0)
            {
                body.localScale = new Vector3(2, 2, 0);
            }
            else if (v3.x < 0)
            {
                body.localScale = new Vector3(-2, 2, 0);
            }

            if (enableAnimation && moveSpeed > 2f)
            {
                animationStateChanger.ChangeAnimationState(animationRunStateName);
            }
            else if (enableAnimation && v3.magnitude > 0)
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
            if(Physics2D.OverlapCircleAll(transform.position-new Vector3(0,.5f,0),0.5f,groundMask).Length > 0){
                rb2d.AddForce(new Vector3(0, jumpForce, 0), ForceMode2D.Impulse);
            }
        }
    }
}
