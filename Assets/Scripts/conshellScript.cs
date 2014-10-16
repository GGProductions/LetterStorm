using UnityEngine;
using System.Collections;

public class conshellScript : MonoBehaviour {

	public GameObject conson;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider otherObj)
	{
		if (otherObj.tag == "projectileTag")
		{
			GameObject go = Instantiate(conson, transform.position, Quaternion.Euler(270, 0, 0)) as GameObject;
			go.name = conson.name;

			Destroy(otherObj.gameObject);
			Destroy(gameObject);

		} 

	}
}
