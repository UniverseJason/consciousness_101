using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Script.UserInterface
{
    public class UIControl : MonoBehaviour
    {
        public string currentSceneName;

        // Pause Menu
        public GameObject pauseMenu;
        private bool isPaused = false;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = !isPaused;
                ShowMenu();
            }
        }

        public void ShowMenu()
        {
            StopAllCoroutines();
            Vector3 targetPosition = isPaused ? new Vector3(0, 500, 0) : new Vector3(0, -1000, 0);
            StartCoroutine(MoveMenu(pauseMenu, targetPosition, 0.5f));
        }

        IEnumerator MoveMenu(GameObject menu, Vector3 targetPosition, float duration)
        {
            float time = 0;
            Vector3 startPosition = menu.transform.localPosition;

            while (time < duration)
            {
                menu.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            menu.transform.localPosition = targetPosition;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void Reload()
        {
            SceneManager.LoadScene(currentSceneName);
        }
    }
}