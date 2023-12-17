using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Assets.Scripts.Controllers
{
    public class MainMenuController : MonoBehaviour
    {
        [FormerlySerializedAs("MainMenu")] public GameObject mainMenu;
        [FormerlySerializedAs("OptionsMenu")] public GameObject optionsMenu;

        private void Start()
        {
            ShowMainMenu();
        }
        public void StartGame()
        {
            SceneManager.LoadScene("Game");

        }

        public void ShowMainMenu( )
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
        public void ShowOptionsMenu()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
