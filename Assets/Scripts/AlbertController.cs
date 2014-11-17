using UnityEngine;
using System.Collections;

public class AlbertController : MonoBehaviour {


	public float PlayerSpeed=5f;
	public float rotateSpeed = 40.0f;

	private Transform mytransform;
	private Transform myAmmoSpawn;
	public AnimationClip walkAnimationClip;
	public AnimationClip idleAnimationClip;
	public AnimationClip fallAnimationClip;

	public float smooth = 110.0F;
	public float tiltAngle = 50.0F;

	public GameObject ProjectilePrefab;

	void Awake(){

		mytransform= this.transform ;
		myAmmoSpawn = transform.Find("AmmoSpawnPoint");
		animation.AddClip (walkAnimationClip,"walking");
		animation.AddClip (idleAnimationClip,"idleing");
		animation.AddClip (fallAnimationClip,"falling");
	}
	
	void Start (){
		animation.wrapMode = WrapMode.Loop;
		animation["idleing"].layer = 0;
		animation["walking"].layer = 0;
		animation["falling"].layer = 0;
	}
	private GameObject poof;
	void Update() {

		if (Input.GetKeyDown("space"))
		{
			// Fire projectile
			Vector3 position = new Vector3(myAmmoSpawn.position.x, myAmmoSpawn.position.y, myAmmoSpawn.position.z);
			poof =Instantiate(ProjectilePrefab, position, this.transform.rotation) as GameObject;
			poof.rigidbody.AddForce( transform.forward * 1000.0f);
		}

		float leftRight = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
		float forwardBackward = Input.GetAxis("Vertical") *  PlayerSpeed * Time.deltaTime ;


		if (leftRight != 0 && forwardBackward != 0)
		{

			//slw speed


		}

		else

		if(leftRight != 0 || forwardBackward != 0){
			animation.CrossFade("walking");
			// Move the player
			mytransform.Translate(Vector3.right * leftRight,Space.World);
			mytransform.Translate(Vector3.forward * forwardBackward);

			//tilting
			float tiltAroundz = Input.GetAxis("Horizontal") * tiltAngle;      
			Quaternion target = Quaternion.Euler(0,tiltAroundz,0);
			transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);


		   

		}
		else
		{
			animation.CrossFade("walking");
			//animation.CrossFade("idleing");
			//mytransform.Translate(Vector3.back * 0.018f);
		}


		if (mytransform.position.x <= -7f)
			mytransform.position = new Vector3(-7f, transform.position.y, transform.position.z);
		else if (mytransform.position.x >= 7)
			mytransform.position = new Vector3(7f, transform.position.y, transform.position.z);
		
		// up and down player movement limitation
		if (mytransform.position.z >6f)
			mytransform.position = new Vector3(gameObject.transform.position.x, transform.position.y, 6f);
		else if (mytransform.position.z <-4.9f)
			mytransform.position = new Vector3(gameObject.transform.position.x,transform.position.y, -4.9f);


	}


	void OnTriggerEnter(Collider otherObj) {

		Context.PlayerInventory.AddCollectedLetter(otherObj.name);
		GameObject go = otherObj.gameObject;

		Destroy(go);


		//if (otherObj.name == "A") {
		//	Debug.Log("yes A");
	//	}
	//	Debug.Log("name is" + otherObj.name);

	}
	
	
}
