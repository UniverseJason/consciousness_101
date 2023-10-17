using Script.ActionSystem;
using Script.Command;
using UnityEngine;

namespace Script.InputControl
{
    public class TokenSkillInput : MonoBehaviour
    {
        public bool EnableSkill;

        public bool EnableFollow;
        public FollowObject followObject;

        private TransformInput _input;

        private void Awake()
        {
            followObject = GetComponent<FollowObject>();
            _input = GetComponent<TransformInput>();
        }

        private void Update()
        {
            if (EnableSkill)
            {
                if (EnableFollow)
                {
                    new FollowCommand(followObject).Execute();
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                EnableFollow = !EnableFollow;
            }

        }
    }
}
