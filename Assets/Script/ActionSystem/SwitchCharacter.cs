using System.Collections.Generic;
using System.Linq;
using Script.InputControl;
using UnityEngine;

namespace Script.ActionSystem
{
    public class SwitchCharacter : MonoBehaviour
    {
        [SerializeField] private bool _enableSwitchCharacter;

        public bool EnableSwitchCharacter
        {
            get { return _enableSwitchCharacter; }
            set { _enableSwitchCharacter = value; }
        }

        [SerializeField] private List<GameObject> _switchableCharacters;

        public List<GameObject> SwitchableCharacters
        {
            get { return _switchableCharacters; }
            set { _switchableCharacters = value; }
        }

        private int _activeCharacterIndex;

        public int ActiveCharacterIndex
        {
            get { return _activeCharacterIndex; }
            set { _activeCharacterIndex = value; }
        }

        private void Awake()
        {
            _switchableCharacters = GameObject.FindGameObjectsWithTag("Player").ToList();

            // default active character is "Winter"
            foreach (var character in _switchableCharacters)
            {
                if (character.gameObject.name == "Winter")
                {
                    _activeCharacterIndex = _switchableCharacters.IndexOf(character);
                    character.gameObject.GetComponent<TransformInput>().EnablePlayerControl = true;
                    break;
                }
            }
        }

        public void SwitchTo(GameObject newCharacter)
        {
            // Check if the newCharacter is in the list of switchable characters
            GameObject characterToSwitchTo = _switchableCharacters.Find(character => character == newCharacter);

            // If characterToSwitchTo is not null, switch to new character!
            if (characterToSwitchTo != null)
            {
                // Disable the current active character
                _switchableCharacters[_activeCharacterIndex].gameObject.GetComponent<TransformInput>().EnablePlayerControl = false;

                // Enable the new character
                characterToSwitchTo.GetComponent<TransformInput>().EnablePlayerControl = true;

                // Update the active character index
                _activeCharacterIndex = _switchableCharacters.IndexOf(characterToSwitchTo);
            }
        }
    }
}
