using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;

	private Vector3 offset;
	private float offsetScale;

	void Start () {
		offsetScale = 1.0f;
		offset = transform.position - player.transform.position;
	}

	public void setOffsetScale(float offsetScale) {
		this.offsetScale = offsetScale;
	}
	
	void LateUpdate () {
		Vector3 pos = (offset * offsetScale + player.transform.position);
		transform.position = new Vector3(pos.x, Mathf.Max(pos.y, offset.y), pos.z);
		// TODO: Camela over position limit.
		// Mathf.Clamp();
	}
}
