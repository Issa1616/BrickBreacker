﻿using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const int NUM_LEVELS = 2;

    private Ball ball;
    private Paddle paddle;
    private Brick[] bricks;

    public int level { get; private set; } = 1;
    public int score { get; private set; } = 0;
    public GameObject youWonPanel;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            FindSceneReferences();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void FindSceneReferences()
    {
        ball = FindObjectOfType<Ball>();
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        if (level > NUM_LEVELS)
        {
            WinGame();
            return;
        }

        SceneManager.sceneLoaded += OnLevelLoaded;
        SceneManager.LoadScene($"Level{level}");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        FindSceneReferences();
    }

    public void OnBallMiss()
    {
        GameOver();
    }

    private void ResetLevel()
    {
        paddle.ResetPaddle();
        ball.ResetBall();
    }

    private void GameOver()
{
    SceneManager.LoadScene("GameOver");
}

    public void NewGame()
    {
        ResetGame();
        LoadLevel(1);
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points;

        if (Cleared()) {
            LoadLevel(level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i] == null) continue;

            if (bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }

        return true;
    }

    public int GetBrickHealth()
    {
        return Mathf.Clamp(level, 1, 5);
    }

    public void ResetGame()
    {
        score = 0;
        level = 1;
    }

        public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        score = 0;
        level = 1;

        SceneManager.LoadScene("MainMenu");
    }

    public void ApplyReverseControls()
    {
        if (paddle != null)
            paddle.ReverseControls(5f);
    }

    private void WinGame()
    {
        Time.timeScale = 0f;

        if (youWonPanel != null)
            youWonPanel.SetActive(true);
    }



}