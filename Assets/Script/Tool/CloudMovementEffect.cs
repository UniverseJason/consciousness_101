using UnityEngine;

namespace Script.Tool
{
    public class CloudMovementEffect : MonoBehaviour
    {
        public float floatStrength = 1f;
        public float floatSpeed = 0.5f;

        private float originalY;

        void Start()
        {
            originalY = transform.position.y;
        }

        void Update()
        {
            // Calculate the new Y position
            float newY = originalY + (Mathf.Sin(Time.time * floatSpeed) * floatStrength);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}