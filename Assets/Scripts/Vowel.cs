using UnityEngine;
using System.Collections;

public class Vowel : MonoBehaviour {

	public GameObject theDrop;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider otherObj) { if (otherObj.tag == "projectileTag") {
		Instantiate(theDrop, transform.position, transform.rotation);


		Destroy(gameObject);   } 
	
	}


	
}
