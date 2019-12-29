using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BallBlast
{

    // --------------------------------------------------
    // ENUMERATIONS
    // --------------------------------------------------

    public enum GUIState
    {
        Tutorial, Game, GameOver, GameVictory
    };

    // --------------------------------------------------
    // GUIController.cs
    // --------------------------------------------------

    public class GUIController : MonoBehaviour
    {
        // --------------------------------------------------
        // EDITOR VARIABLES
        // --------------------------------------------------

        [Header("Editor Support")]
        public bool printStates = false;
        public bool printMessages = false;
        public bool deleteStorageAtStart;

        [Header("Tutorial Panel")]
        public GameObject tutorialPanel;

        [Header("Game Panel")]
        public Camera mainCamera;
        public GameObject gamePanel;
        public Text scoreText;
        public Text highScoreText;
        public TextMeshProUGUI messageTMP;
        public TextMeshProUGUI levelTMP;
        public Button restartButton;

        [Header("Game Over Panel")]
        public GameObject gameOverPanel;
        public Text gameOverScoreText;
        public Text gameOverHighScoreText;
        public Button gameOverRestartButton;

        [Header("Game Victory Panel")]
        public GameObject gameVictoryPanel;
        public Text gameVictoryScoreText;
        public Text gameVictoryHighScoreText;
        public Button gameVictoryNextButton;

        [Header("Data")]
        public LevelData levelData;

        // --------------------------------------------------
        // PRIVATE VARIABLES
        // --------------------------------------------------

        private static GUIController staticInstance;
        private static GUIState state;
        private static int score;
        private static int highScore;
        private static string message;
        private static bool isRestartButtonActive;
        private static bool isMessageWriting;

        // --------------------------------------------------
        // FUNDAMENTAL
        // --------------------------------------------------

        private void Awake()
        {
            staticInstance = GetComponent<GUIController>();

            tutorialPanel.SetActive(true);
            gamePanel.SetActive(false);
            messageTMP.gameObject.SetActive(false);
            gameOverPanel.SetActive(false);

            InitLocalStorage();

            SCORE = 0;
            STATE = GUIState.Game;

            levelTMP.text = "LEVEL 1";
            //levelTMP.text = "Level " + levelData.level.ToString();
        }

        // --------------------------------------------------
        // METHODS
        // --------------------------------------------------

        public void START_LEVEL()
        {
            IS_RESTART_BUTTON_ACTIVE = true;
            STATE = GUIState.Game;

            Time.timeScale = 1;

            tutorialPanel.SetActive(false);
            gamePanel.SetActive(true);
            gameOverPanel.SetActive(false);
            gameVictoryPanel.SetActive(false);
        }

        public void PRINT_LEVEL()
        {
            MESSAGE = "LEVEL " + levelData.level.ToString();

            levelTMP.text = MESSAGE;
        }

        public void CLEAR_MESSAGE()
        {
            isMessageWriting = false;

            MESSAGE = "";
        }

        public void GAME_VICTORY()
        {
            STATE = GUIState.GameVictory;

            _update_scores();
            _save_high_score();

            tutorialPanel.SetActive(false);
            gamePanel.SetActive(false);
            gameOverPanel.SetActive(false);
            gameVictoryPanel.SetActive(true);

            void _update_scores()
            {
                gameVictoryScoreText.text = score.ToString();
                gameVictoryHighScoreText.text = highScore.ToString();
            }

            void _save_high_score()
            {
                if (SCORE > HIGH_SCORE)
                {
                    HIGH_SCORE = SCORE;

                    PlayerPrefs.SetInt("highScore", HIGH_SCORE);
                    PlayerPrefs.Save();
                }
            }
        }

        public void GAME_OVER()
        {
            IS_RESTART_BUTTON_ACTIVE = true;

            staticInstance.StartCoroutine(staticInstance.GameOver());
        }

        public void RESTART()
        {
            if (isRestartButtonActive && !isMessageWriting || STATE == GUIState.GameOver)
            {
                START_LEVEL();

                SCORE = 0;
                IS_RESTART_BUTTON_ACTIVE = false;
            }
        }

        public void GO_TO_LINK()
        {
            Application.OpenURL("https://www.linkedin.com/in/sametbaykul/");
        }

        public void INCREASE_POINT()
        {
            SCORE++;
        }

        // Access Methods:

        public int SCORE
        {
            set
            {
                score = value;

                staticInstance.scoreText.text = score.ToString();

                if (staticInstance.scoreText.gameObject.activeInHierarchy)
                {
                    staticInstance.scoreText.gameObject.GetComponent<Animator>().Play("Change", 0, 0);
                }
            }
            get
            {
                return score;
            }
        }

        public static int HIGH_SCORE
        {
            set
            {
                highScore = value;

                staticInstance.highScoreText.text = highScore.ToString();
            }
            get
            {
                return highScore;
            }
        }

        public string MESSAGE
        {
            set
            {
                message = value;

                if (isMessageWriting)
                {
                    return;
                }

                if (staticInstance.printMessages)
                {
                    print("MESSAGE: " + message);
                }

                staticInstance.messageTMP.text = message;
                staticInstance.messageTMP.gameObject.SetActive(true);
                staticInstance.StartCoroutine(staticInstance.FadeOutMessage());
            }
            get
            {
                return message;
            }
        }

        public static bool IS_RESTART_BUTTON_ACTIVE
        {
            set
            {
                isRestartButtonActive = value;
            }
        }

        public static GUIState STATE
        {
            set
            {
                state = value;

                if (staticInstance.printStates)
                {
                    print("GUIController.STATE = " + state);
                }
            }
            get
            {
                return state;
            }
        }

        // --------------------------------------------------
        // FUNCTIONS
        // --------------------------------------------------

        private static void InitLocalStorage()
        {
            _set_highscore();

            void _set_highscore()
            {
                if (PlayerPrefs.HasKey("highScore"))
                {
                    if (staticInstance.deleteStorageAtStart)
                    {
                        PlayerPrefs.SetInt("highScore", 0);
                    }

                    HIGH_SCORE = PlayerPrefs.GetInt("highScore", 0);
                }
            }
        }

        // --------------------------------------------------
        // COROUTINES
        // --------------------------------------------------

        private IEnumerator GameOver()
        {
            STATE = GUIState.GameOver;

            _update_scores();
            _save_high_score();

            tutorialPanel.SetActive(false);
            gamePanel.SetActive(false);
            gameOverPanel.SetActive(true);
            gameVictoryPanel.SetActive(false);

            yield break;

            void _update_scores()
            {
                gameOverScoreText.text = score.ToString();
                gameOverHighScoreText.text = highScore.ToString();
            }

            void _save_high_score()
            {
                if (SCORE > HIGH_SCORE)
                {
                    HIGH_SCORE = SCORE;

                    PlayerPrefs.SetInt("highScore", HIGH_SCORE);
                    PlayerPrefs.Save();
                }
            }
        }

        private IEnumerator FadeOutMessage()
        {
            isMessageWriting = true;

            yield return new WaitForSeconds(1);

            /*if (staticInstance.messageTMP.gameObject.activeInHierarchy)
            {
                staticInstance.messageTMP.gameObject.GetComponent<Animator>().Play("Delete", 0);
            }*/

            yield return new WaitForSeconds(0.5f);

            staticInstance.messageTMP.gameObject.SetActive(false);

            isMessageWriting = false;

            yield break;
        }
    }
}
