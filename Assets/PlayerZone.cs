using UnityEngine;
using System.Collections;

public class PlayerZone : MonoBehaviour {
	void OnTriggerExit (Collider obj) {
		if (obj.tag == "itai") {
			Destroy(obj);
		}
	}
}
