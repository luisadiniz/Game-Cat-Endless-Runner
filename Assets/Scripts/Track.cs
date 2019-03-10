using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private Vector2 _numberOfObstacles;

    [SerializeField] private List<GameObject> NewOstacles;

    [SerializeField] private Player _player;

	void Start ()
    {
        int newRandomNumber = (int)Random.Range(_numberOfObstacles.x, _numberOfObstacles.y);
        for (int i = 0; i < newRandomNumber; i++)
        {
            NewOstacles.Add(Instantiate(_obstacles[Random.Range(0, _obstacles.Length)], transform));
            NewOstacles[i].SetActive(false);
        }

        PositionateObstacles();
	}
	
	private void PositionateObstacles()
    {
        for (int i = 0; i < NewOstacles.Count; i++)
        {
            float posZMin = (297f / NewOstacles.Count) + (297 / NewOstacles.Count) * i;
            float posZMax = (297f / NewOstacles.Count) + (297 / NewOstacles.Count) * i + 1;
            NewOstacles[i].transform.localPosition = new Vector3(0, 0, Random.Range(posZMin, posZMax));

            NewOstacles[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.IncreaseSpeed();
            transform.position = new Vector3(0, 0, transform.position.z + 297 * 2);
            PositionateObstacles();
        }
    }
}
