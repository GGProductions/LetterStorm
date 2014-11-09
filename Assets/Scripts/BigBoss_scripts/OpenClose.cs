using UnityEngine;
using System.Collections;

public class OpenClose : MonoBehaviour {


	public Transform slide1;
	public Transform slide2;
	public Transform slide3;

	GameObject LeftClaw;
	GameObject pivitL;
	GameObject cursor2;
	Quaternion rotation1;
	Quaternion rotation2;

	public Transform from;
	public Transform to;
   // public float speed = 0.1F;
	public float speed = 0.1F;
	// Use this for initialization
	void Awake () {
		//rotation1 = new Quaternion(0.0f, -4.0f, 0.0f,0.9f); //making a quaterinon with hard coded values... if you're bored try to read about quaternion rotations  it's prety complicated ...
		LeftClaw = GameObject.Find("Cube_bigboss_Claw_L"); //finding left claw .. this object will look at all the objects in the scene and find the one called "Cube_bigboss_Claw_L". the other way would be to locate the claww wia children of this.transform. as you can see this script is attached to Cube_Bigboss, and Cube big boss has transform children. we can start looking in the chain of childer objects of this.transform. this is more efficient when your game has a shit load of objects and you don't want to search all of them but rather only search in the chain of children object 
		pivitL = GameObject.Find("Pivot_Claw_L");
	cursor2 = GameObject.Find("pivot_center_arrow_R_2");
	rotation2 = cursor2.transform.rotation;
	}



	IEnumerator Start()
	{
		//Vector3 pointA_init = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		while (true)
		{
			yield return StartCoroutine(MoveObject(transform, slide1.position, slide2.position, 3.0f));
			yield return StartCoroutine(MoveObject(transform, slide2.position, slide3.position, 3.0f));
			yield return StartCoroutine(MoveObject(transform, slide3.position, slide2.position, 3.0f));
			yield return StartCoroutine(MoveObject(transform, slide2.position, slide1.position, 3.0f));
		}
	}
	IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
	{
		var i = 0.0f;
		var rate = 1.0f / time;
		while (i < 1.0f)
		{
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}


	// Update is called once per frame
	void Update () {
		Debug.Log(to.rotation);
		//rotation2.y = Mathf.Clamp(Time.time, -30f, 30f);
		//transform.rotation = rotation2;
	LeftClaw.transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed);//look in the scene you will find 2 giant arrows " Cursor_3d_dirrectional" . each has an initial rotation stored in the form of quaternion rotation. the lerp function will rotate leftclaw.transform from the rotation of the first cursor3d to the rotation of the second cursor32
	

		//	LeftClaw.transform.rotation = Quaternion.Lerp(from.rotation, rotation1, Time.time * speed);
	//	float rotationSpeed = 100 * Time.deltaTime;
	//	pivitL.transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed);

	   
	}
}
