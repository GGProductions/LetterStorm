using UnityEngine;
using System.Collections;

public class AlbertProjectile : MonoBehaviour {

	public GameObject boom;
	private float currentRotationSpeed;
	// Use this for initialization
	void Start () {
		currentRotationSpeed = 300f;
	}
	
	// Update is called once per frame
	void Update () {
		float rotationSpeed = currentRotationSpeed * Time.deltaTime;
		transform.Rotate(new Vector3(0, 0, 2) * rotationSpeed);

	  //  transform.Translate(new Vector3(0, 0, 0.5f), Space.World);

		//rigidbody.AddForce(Vector3.forward * 10);

		if (transform.position.z > 20)
			Destroy(gameObject);
	
	}



	void OnTriggerEnter(Collider otherObj)
	{





        if (otherObj.tag == "enemy" || otherObj.tag == "bossTag")
        {
             Instantiate(boom, transform.position, Quaternion.Euler(270, 0, 0)) ;
            Destroy(gameObject);
       

        } 




	}



}
