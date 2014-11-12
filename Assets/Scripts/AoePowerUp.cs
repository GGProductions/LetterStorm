using UnityEngine;
using System.Collections;

public class AoePowerUp : MonoBehaviour {

	public  GameObject aoe;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider otherObj)
	{
		//Debug.Log("in");
		if (otherObj.tag == "albertTag")
		{
		//	Debug.Log("in albert");
			Instantiate(aoe, this.transform.position, this.transform.rotation);
			//Destroy(gameObject);
		}



	}

}
