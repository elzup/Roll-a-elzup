using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float speed;

	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
		speed = 10.0f;
	}
	
	void FixedUpdate () {
		float moveHorizonal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizonal, 0.0f, moveVertical);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "item") {
			other.gameObject.SetActive(false);
		}
	}

}
