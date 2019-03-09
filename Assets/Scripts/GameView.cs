using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameView : MonoBehaviour {

    [SerializeField] private GameObject _gameOverPanel;


    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    public void RestatButton()
    {
        SceneManager.LoadScene(1);
    }
}
