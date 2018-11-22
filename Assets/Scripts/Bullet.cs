using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject player;
	public float speed = 5f;

	// Use this for initialization
	void Start () {
		// GetComponent<Rigidbody2D>().velocity = transform.right * -1 * speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnBecameInvisible() {
		Destroy(gameObject);
	}
}
