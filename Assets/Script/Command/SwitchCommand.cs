using Script.ActionSystem;
using UnityEngine;

namespace Script.Command
{
    public class SwitchCommand : ICommand
    {
        private SwitchCharacter _switchCharacter;
        private GameObject _newCharacter;

        public SwitchCommand(SwitchCharacter switchCharacter, GameObject newCharacter)
        {
            _switchCharacter = switchCharacter;
            _newCharacter = newCharacter;
        }

        public void Execute()
        {
            _switchCharacter.SwitchTo(_newCharacter);
        }
    }
}
