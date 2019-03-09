using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour {

    public static GameDataManager _gameData;

    private void Awake()
    {
        if (_gameData == null)
        {
            _gameData = this;
        }
        else if(_gameData != this)
        {
            Destroy(_gameData);
        }

        DontDestroyOnLoad(_gameData);
    }

    void Start () {
		
	}
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

}
