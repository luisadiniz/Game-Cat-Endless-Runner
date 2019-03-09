using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Transform player;
    private Vector3 offset;

    void Start ()
    {
        offset = transform.position - player.position;
	}
	
    //atualiza no final do frame
	void LateUpdate ()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, player.position.z + offset.z);
        transform.position = newPosition;
	}
}
