using UnityEngine;
using System.Collections;

public class CollectibleChar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.back * 1.2f * Time.deltaTime, Space.World);
	}
}
