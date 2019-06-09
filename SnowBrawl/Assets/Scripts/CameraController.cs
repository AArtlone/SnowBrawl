using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

	void Start () {

        offset = transform.position - player.transform.position;

	}
	
	void LateUpdate () {

        transform.position = player.transform.position + offset;

        if (bounds) {

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));

        }
		
	}
}
