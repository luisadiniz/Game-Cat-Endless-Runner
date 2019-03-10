using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private Vector2 _numberOfObstacles;

    [SerializeField] private GameObject _coin;
    [SerializeField] private Vector2 _numberOfCoins;

    [SerializeField] private List<GameObject> NewOstacles;
    [SerializeField] private List<GameObject> NewCoins;

    [SerializeField] private Player _player;

	void Start ()
    {
        int newObstaclesNumber = (int)Random.Range(_numberOfObstacles.x, _numberOfObstacles.y);
        for (int i = 0; i < newObstaclesNumber; i++)
        {
            NewOstacles.Add(Instantiate(_obstacles[Random.Range(0, _obstacles.Length)], transform));
            NewOstacles[i].SetActive(false);
        }

        int newCoinsNumber = (int)Random.Range(_numberOfCoins.x, _numberOfCoins.y);
        for (int i = 0; i < newCoinsNumber; i++)
        {
            NewCoins.Add(Instantiate(_coin, transform));
            NewCoins[i].SetActive(false);
        }

        PositionateObstacles();
        PositionateCoins();
	}
	
	private void PositionateObstacles()
    {
        for (int i = 0; i < NewOstacles.Count; i++)
        {
            float posZMin = (297f / NewOstacles.Count) + (297 / NewOstacles.Count) * i;
            float posZMax = (297f / NewOstacles.Count) + (297 / NewOstacles.Count) * i + 1;
            NewOstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));

            NewOstacles[i].SetActive(true);

            if(NewOstacles[i].GetComponent<ChangeLane>() != null)
            {
                NewOstacles[i].GetComponent<ChangeLane>().PositionLane();
            }
        }
    }

    private void PositionateCoins()
    {
        float posZmin = 10f;
        for (int i = 0; i < NewCoins.Count; i++)
        {
            float posZMax = posZmin + 5f;
            float randomPosZ = Random.Range(posZmin, posZMax);
            NewCoins[i].transform.localPosition = new Vector3(transform.position.x, transform.position.y, randomPosZ);

            NewCoins[i].SetActive(true);
            NewCoins[i].GetComponent<ChangeLane>().PositionLane();
            posZmin = randomPosZ + 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.IncreaseSpeed();
            transform.position = new Vector3(0, 0, transform.position.z + 297 * 2);
            PositionateObstacles();
            PositionateCoins();
        }
    }
}
