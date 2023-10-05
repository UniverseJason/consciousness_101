using System;
using Script.ActionSystem;
using Script.Command;
using UnityEngine;

namespace Script.InputControl
{
    public class SkillInput : MonoBehaviour
    {
        [SerializeField] public bool EnableSkill;
        [SerializeField] private SwitchCharacter _switchCharacter;

        private void Awake()
        {
            _switchCharacter = GetComponent<SwitchCharacter>();
        }

        private void Update()
        {
            if (EnableSkill)
            {
                HandleSwitchInput();
            }
        }

        private void HandleSwitchInput()
        {
            // TODO: need UI!
            /*
             * Show a list on the screen says which character is switchable
             * When player click on of the character in that list
             * use switch to command:
             * i.e. new SwitchCommand(_switchCharacter, some_game_object).Execute();
             */
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _switchCharacter.SwitchTo(_switchCharacter.SwitchableCharacters[0]);
            }
        }
    }
}
