using UnityEngine;
using System.Collections;

public class AlbertProjectile : MonoBehaviour {

	public GameObject boom;
	private float currentRotationSpeed;

	void Start () {
		currentRotationSpeed = 300f;
	}
	
/// <summary>
/// spin only, albert is responcible for adding foce to this gameobject to make it move in his local forward direction
/// </summary>
	void Update () {
		float rotationSpeed = currentRotationSpeed * Time.deltaTime;
		transform.Rotate(new Vector3(0, 0, 2) * rotationSpeed);
		if (transform.position.z > 20)
			Destroy(gameObject);
	}

/// <summary>
/// only two types of adverse tags: enemy and bossTag
/// </summary>
/// <param name="otherObj"></param>
	void OnTriggerEnter(Collider otherObj)
	{
		if (otherObj.tag == "enemy" || otherObj.tag == "bossTag")
		{
			Instantiate(boom, transform.position, Quaternion.Euler(270, 0, 0)) ;
			Destroy(gameObject);  
		} 
	}
}
