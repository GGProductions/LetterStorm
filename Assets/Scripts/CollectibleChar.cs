using UnityEngine;
using System.Collections;

public class CollectibleChar : MonoBehaviour {


	public bool wasThrwon;
	// Use this for initialization
	void Start () {
		wasThrwon = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.back * 1.2f * Time.deltaTime, Space.World);
		if (transform.position.z < -6f) Destroy(gameObject);
	}

}
