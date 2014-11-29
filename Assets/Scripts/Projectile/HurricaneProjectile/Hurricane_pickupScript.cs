using UnityEngine;
using System.Collections;

public class Hurricane_pickupScript : MonoBehaviour {

	#region Public Variables ---------------------------------------------

	
	#endregion Public Variables ------------------------------------------

	#region Private Variables ---------------------------------------------
	private float currentRotationSpeed;
	float timeElapsed = 0;
	#endregion Private Variables ------------------------------------------

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(Vector3.back * 1.2f * Time.deltaTime, Space.World);
		this.transform.Rotate(new Vector3(0, 0, 2) * currentRotationSpeed * Time.deltaTime);

		timeElapsed += Time.deltaTime * 5;
		float factor = Mathf.Abs(Mathf.Cos(timeElapsed) * 0.5f) + 0.5f;

		this.transform.localScale = new Vector3(factor, 1, factor);

		if (transform.position.z < -5f || transform.position.z > 9 || transform.position.x < -10 || transform.position.x > 10)
			Destroy(gameObject);
	}

	
	  void OnTriggerEnter(Collider otherObj){
		  if (otherObj.tag == "albertTag")
		  {
			  Instantiate(Resources.Load("PowerUpsResources/HurricanePrefab"),
											transform.position,
											Quaternion.Euler(-180, 0, 0));
			  Destroy(gameObject);
		  }
		}

	
	
	
}
