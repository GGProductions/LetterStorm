using UnityEngine;
using System.Collections;


public class Vowel : MonoBehaviour {

	public GameObject theDrop;

	// Use this for initialization
	void Start () {
		this.transform.GetComponent<BoxCollider>().enabled = true;
		this.transform.GetComponent<BoxCollider>().isTrigger = true;
	}
	

	void OnTriggerEnter(Collider otherObj) { 
		
		if (otherObj.tag == "projectileTag") {
		GameObject go = Instantiate(theDrop, transform.position, Quaternion.Euler(270, 0, 0) ) as GameObject;
		go.name = theDrop.name;
		//keep the follwoing line if we want vowels to destroy aoe . pecile gets auto destroyed.
		//Destroy(otherObj.gameObject);
		Destroy(gameObject);  
	
	} 
	
	}


	
}
