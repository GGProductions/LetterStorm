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
	void OnTriggerEnter(Collider otherObj) { 
		
		if (otherObj.tag == "projectileTag") {
		GameObject go = Instantiate(theDrop, transform.position, Quaternion.Euler(270, 0, 0) ) as GameObject;
		go.name = theDrop.name;

		Destroy(otherObj.gameObject);
		Destroy(gameObject);  
	
	} 
	
	}


	
}
