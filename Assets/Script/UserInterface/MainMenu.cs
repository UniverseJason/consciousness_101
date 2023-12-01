using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.UserInterface
{
    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("Scenes/Level");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}