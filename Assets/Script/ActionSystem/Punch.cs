using System;
using System.Collections.Generic;
using Script.Animation;
using Script.InputControl;
using Script.Role;
using UnityEngine;

namespace Script.ActionSystem
{
    public class Punch : MonoBehaviour
    {
        [Header("Logic Control")]
        public bool EnablePunch;
        public bool CheckKeyInput;
        public String EnablePunchCharacterName;
        private bool key;

        [Header("Detection")]
        [SerializeField] private float punchDamage = 10f;
        public CapsuleCollider2D punchCollider;

        [Header("Check Character Activation")]
        public bool CheckCharacterActivation;
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
            if (CheckCharacterActivation)
            {
                int currentActiveObjectIndex = switchObject.ActiveObjectIndex;
                List<GameObject> switchableObjects = switchObject.SwitchableObjects;
                EnablePunch = switchableObjects[currentActiveObjectIndex].name == EnablePunchCharacterName;
            }

            if (CheckKeyInput) key = Input.GetKeyDown(KeyCode.E);
            else key = true;

            if (!key || !EnablePunch) return;

            transInput.enabled = false;
            animationStateChanger.ChangeAnimationState(punchAnimationStateName);
        }

        public void SetPunchStart()
        {
            punchCollider.enabled = true;
        }

        public void SetPunchEnd()
        {
            punchCollider.enabled = false;
            transInput.enabled = true;
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
            }
        }
    }
}