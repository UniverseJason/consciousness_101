using UnityEngine;

namespace Script.ActionSystem
{
    public class FollowObject : MonoBehaviour
    {
        public Transform Target;

        [SerializeField] private float _followSpeed = 1f;
        [SerializeField] private float _stoppingDistance = 0.7f;

        [SerializeField] private float _offsetX = 0f; // Offset in the X direction
        [SerializeField] private float _offsetY = 0.3f; // Offset in the Y direction

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