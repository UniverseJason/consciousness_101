using System.Collections.Generic;
using Script.ActionSystem;
using Script.TokenAction;
using UnityEngine;

namespace Script.InputControl
{
    public class TokenInput : MonoBehaviour
    {
        public bool EnableSkill;

        // Follow Object
        public bool EnableFollow;
        private FollowObject followObject;

        // Attack
        public bool EnableAttack;
        private TokenAttack tokenAttack;
        private bool isAttacking;
        private bool isReturning;

        // Only Attack when winter is active
        // Need reference to UI Controller
        public GameObject characterListUIController;
        private SwitchObject switchObject;

        private void Awake()
        {
            followObject = GetComponent<FollowObject>();
            tokenAttack = GetComponent<TokenAttack>();
            switchObject = characterListUIController.GetComponent<SwitchObject>();
        }

        private void Update()
        {
            if (EnableSkill)
            {
                // Only Attack when winter is active
                int currentActiveObjectIndex = switchObject.ActiveObjectIndex;
                List<GameObject> switchableObjects = switchObject.SwitchableObjects;
                EnableAttack = switchableObjects[currentActiveObjectIndex].name == "Winter";

                if (Input.GetKeyDown(KeyCode.F)) EnableFollow = !EnableFollow;

                if (EnableFollow) followObject.FollowTarget();

                if (EnableAttack)
                {
                    if (Input.GetMouseButtonDown(0) && !isAttacking && !isReturning)
                    {
                        tokenAttack.StartAttack();
                        isAttacking = true;
                    }

                    if (isAttacking)
                    {
                        tokenAttack.MoveTo(tokenAttack.TargetPosition);
                        if (Vector3.Distance(transform.position, tokenAttack.TargetPosition) < 0.01f)
                        {
                            isAttacking = false;
                            isReturning = true;
                        }
                    }

                    if (isReturning)
                    {
                        tokenAttack.MoveTo(tokenAttack.OriginPosition);
                        if (Vector3.Distance(transform.position, tokenAttack.OriginPosition) < 0.01f)
                        {
                            isReturning = false;
                        }
                    }
                }
            }
        }
    }
}