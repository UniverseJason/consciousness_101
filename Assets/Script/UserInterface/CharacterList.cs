using System.Collections.Generic;
using Script.ActionSystem;
using UnityEngine;

namespace Script.UserInterface
{
    public class CharacterList : MonoBehaviour
    {
        [SerializeField] private GameObject _characterBt;
        [SerializeField] private Transform _canvasTransform;

        // initial position of the UI element
        private Vector3 initPosition = new Vector3(60, 0, 0);

        // space between each UI element
        private float offsetY = 20;

        private SwitchObject _switchObject;
        private List<GameObject> _switchableObjects;

        private void Awake()
        {
            _switchObject = GetComponent<SwitchObject>();
            _switchableObjects = _switchObject.SwitchableObjects;
        }

        private void Start()
        {
            ShowUI();
        }

        private void ShowUI()
        {
            foreach (var item in _switchableObjects)
            {
                GameObject instance = Instantiate(_characterBt);
                instance.transform.SetParent(_canvasTransform, false);

                RectTransform rectTransform = instance.GetComponent<RectTransform>();

                // Set the anchors as per your design
                rectTransform.anchorMin = new Vector2(0, 0.5f);
                rectTransform.anchorMax = new Vector2(0, 0.5f);
                rectTransform.pivot = new Vector2(0.5f, 0.5f);

                // Set the anchored position
                rectTransform.anchoredPosition = initPosition;
                initPosition.y -= rectTransform.rect.height + offsetY;

                // Set text
                instance.GetComponentInChildren<UnityEngine.UI.Text>().text = item.name;

                // Add listener
                instance.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => _switchObject.SwitchTo(item));
            }
        }
    }
}
