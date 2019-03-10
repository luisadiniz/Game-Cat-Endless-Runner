using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : MonoBehaviour {

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _scoreText;


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
}
