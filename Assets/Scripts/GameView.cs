using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour {

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _coinText;

    [SerializeField] private GameObject _statsPanel;
    [SerializeField] private Text _highestScoreText;
    [SerializeField] private Text _totalCoinsText;


    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    public void RestatButton()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateScoreText (int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateCoinsText(int coin)
    {
        _coinText.text = coin.ToString();
    }

    public void StatsPanel()
    {
        _gameOverPanel.SetActive(false);

        PlayerData data = SaveSystem.LoadPlayer();
        _highestScoreText.text = "Highest Score: " + (int)data.HighestScore;
        _totalCoinsText.text = "Total Coins: " + data.TotalCoins;

        _statsPanel.SetActive(true);
    }
}
