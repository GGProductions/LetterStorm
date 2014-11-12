using UnityEngine;
using System.Collections;

public class AlbertProjectile : MonoBehaviour {

	public GameObject PencilExplosion;
	private float currentRotationSpeed;

	/// <summary>
	/// initialize the pencils's rotation speed to 300... may need to be smaller 
	/// </summary>
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
	/// pencil reacts to only two types of adverse tags: enemy and bossTag
	/// </summary>
	/// <param name="otherObj"></param>
	void OnTriggerEnter(Collider otherObj)
	{
		if (otherObj.tag == "enemy" || otherObj.tag == "bossTag")
		{
			Instantiate(PencilExplosion, transform.position, Quaternion.Euler(270, 0, 0)) ;
			Destroy(gameObject);  
		} 
	}
}
