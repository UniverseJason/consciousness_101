using System.Collections.Generic;
using System.Linq;
using Script.InputControl;
using UnityEngine;

namespace Script.ActionSystem
{
    public class SwitchObject : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _switchableObjects;
        public List<GameObject> SwitchableObjects
        {
            get { return _switchableObjects; }
            set { _switchableObjects = value; }
        }

        private int _activeObjectIndex;
        public int ActiveObjectIndex
        {
            get { return _activeObjectIndex; }
            set { _activeObjectIndex = value; }
        }

        private CameraFollow _cameraFollow;

        private void Awake()
        {
            _switchableObjects = GameObject.FindGameObjectsWithTag("Player").ToList();
            _cameraFollow = Camera.main.GetComponent<CameraFollow>();

            // default active character is "Winter"
            foreach (var item in _switchableObjects)
            {
                if (item.gameObject.name == "Winter")
                {
                    // Enable the character by using its index
                    _activeObjectIndex = _switchableObjects.IndexOf(item);
                    item.gameObject.GetComponent<TransformInput>().EnablePlayerControl = true;

                    // Enable camera follow to character
                    _cameraFollow.EnableCameraFollow = true;
                    _cameraFollow.Target = item.transform;
                    break;
                }
            }
        }

        public void SwitchTo(GameObject newObject)
        {
            // Check if the newObject is in the list of switchable object
            GameObject switchableObject = _switchableObjects.Find(o => o == newObject);

            // If switchableObject is not null, switch!
            if (switchableObject != null)
            {
                // Disable the current active object
                _switchableObjects[_activeObjectIndex].gameObject.GetComponent<TransformInput>().EnablePlayerControl = false;

                // Enable the new character
                switchableObject.GetComponent<TransformInput>().EnablePlayerControl = true;

                // Update the active character index
                _activeObjectIndex = _switchableObjects.IndexOf(switchableObject);

                // Enable camera follow to new character
                _cameraFollow.Target = switchableObject.transform;
            }
        }
    }
}
