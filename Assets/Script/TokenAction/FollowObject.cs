using UnityEngine;
namespace Script.TokenAction
{
    public class FollowObject : MonoBehaviour
    {
        public Transform Target;

        public float followSpeed = 1.5f;
        public float stoppingDistance = 0.7f;

        public float offsetX = 0f; // Offset in the X direction
        public float offsetY = 0.3f; // Offset in the Y direction

        public bool enableVerticalMovementOnly = false;
        public bool enableHorizontalMovementOnly = false;

        public void FollowTarget()
        {
            if(Target != null)
            {
                // If vertical-only movement is enabled, keep the current x position.
                float targetX = enableVerticalMovementOnly ? transform.position.x : Target.position.x + offsetX;

                // If horizontal-only movement is enabled, keep the current y position.
                float targetY = enableHorizontalMovementOnly ? transform.position.y : Target.position.y + offsetY;

                Vector3 desiredPosition = new Vector3(targetX, targetY, 0);

                float distance = Vector3.Distance(transform.position, desiredPosition);
                if (distance < stoppingDistance) return;

                transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            }
        }
    }
}