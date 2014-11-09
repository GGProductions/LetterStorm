using UnityEngine;
using System.Collections;

public class AoeBall : MonoBehaviour {

/// <summary>
/// starting scale must be larger than albert's capsule collider or elsd ther will be a collsion as soon as aoe is instanciated
/// </summary>
	void Start () {
		transform.localScale += new Vector3(3.2F, 3.2f, 3.2f);
	}

/// <summary>
/// grow over time 
/// </summary>
	void Update () {
	this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(20f, 20f, 20f), 0.10f);
		if (transform.localScale.x > 18f)
			Destroy(gameObject);
	}

}
