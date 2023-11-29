using System.Collections.Generic;
using Script.ActionSystem;
using Script.Role;
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

        // track the subscription
        private List<(Character character, UnityEngine.UI.Text textComponent)> subscriptions = new List<(Character, UnityEngine.UI.Text)>();

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

                // Get the character component
                Character characterComponent = item.GetComponent<Character>();

                // Set text with character name and initial health
                UnityEngine.UI.Text textComponent = instance.GetComponentInChildren<UnityEngine.UI.Text>();
                textComponent.text = item.name + "\n" + characterComponent.Health;

                // Subscribe to the OnHealthChanged event
                characterComponent.OnHealthChanged += (newHealth) =>
                {
                    textComponent.text = item.name + "\n" + newHealth;
                };

                // Store the subscription
                subscriptions.Add((characterComponent, textComponent));

                // Add listener
                instance.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => _switchObject.SwitchTo(item));
            }
        }

        private void OnDestroy()
        {
            foreach (var (character, textComponent) in subscriptions)
            {
                if (character != null)
                {
                    character.OnHealthChanged -= (newHealth) =>
                    {
                        textComponent.text = character.name + "\n" + newHealth;
                    };
                }
            }
        }


    }
}
