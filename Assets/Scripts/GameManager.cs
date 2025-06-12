using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private float score;
    public static System.Action OnGameReset;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject spawnManager;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button gameReplayButton;
    [SerializeField] private Button gameQuitButton;

    void Start()
    {
        easyButton.onClick.AddListener(() => StartGame(1));
        mediumButton.onClick.AddListener(() => StartGame(2));
        hardButton.onClick.AddListener(() => StartGame(3));
        resumeButton.onClick.AddListener(ResumeGame);
        replayButton.onClick.AddListener(ReplayGame);
        quitButton.onClick.AddListener(QuitGame);
        gameReplayButton.onClick.AddListener(ReplayGame);
        gameQuitButton.onClick.AddListener(QuitGame);
    }

    void StartGame(int difficulty)
    {
        SpawnManager spawnManagerScript = spawnManager.GetComponent<SpawnManager>();
        Time.timeScale = 1f;

        scoreText.SetActive(true);
        player.SetActive(true);
        spawnManager.SetActive(true);
        score = 0;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        mainScreen.SetActive(false);

        if (difficulty == 1)
        {
            spawnManagerScript.minSpeed = 3f;
            spawnManagerScript.maxSpeed = 6f;
            spawnManagerScript.maxYChange = 2f;
            spawnManagerScript.minYChange = -3f;
            spawnManagerScript.maxYRange = 8f;
            spawnManagerScript.minLength = 7f;
            spawnManagerScript.maxLength = 12f;
            spawnManagerScript.xRange = 4f;
        }
        else if (difficulty == 2)
        {
            spawnManagerScript.minSpeed = 5f;
            spawnManagerScript.maxSpeed = 10f;
            spawnManagerScript.maxYChange = 3f;
            spawnManagerScript.minYChange = -5f;
            spawnManagerScript.maxYRange = 10f;
            spawnManagerScript.minLength = 5f;
            spawnManagerScript.maxLength = 10f;
            spawnManagerScript.xRange = 6f;
        }
        else if (difficulty == 3)
        {
            spawnManagerScript.minSpeed = 7f;
            spawnManagerScript.maxSpeed = 13f;
            spawnManagerScript.maxYChange = 4f;
            spawnManagerScript.minYChange = -6f;
            spawnManagerScript.maxYRange = 12f;
            spawnManagerScript.minLength = 2f;
            spawnManagerScript.maxLength = 6f;
            spawnManagerScript.xRange = 7f;
        }
    }

    public void GameOver()
    {
        player.SetActive(false);
        spawnManager.SetActive(false);
        scoreText.SetActive(false);
        gameOverText.text = "Well done! You Scored " + score + " points!";
        gameOverScreen.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }
    void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    void ReplayGame()
    {
        Debug.Log("1");
        player.transform.SetParent(null);
        player.transform.position = new Vector3(0, 15, 0);
        player.SetActive(false);
        spawnManager.SetActive(false);
        score = 0;
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        mainScreen.SetActive(true);
        OnGameReset?.Invoke();
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    void Update()
    {
        if (player.transform.position.x > score)
        {
            score = (float)Math.Round(player.transform.position.x);
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
        }
    }
}