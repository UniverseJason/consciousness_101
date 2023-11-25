using UnityEngine;
namespace Script.TokenAction
{
    public class FollowObject : MonoBehaviour
    {
        public Transform Target;

        public float _followSpeed = 1.5f;
        public float _stoppingDistance = 0.7f;

        public float _offsetX = 0f; // Offset in the X direction
        public float _offsetY = 0.3f; // Offset in the Y direction

        public void FollowTarget()
        {
            if(Target != null)
            {
                Vector3 desiredPosition = Target.position + new Vector3(_offsetX, _offsetY, 0);
                float distance = Vector3.Distance(transform.position, desiredPosition);

                if(distance > _stoppingDistance)
                {
                    transform.position = Vector3.Lerp(transform.position, desiredPosition, _followSpeed * Time.deltaTime);
                }
            }
        }
    }
}