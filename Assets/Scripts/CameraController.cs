using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;

	private Vector3 offset;

	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	void LateUpdate () {
		transform.position = offset + Vector3.Scale(Vector3.one - Vector3.up, player.transform.position);
		// TODO: Camela over position limit.
		// Mathf.Clamp();
	}
}
