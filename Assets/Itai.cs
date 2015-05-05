using UnityEngine;
using System.Collections;

public class Itai : MonoBehaviour {

	public bool isClone;
	void FixedUpdate() {
		transform.position = transform.position + Vector3.up * 0.1f;
	}

}
