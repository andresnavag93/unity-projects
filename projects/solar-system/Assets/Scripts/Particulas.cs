using UnityEngine;
using System.Collections;

public class Particulas : MonoBehaviour {

	public float startRotation;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		this.GetComponent<ParticleSystem>().startRotation = -360f * Mathf.PI / 180f;
	}
}