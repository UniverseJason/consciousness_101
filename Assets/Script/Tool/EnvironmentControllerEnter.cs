using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Script.Tool
{
    public class EnvironmentControllerEnter : MonoBehaviour
    {
        public List<GameObject> objectsToFade;
        public float fadeDuration = 1f;

        private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

        private void Start()
        {
            // Initialize spriteRenderers list with SpriteRenderer components from objectsToFade
            foreach (var obj in objectsToFade)
            {
                SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    spriteRenderers.Add(sr);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (var sr in spriteRenderers)
                {
                    StopCoroutine(FadeTo(sr, 0.0f, fadeDuration));
                    StartCoroutine(FadeTo(sr, 0.0f, fadeDuration));
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (var sr in spriteRenderers)
                {
                    StopCoroutine(FadeTo(sr, 1.0f, fadeDuration));
                    StartCoroutine(FadeTo(sr, 1.0f, fadeDuration));
                }
            }
        }

        IEnumerator FadeTo(SpriteRenderer spriteRenderer, float targetOpacity, float duration)
        {
            float startOpacity = spriteRenderer.color.a;
            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(startOpacity, targetOpacity, t));
                spriteRenderer.color = newColor;
                yield return null;
            }

            // Ensure the final opacity is set
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetOpacity);
        }
    }
}
