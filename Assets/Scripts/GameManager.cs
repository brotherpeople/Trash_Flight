using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject pausePanel;
    private int coin = 0;

    [HideInInspector]
    public bool isGameOver = false;
    public bool isGamePaused = false;

    // singleton
    void Awake() {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGameOver) { TogglePause(); }
            else if (isGameOver) { Application.Quit(); }
        }
    }

    private void TogglePause()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    public void IncreaseCoin()
    {
        coin++;
        text.SetText(coin.ToString());

        if (coin % 20 == 0)
        {   // 20, 40, 50...
            Player player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver() {
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null) {
            enemySpawner.StopEnemyRoutine();    // stop spawning enemies when game over
        }

        // pops up after 1 second
        Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
}
