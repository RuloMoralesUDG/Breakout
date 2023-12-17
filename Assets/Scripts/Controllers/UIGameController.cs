using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private GameObject losePanel;
        [SerializeField] private GameObject winnerPanel;
        [SerializeField] private GameObject[] livesImg;
        [SerializeField] private Text gameTimeText;

        internal void ShowLosePanel()
        {
            losePanel.SetActive(true);
        }
        internal void ShowWinnerPanel(float gameTime)
        {
            winnerPanel.SetActive(true);
            gameTimeText.text = $"Your time: {Mathf.Floor(gameTime)} s";
        }
        public void TryAgain()
        {
            SceneManager.LoadScene("Game");
        }
        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        public void UpdateUILives(byte currentLives)
        {
            for (var i = 0; i < livesImg.Length; i++)
            {
                if (i >= currentLives)
                {
                    livesImg[i].SetActive(false);
                }
            }
        }
    }
}
