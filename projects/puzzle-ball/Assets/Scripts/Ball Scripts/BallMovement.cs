using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour {

	private string direction = "";
	private string directionLastFrame = "";

	[HideInInspector]
	public int onFloorTracker = 0;

	private bool fullSpeed = false;

	//speed variables
	private int floorSpeed = 100;
	private int airSpeed = 20;
	private float airSpeedDiagonal = 5.858f;
	private float airDrag = 0.1f;
	private float floorDrag = 2.29f;
	private float delta = 50f;

	// camera variables
	private Vector3 cameraRelativeRight;
	private Vector3 cameraRelativeUp;
	private Vector3 cameraRelativeDown;
	private Vector3 cameraRelativeUpRight;
	private Vector3 cameraRelativeUpLeft;

	// velocity and magnitude variables
	private Vector3 xVel;
	private Vector3 zVel;
	private float xSpeed;
	private float zSpeed;

	// movement axis
	private string AxisY = "Vertical";
	private string AxisX = "Horizontal";

	private Rigidbody myBody;

	private Camera mainCamera;

	void Awake() {
		myBody = GetComponent<Rigidbody> ();
		mainCamera = Camera.main;
	}

	void Start () {
		
	}

	void Update () {
        UpdateCameraRelativePosition();
        GetDirection ();
        FullSpeedController();
        DragAdjustmentAndAirSpeed();
        BallFellDown();
    }

	void FixedUpdate() {
        MoveTheBall();
    }

	void LateUpdate() {
        directionLastFrame = direction;
    }

	void GetDirection() {
		direction = "";

		if (Input.GetAxis (AxisY) > 0) {
			direction += "up";
		} else if (Input.GetAxis (AxisY) < 0) {
			direction += "down";
		}

		if (Input.GetAxis (AxisX) > 0) {
			direction += "right";
		} else if (Input.GetAxis (AxisX) < 0) {
			direction += "left";
		}

	}

	void MoveTheBall() {
		switch (direction) {
			
		case "upright":

			if (onFloorTracker > 0) {
				// on floor
				if (fullSpeed) {
					myBody.AddForce (floorSpeed * cameraRelativeUpRight *
						Time.fixedDeltaTime * delta);
				} else {
					myBody.AddForce ((floorSpeed  - 75f) * cameraRelativeUpRight *
						Time.fixedDeltaTime * delta);
				}

			} else if (onFloorTracker == 0) {
				// in air
				if (zVel.normalized == cameraRelativeUp) {
					if (zSpeed < (airSpeed - airSpeedDiagonal - 0.1f)) {
						myBody.AddForce (10.6f * cameraRelativeUp *
						Time.fixedDeltaTime * delta);
					}
				} else {
					myBody.AddForce (10.6f * cameraRelativeUp * 
						Time.fixedDeltaTime * delta);
				}

				if (xVel.normalized == cameraRelativeRight) {
					if (xSpeed < (airSpeed - airSpeedDiagonal - 0.1f)) {
						myBody.AddForce (10.6f * cameraRelativeRight *
							Time.fixedDeltaTime * delta);
					}
				} else {
					myBody.AddForce (10.6f * cameraRelativeRight *
						Time.fixedDeltaTime * delta);
				}

			}

			break;

		case "upleft":

			if (onFloorTracker > 0) {
				// on floor
				if (fullSpeed) {
					myBody.AddForce (floorSpeed * cameraRelativeUpLeft *
					Time.fixedDeltaTime * delta);
				} else {
					myBody.AddForce ((floorSpeed - 75f) * cameraRelativeUpLeft *
					Time.fixedDeltaTime * delta);
				}
			} else if(onFloorTracker == 0) {
				// in air
				if(zVel.normalized == cameraRelativeUp) {
					if (zSpeed < (airSpeed - airSpeedDiagonal - 0.1f)) {
						myBody.AddForce (10.6f * cameraRelativeUp
							* Time.fixedDeltaTime * delta);
					} else {
						myBody.AddForce (10.6f * cameraRelativeUp 
							* Time.fixedDeltaTime * delta);
					}
				}

				if (xVel.normalized == -cameraRelativeRight) {
					if (xSpeed < (airSpeed - airSpeedDiagonal - 0.1f)) {
						myBody.AddForce (10.6f * -cameraRelativeRight *
						Time.fixedDeltaTime * delta);
					} 
				} else {
					myBody.AddForce (10.6f * -cameraRelativeRight *
						Time.fixedDeltaTime * delta);
				}

			}

			break;

		case "downright":

			if (onFloorTracker > 0) {
				// on floor
				if (fullSpeed) {
					myBody.AddForce (floorSpeed * -cameraRelativeUpLeft *
						Time.fixedDeltaTime * delta);
				} else {
					myBody.AddForce ((floorSpeed - 75f) * -cameraRelativeUpLeft *
						Time.fixedDeltaTime * delta);
				}
			} else if(onFloorTracker == 0) {
				// in air
				if(zVel.normalized == -cameraRelativeUp) {
					if (zSpeed < (airSpeed - airSpeedDiagonal - 0.1f)) {
						myBody.AddForce (10.6f * -cameraRelativeUp
							* Time.fixedDeltaTime * delta);
					} else {
						myBody.AddForce (10.6f * -cameraRelativeUp 
							* Time.fixedDeltaTime * delta);
					}
				}

				if (xVel.normalized == cameraRelativeRight) {
					if (xSpeed < (airSpeed - airSpeedDiagonal - 0.1f)) {
						myBody.AddForce (10.6f * cameraRelativeRight *
							Time.fixedDeltaTime * delta);
					} 
				} else {
					myBody.AddForce (10.6f * cameraRelativeRight *
						Time.fixedDeltaTime * delta);
				}

			}

			break;

		case "downleft":

			if (onFloorTracker > 0) {
				// on floor
				if (fullSpeed) {
					myBody.AddForce (floorSpeed * -cameraRelativeUpRight *
						Time.fixedDeltaTime * delta);
				} else {
					myBody.AddForce ((floorSpeed - 75f) * -cameraRelativeUpRight *
						Time.fixedDeltaTime * delta);
				}
			} else if(onFloorTracker == 0) {
				// in air
				if(zVel.normalized == -cameraRelativeUp) {
					if (zSpeed < (airSpeed - airSpeedDiagonal - 0.1f)) {
						myBody.AddForce (10.6f * -cameraRelativeUp
							* Time.fixedDeltaTime * delta);
					} else {
						myBody.AddForce (10.6f * -cameraRelativeUp 
							* Time.fixedDeltaTime * delta);
					}
				}

				if (xVel.normalized == -cameraRelativeRight) {
					if (xSpeed < (airSpeed - airSpeedDiagonal - 0.1f)) {
						myBody.AddForce (10.6f * -cameraRelativeRight *
							Time.fixedDeltaTime * delta);
					} 
				} else {
					myBody.AddForce (10.6f * -cameraRelativeRight *
						Time.fixedDeltaTime * delta);
				}

			}

			break;

		case "up":

			if (onFloorTracker > 0) {
				// on floor
				if (fullSpeed) {
					myBody.AddForce (floorSpeed * cameraRelativeUp *
						Time.fixedDeltaTime * delta);
				} else {
					myBody.AddForce ((floorSpeed - 75f) * cameraRelativeUp *
						Time.fixedDeltaTime * delta);
				}
			} else if(onFloorTracker == 0) {
				// in air
				if(zSpeed < airSpeed) {
					myBody.AddForce ((airSpeed * 0.75f) * cameraRelativeUp
						* Time.fixedDeltaTime * delta);
				}

				if (xSpeed > 0.1f) {
					if (xVel.normalized == cameraRelativeRight) {
						myBody.AddForce ((airSpeed * 0.75f) * -cameraRelativeRight
							* Time.fixedDeltaTime * delta);	
					} else if (xVel.normalized == -cameraRelativeRight) {
						myBody.AddForce ((airSpeed * 0.75f) * cameraRelativeRight
							* Time.fixedDeltaTime * delta);
					}
				}

			}

			break;

		case "down":

			if (onFloorTracker > 0) {
				// on floor
				if (fullSpeed) {
					myBody.AddForce (floorSpeed * -cameraRelativeUp *
						Time.fixedDeltaTime * delta);
				} else {
					myBody.AddForce ((floorSpeed - 75f) * -cameraRelativeUp *
						Time.fixedDeltaTime * delta);
				}
			} else if(onFloorTracker == 0) {
				// in air
				if(zSpeed < airSpeed) {
					myBody.AddForce ((airSpeed * 0.75f) * -cameraRelativeUp
						* Time.fixedDeltaTime * delta);
				}

				if (xSpeed > 0.1f) {
					if (xVel.normalized == cameraRelativeRight) {
						myBody.AddForce ((airSpeed * 0.75f) * -cameraRelativeRight
							* Time.fixedDeltaTime * delta);	
					} else if (xVel.normalized == -cameraRelativeRight) {
						myBody.AddForce ((airSpeed * 0.75f) * cameraRelativeRight
							* Time.fixedDeltaTime * delta);
					}
				}

			}

			break;

		case "right":

			if (onFloorTracker > 0) {
				// on floor
				if (fullSpeed) {
					myBody.AddForce (floorSpeed * cameraRelativeRight *
						Time.fixedDeltaTime * delta);
				} else {
					myBody.AddForce ((floorSpeed - 75f) * cameraRelativeRight *
						Time.fixedDeltaTime * delta);
				}
			} else if(onFloorTracker == 0) {
				// in air
				if(xSpeed < airSpeed) {
					myBody.AddForce ((airSpeed * 0.75f) * cameraRelativeRight
						* Time.fixedDeltaTime * delta);
				}

				if (zSpeed > 0.1f) {
					if (zVel.normalized == cameraRelativeUp) {
						myBody.AddForce ((airSpeed * 0.75f) * -cameraRelativeUp
							* Time.fixedDeltaTime * delta);	
					} else if (zVel.normalized == -cameraRelativeUp) {
						myBody.AddForce ((airSpeed * 0.75f) * cameraRelativeUp
							* Time.fixedDeltaTime * delta);
					}
				}

			}

			break;

		case "left":

			if (onFloorTracker > 0) {
				// on floor
				if (fullSpeed) {
					myBody.AddForce (floorSpeed * -cameraRelativeRight *
						Time.fixedDeltaTime * delta);
				} else {
					myBody.AddForce ((floorSpeed - 75f) * -cameraRelativeRight *
						Time.fixedDeltaTime * delta);
				}
			} else if(onFloorTracker == 0) {
				// in air
				if(xSpeed < airSpeed) {
					myBody.AddForce ((airSpeed * 0.75f) * -cameraRelativeRight
						* Time.fixedDeltaTime * delta);
				}

				if (zSpeed > 0.1f) {
					if (zVel.normalized == cameraRelativeUp) {
						myBody.AddForce ((airSpeed * 0.75f) * -cameraRelativeUp
							* Time.fixedDeltaTime * delta);	
					} else if (zVel.normalized == -cameraRelativeUp) {
						myBody.AddForce ((airSpeed * 0.75f) * cameraRelativeUp
							* Time.fixedDeltaTime * delta);
					}
				}

			}

			break;

		}
	}

	void UpdateCameraRelativePosition() {

		// You need to understand the difference between local space and world space. 
		// Local is for directions and rotations relative to the object, 
		// and world is relative to the game world. 
		// This function takes a local direction from an object and finds 
		// that direction in world space. 
		// Vector3(1,0,0) is the same as Vector3.right, 
		// which in the object's local space is the direction 
		// pointing to the right of the object. 
		// Depending on how the object is rotated, the output in world space will change.

		// To understand TransformDirection, you must first understand the difference 
		// in World and Local space. 
		// Now, TransformDirection, as per the docs, transform a Vector from local 
		// space to world space. In the world space, vector (1,0,0) 
		// is one unit to the right of the origo, in local space, 
		// however that same vector is one step to the right based 
		// on the object's current rotation. 
		// What Transform direction does is it takes that relative movement, 
		// and returns how it is in relation to the origo.
		// You can see this effect for yourself if you imagine your monitor 
		// being the center of the world (0,0,0). 
		// The direction the back of your screen shows, 
		// is the positive z-axis(world forward), the direction the 
		// right edge of the screen is the positive x-axis (world right), 
		// and up is the positive y-axis (world up). 
		// Now, as long as you face your monitor, i.e. your(=local) forward is 
		// the same as the world (=your monitor) forward and your(=local) right 
		// is the same as the world right.
		// Now, if you turn so that your left is towards the monitor, 
		// the directions don't match anymore; your(=local) forward is suddenly 
		// pointing to the same direction as world right! 
		// But say you are blind and want to know which way you're actually 
		// facing in relation to the monitor. You'd need to do 
		// you.TransformDirection(you.forward) and that would return (1,0,0), 
		// which is equal to the world right.
		// The code you posted returns the x-axis (i.e. way right) for the current 
		// object in world space. The same can be achieved by simply using 
		// transform.right, because transform.direction is just shorthand for this translation.

		// EXPLANTION TO transform.TransformDirection
		// http://answers.unity3d.com/questions/506740/i-need-help-understanding-transformdirection.html

		cameraRelativeRight = mainCamera.transform.TransformDirection (Vector3.right);
		cameraRelativeUp = mainCamera.transform.TransformDirection (Vector3.forward);
		cameraRelativeUp.y = 0f;
		cameraRelativeUp = cameraRelativeUp.normalized;

		cameraRelativeUpRight = (cameraRelativeUp + cameraRelativeRight);
		cameraRelativeUpRight = cameraRelativeUpRight.normalized;

		cameraRelativeUpLeft = (cameraRelativeUp - cameraRelativeRight);
		cameraRelativeUpLeft = cameraRelativeUpLeft.normalized;


		// Think of it this way... a Vector holds 2 pieces of information - 
		// a point in space and a magnitude. 
		// The magnitude is the length of the line formed between (0, 0, 0) 
		// and the point in space. If you "normalize" a vector 
		// (also known as the "unit vector" - Google it), 
		// the result is a line that starts a (0, 0, 0) and "points" to your original 
		// point in space. If you were to take the length of this "pointer" 
		// it would equal 1 unit length

		// A Vector2 is either a point or a direction, 
		// depending on what you use it for. For example, if it's (5, 0), 
		// then it's either a point at x=5, y=0, or it's a vector pointing 
		// along the positive x axis with a slope of 0 and a length of 5. 
		// In this case it's not normalized since the length is greater than 1. 
		// If you normalize it, then it will become (1, 0) and will have a length of 1.

		// EXPLANATION OF NORMALIZED ALSO GOOGLE IT
		// https://forum.unity3d.com/threads/what-is-vector3-normalize.164135/
		// http://answers.unity3d.com/questions/47363/what-is-vector3normalized.html

	}

	void FullSpeedController() {
		if (direction != directionLastFrame) {
			if (direction == "") {
				StopCoroutine ("FullSpeedTimer");
				fullSpeed = false;
			} else if (directionLastFrame == "") {
				StartCoroutine ("FullSpeedTimer");
			}
		}
	}

	IEnumerator FullSpeedTimer() {
		yield return new WaitForSeconds (0.07f);
		fullSpeed = true;
	}

	void DragAdjustmentAndAirSpeed() {
		if (onFloorTracker > 0) {
			// on the floor
			myBody.drag = floorDrag;
		} else {
			// in air
			xVel = Vector3.Project(myBody.velocity, cameraRelativeRight);
			zVel = Vector3.Project(myBody.velocity, cameraRelativeUp);

			xSpeed = xVel.magnitude;
			zSpeed = zVel.magnitude;

			myBody.drag = airDrag;

			// EXPLANATION WHAT IS MAGNITUDE
			// The magnitude is the distance between the vector's origin (0,0,0) 
			// and its endpoint. If you think of the vector as a line, 
			// the magnitude is equal to its length.
			// With two vectors, a and b, then (a-b).magnitude is the distance between them.
			// That's what Vector3.Distance() does actually. 
			// Since rigidbody.velocity is a vector, 
			// then rigidbody.velocity.magnitude is how fast a rigidbody is going.
			// https://forum.unity3d.com/threads/what-is-vector3-magnitude.50125/
			// https://docs.unity3d.com/ScriptReference/Vector3-magnitude.html


		}
			
	}

	void BallFellDown() {
		if (transform.position.y < -30f) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}

	void OnCollisionEnter(Collision target) {
		if (target.gameObject.tag == "Floor") {
			onFloorTracker++;
		}
	}

	void OnCollisionExit(Collision target) {
		if (target.gameObject.tag == "Floor") {
			onFloorTracker--;
		}
	}

} // class
