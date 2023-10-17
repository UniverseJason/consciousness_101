using Script.ActionSystem;
using UnityEngine;

namespace Script.Command
{
    public class SwitchCommand : ICommand
    {
        private SwitchObject _switchObject;
        private GameObject _newObject;

        public SwitchCommand(SwitchObject switchObject, GameObject newObject)
        {
            _switchObject = switchObject;
            _newObject = newObject;
        }

        public void Execute()
        {
            _switchObject.SwitchTo(_newObject);
        }
    }
}
