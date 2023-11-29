using UnityEngine;

namespace Script.Tool
{
    public class Trigger : MonoBehaviour
    {
        public GameObject triggerOn;
        public GameObject triggerOff;

        public bool isPlayerInRange = false;

        private void Update()
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
            {
                triggerOn.SetActive(false);
                triggerOff.SetActive(true);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = false;
            }
        }
    }
}