using UnityEngine;
using System.Collections;

public class LetterCollect : MonoBehaviour {




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider otherObj) { if (otherObj.tag == "albertTag") { Destroy(gameObject);} }

}
