using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	int spinSpeed = 0;
	void Update () {
		spinSpeed += Random.Range(-10, 10);
		transform.Rotate (new Vector3(15, spinSpeed, 30) * Time.deltaTime);
	}
}
