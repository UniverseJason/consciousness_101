using System.Collections.Generic;
using Script.Animation;
using Script.InputControl;
using Script.Role;
using UnityEngine;

namespace Script.ActionSystem
{
    public class Punch : MonoBehaviour
    {
        public bool EnablePunch;
        [SerializeField] private float punchDamage = 10f;

        public CapsuleCollider2D punchCollider;

        // only apply punch for current active character
        public GameObject characterListUIController;
        private SwitchObject switchObject;

        // Animation
        private AnimationStateChanger animationStateChanger;
        [SerializeField] private string punchAnimationStateName;

        private TransformInput transInput;

        private void Awake()
        {
            punchCollider = GetComponent<CapsuleCollider2D>();
            animationStateChanger = GetComponent<AnimationStateChanger>();
            switchObject = characterListUIController.GetComponent<SwitchObject>();
            transInput = GetComponent<TransformInput>();
            punchCollider.enabled = false;
        }

        private void Update()
        {
            // Only Attack when Bot 01 is active
            int currentActiveObjectIndex = switchObject.ActiveObjectIndex;
            List<GameObject> switchableObjects = switchObject.SwitchableObjects;
            EnablePunch = switchableObjects[currentActiveObjectIndex].name == "Bot 01";

            if (Input.GetKeyDown(KeyCode.J) && EnablePunch)
            {
                transInput.enabled = false;
                animationStateChanger.ChangeAnimationState(punchAnimationStateName);
            }
        }

        public void SetPunchStart()
        {
            punchCollider.enabled = true;
            Debug.Log("Enable Punch!");
        }

        public void SetPunchEnd()
        {
            punchCollider.enabled = false;
            transInput.enabled = true;
            Debug.Log("Disable Punch!");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") && punchCollider.enabled)
            {
                var enemy = collision.GetComponent<Character>();
                if (enemy != null)
                {
                    enemy.TakeDamage(punchDamage);
                }
                Debug.Log("Punch Hit!");
            }
        }
    }
}