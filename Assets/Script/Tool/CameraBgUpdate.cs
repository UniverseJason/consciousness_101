using UnityEngine;

namespace Script.Tool
{
    public class CameraBgUpdate : MonoBehaviour
    {
        public Transform FarBackground, MiddleBackground;
        private Vector3 previousCameraLocation;

        private void Start()
        {
            previousCameraLocation = transform.position;
        }

        private void Update()
        {
            Vector2 moveAmount = new Vector2(transform.position.x - previousCameraLocation.x, transform.position.y - previousCameraLocation.y);
            FarBackground.position += new Vector3(moveAmount.x, moveAmount.y, 0);
            MiddleBackground.position += new Vector3(moveAmount.x, moveAmount.y, 0) * 0.5f;
            previousCameraLocation = transform.position;
        }
    }
}
