using UnityEngine;
using System.Collections;

public class CameraTorigger : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.tag);
	}
}
