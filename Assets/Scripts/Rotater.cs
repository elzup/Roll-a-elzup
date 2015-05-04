using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	int spinSpeed = 0;
	Vector3 velocity;
	Vector3 acceralation;

	bool is_catched;

	void Start() {
		this.velocity = Vector3.zero;
		this.acceralation = Vector3.zero;
		this.is_catched = false;
	}

	void Update () {
		spinSpeed += Random.Range(-10, 10);
		transform.Rotate (new Vector3(15, 30, spinSpeed) * Time.deltaTime);
		if (is_catched) {
			transform.position += this.velocity;
		}
	}

	void DelayDisappear() {
		this.gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			Vector3 movement = transform.position - other.transform.position;
			this.is_catched = true;
			this.velocity = movement;
			this.spinSpeed = 1000;
			Invoke("DelayDisappear", 1.0f);
		}
	}
}
