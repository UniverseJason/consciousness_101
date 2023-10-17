using UnityEngine;

namespace Script.ActionSystem
{
    public class CameraFollow : MonoBehaviour
    {
        public bool EnableCameraFollow = true;

        [SerializeField] private Transform _target;
        public Transform Target
        {
            get { return _target; }
            set { _target = value; }
        }

        [SerializeField] private float _smoothSpeed = 0.125f;
        [SerializeField] private Vector2 _offset; // Offset for x and y position

        private void LateUpdate()
        {
            if (EnableCameraFollow)
            {
                Vector3 desiredPosition = new Vector3(_target.position.x + _offset.x, _target.position.y + _offset.y, -10);
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
                transform.position = smoothedPosition;
            }
        }
    }
}