using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeBallScript : MonoBehaviour {

	private Vector3 smallBallScale = new Vector3(1.5f, 1.5f, 1.5f);
	private Vector3 mediumBallScale = new Vector3(3f, 3f, 3f);
	private Vector3 largeBallScale = new Vector3(7f, 7f, 7f);

	private float smallBallMass = 0.7f;
	private float mediumBallMass = 1f;
	private float largeBallMass = 2f;

	private bool removeResizer;
	private bool resizerRemoveCollided;
	private bool ballResized;

	private string smallBall = "SmallBall";
	private string mediumBall = "MediumBall";
	private string largeBall = "LargeBall";

	void Awake () {
		if (gameObject.name == smallBall || gameObject.name == mediumBall || gameObject.name == largeBall) {
			removeResizer = false;
		} else {
			removeResizer = true;
		}
	}
	
	void OnTriggerEnter(Collider target) {
		if (target.gameObject.tag == "Ball") {

			if (gameObject.tag == smallBall) {
				if (target.gameObject.transform.localScale != smallBallScale) {
					target.gameObject.transform.localScale = smallBallScale;
					target.gameObject.GetComponent<Rigidbody> ().mass = smallBallMass;
					ballResized = true;
				}
			}

			if (gameObject.tag == mediumBall) {
				if (target.gameObject.transform.localScale != mediumBallScale) {
					target.gameObject.transform.localScale = mediumBallScale;
					target.gameObject.GetComponent<Rigidbody> ().mass = mediumBallMass;
					ballResized = true;
				}
			}

			if (gameObject.tag == largeBall) {
				if (target.gameObject.transform.localScale != largeBallScale) {
					target.gameObject.transform.localScale = largeBallScale;
					target.gameObject.GetComponent<Rigidbody> ().mass = largeBallMass;
					ballResized = true;
				}
			}

			if (ballResized) {

				if (removeResizer) {
					resizerRemoveCollided = true;
				}

				ballResized = false;
				target.gameObject.GetComponent<BallSound> ().PlayPickUpSound ();
			}

			if (resizerRemoveCollided) {
				gameObject.SetActive (false);
			}

		}
	}


} // class
