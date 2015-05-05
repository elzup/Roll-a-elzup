using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private int count;
	public float speed;

	public Text timerText;
	public Text bText;
	public Text sText;
	public Text gText;
	public Text winText;

	private GameStates gameState;
	enum GameStates { Progress, GameOver, Finish };

	public CameraController camera;

	public int countBronzeMax; 
	private int countBronze;
	public int countSilverMax; 
	private int countSilver;
	public int countGoldMax; 
	private int countGold;

	public int HideFlagsag;

	public Collider torigger;

	System.DateTime startTime;
	System.TimeSpan scoreTime;

	int itai;

	void Start () {
		rb = GetComponent<Rigidbody>();
		count = 0;
		winText.text = "";
		gameState = GameStates.Progress;
		this.countGold = 0;
		this.countSilver = 0;
		this.countBronze = 0;
		this.itai = 0;
		this.startTime = System.DateTime.Now;
		this.scoreTime = new System.TimeSpan();
		this.timerText.text = "";
		updateCountText();
		StartCoroutine(LateStart (0.001f));
	}

	IEnumerator LateStart(float time) {
		yield return new WaitForSeconds(time);
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
		float rx = rb.rotation.eulerAngles.x % 360;
		float rz = rb.rotation.eulerAngles.z % 360;
		print (rb.rotation.eulerAngles.x);
		print (rb.rotation.eulerAngles.z);
		if ((rx < 30 || 330 < rx) &&
		    (150 < rz && rz < 210)) {
			itai ++;
			Debug.Log (itai);
		}
		if (gameState != GameStates.Finish) {
			scoreTime = (System.DateTime.Now - startTime);
		}
		timerText.text = string.Format("Timer {0,2}:{1,2}:{2,3}", scoreTime.Minutes, scoreTime.Seconds, scoreTime.Milliseconds);
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
			float bound = 0;
			float hbound = 0;
			if (other.name.Contains("First")) {
				bound = 1000;
				countBronze += 1;
			} else if (other.name.Contains("Second")) {
				bound = 2000;
				countSilver += 1;
			} else {
				bound = 6000;
				countGold += 1;
				hbound = 500.0f;
			}
			other.isTrigger = false;
			Vector3 movement = (rb.transform.position - rb.velocity) - other.transform.position;
			Vector3 movementXY = new Vector3(movement.x, 0, movement.z);
			Vector3 force = (movementXY / movementXY.magnitude) * bound;
			count += 1;
			rb.AddForce(new Vector3(force.x, hbound, force.z));
			updateCountText();
			if (count >= countBronzeMax + countSilverMax + countGoldMax) {
				winText.text = "You Win! [Score: " + scoreTime.Minutes + "m" + scoreTime.Seconds + "s" + scoreTime.Milliseconds+ "]";
				gameState = GameStates.Finish;
			}
		} else if (other.gameObject.tag == "torigger") {
			this.camera.setOffsetScale(2.0f);
		}
	}

	private void updateCountText() {
		bText.text = string.Format("Brond  : {0,2}/{1,2}", countBronze, countBronzeMax);
		sText.text = string.Format("Silver : {0,2}/{1,2}", countSilver, countSilverMax);
		gText.text = string.Format("Gold   : {0,2}/{1,2}", countGold, countGoldMax);
	}
}
