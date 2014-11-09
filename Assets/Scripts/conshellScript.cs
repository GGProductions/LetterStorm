using UnityEngine;
using System.Collections;

public class conshellScript : MonoBehaviour {

	public GameObject conson;

/// <summary>
/// make a drop and disapear 
/// </summary>
/// <param name="otherObj"></param>
	void OnTriggerEnter(Collider otherObj)
	{



		if (otherObj.tag == "projectileTag")
		{
			GameObject go = Instantiate(conson, transform.position, Quaternion.Euler(270, 0, 0)) as GameObject;
			go.name = conson.name;
			Destroy(gameObject);
		}


	}
}
