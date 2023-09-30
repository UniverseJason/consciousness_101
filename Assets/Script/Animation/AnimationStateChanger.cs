using Unity.VisualScripting;
using UnityEngine;

namespace Script.Animation
{
    public class AnimationStateChanger : MonoBehaviour
    {
        public Animator animator;
        public string animationCurrentState;

        public void ChangeAnimationState(string newState)
        {
            if (newState == animationCurrentState)
            {
                return;
            }

            animationCurrentState = newState;
            animator.Play(animationCurrentState);
        }
    }
}
