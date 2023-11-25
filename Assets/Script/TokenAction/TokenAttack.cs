using UnityEngine;

namespace Script.TokenAction
{
    public class TokenAttack : MonoBehaviour
    {
        public Vector3 OriginPosition;
        public Vector3 TargetPosition;

        public float AttackRange = 5f;
        public float AttackSpeed = 20f;

        private Camera mainCamera;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void StartAttack()
        {
            OriginPosition = transform.position;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = mainCamera.nearClipPlane;  // Use nearClipPlane distance
            TargetPosition = mainCamera.ScreenToWorldPoint(mousePos);

            Vector3 direction = (TargetPosition - OriginPosition).normalized;
            TargetPosition = OriginPosition + direction * AttackRange;

            TargetPosition.z = 0;
        }

        public void MoveTo(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, AttackSpeed * Time.deltaTime);
        }
    }
}
