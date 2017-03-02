using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	[HideInInspector] public bool jump = false;
	[HideInInspector] public bool facingRight = true;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;

	private bool grounded = false;
	private Rigidbody2D rb2d;
	private Animator anim;

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float horizontalInput = Input.GetAxis ("Horizontal");
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate() {
		float h = Input.GetAxis ("Horizontal");
		if (Mathf.Abs(h) > 0f) {
			if (h * rb2d.velocity.x < maxSpeed) {
				rb2d.AddForce (Vector2.right * h * moveForce);
			}

			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
			}

			if (h > 0 && !facingRight)
				Flip ();
			else if (h < 0 && facingRight)
				Flip ();

			anim.SetTrigger ("Walk");
		} else {
			float speedXNow = rb2d.velocity.x;
			float speedYNow = rb2d.velocity.y;
			rb2d.velocity = new Vector2 (Mathf.Lerp (speedXNow, 0f, 1.0f), speedYNow);
			anim.SetTrigger ("Idle");
		}

		if (jump) {
			rb2d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}
		Debug.Log (jump);
		Debug.Log (rb2d.velocity);

	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
