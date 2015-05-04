using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private int count;
	public float speed;

	public Text countText;
	public Text winText;

	private GameStates gameState;
	enum GameStates { Progress, GameOver, Finish };

	public CameraController camera;

	public Collider torigger;

	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		winText.text = "";
		SetCountText();
		gameState = GameStates.Progress;
	}

	void FixedUpdate () {
		float moveHorizonal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizonal, 0.0f, moveVertical);
		rb.AddForce(movement * speed);

		if (rb.position.y < -10.0 && gameState == GameStates.Progress) {
			gameState = GameStates.GameOver;
			winText.text = "Game over";
			Invoke ("Retry", 3.0f);
		}
	}

	void Retry() {
		this.camera.setOffsetScale(1.0f);
		winText.text = "";
		rb.position = new Vector3(0.0f, 1.0f, 0.0f);
		rb.velocity = Vector3.zero;
		gameState = GameStates.Progress;
	}

//	void Update() {
//		if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Backspace)) {
//			Application.Quit();
//		}
//	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "item") {
			int bound = 0;
			if (other.name.Contains("First")) {
				bound = 1000;
			} else if (other.name.Contains("Second")) {
				bound = 2000;
			} else {
				bound = 3000;
			}
			Vector3 movement = (rb.transform.position - rb.velocity) - other.transform.position;
			Vector3 movementXY = new Vector3(movement.x, 0, movement.z);
			Vector3 force = (movementXY / movementXY.magnitude) * bound;
			rb.AddForce(force);
			count += 1;
			SetCountText();
		} else if (other.gameObject.tag == "torigger") {
			this.camera.setOffsetScale(2.0f);
		}
	}

	void SetCountText() {
		countText.text = "Count: " + count.ToString();
		if (count >= 6) {
			winText.text = "You Win!";
			gameState = GameStates.Finish;
		}
	}
}
