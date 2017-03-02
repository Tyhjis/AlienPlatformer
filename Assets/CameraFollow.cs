using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {


	public GameObject playerCharacter;

	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position - playerCharacter.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		Vector3 targetPosition = playerCharacter.transform.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetPosition, 1.0f);
	}
}
