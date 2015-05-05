using UnityEngine;
using System.Collections;

public class CameraLimit : MonoBehaviour {
	public GameObject player;

	void LateUpdate () {
		this.transform.position = player.transform.position;
//		this.transform.Rotate (startRotate);
	}
}
