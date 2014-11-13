using UnityEngine;
using System.Collections;

public class Boss_Cannon_script : MonoBehaviour
{

	#region publics
	/// <summary>
	/// damp factor that controls how fast aiming at albert is to be done.
	/// damp = 1.8f ----> easy  mode , long lag 
	/// damp = 3.0f ----> medium  mode , medium lag 
	/// damp = 10f ----> Hard  mode , very short lag,  
	/// </summary>
   public float cannonAngleDamp = 3f; //med

	#endregion

   #region privates
   /// <summary>
	///  Maximum amount of hits needed to kill the gun, this script is also attached to bossEey, but no worries: BossEye's collider is turned off
	/// </summary>
   private int MaxHits = 5;

   private int number_of_cannon_attached;
   private Vector3 player_location_transform;
   private Quaternion angleNeeded;
   private int hitcount;
   private GameObject theBoss;
   private Boss_3d_wordGen bossWordGenScript;
   private Transform cannonText_trans;

   private Transform Boss_body; //Will use this to get a ref to the boss+3d_wordgen by going up the chain of parents till i find the root. look in Start()

   private GameObject smaoke;
   private Transform myparentBone;


   #endregion


   /// <summary>
	/// intitialize the hitcount to 0 
	/// </summary>
	void Awake()
	{
		hitcount = 0;
	}

	/// <summary>
	/// on start get a ref to :
	/// 1) cannonText label
	/// 2) The Boss of this gun 
	/// </summary>
	void Start()
	{
		myparentBone = transform.parent;

		cannonText_trans = transform.FindChild("CannonHealthLabel");
		//run up the chain of parents :) 
		Boss_body = this.transform;
		while (Boss_body.GetComponent<Boss_3d_wordGen>() == null)
		{
			Boss_body = Boss_body.parent;
			//Debug.Log("Iam" + parentalunit);
		}
		//Debug.Log("Iam LAST " + parentalunit);
		//the boss body was found , now get the script that's attached to it
		bossWordGenScript = Boss_body.GetComponent<Boss_3d_wordGen>();

	}


	/// <summary>
	/// update the cannon's text lable
	/// update the rotation of the gun only  aiming at albert 
	/// </summary>
	void Update()
	{
		
		cannonText_trans.GetComponent<TextMesh>().text = (MaxHits - hitcount + 1).ToString();

		//locating Albert must be done continuously 
		player_location_transform = GameObject.Find("AlbertPlayerPrefab").transform.position;
		angleNeeded = Quaternion.LookRotation(player_location_transform - this.transform.position);

		//tilt the gun and aim acording to the damp factor for speed of rotation
		transform.rotation = Quaternion.Slerp(this.transform.rotation, angleNeeded, Time.deltaTime * cannonAngleDamp);

		if (hitcount > MaxHits)
		{
			//Let the boss know that this gun has died. Boss will deal with allerting allbert if the required amount of guns have died.
			bossWordGenScript.AcannonHasDied();
			GameObject go = Instantiate(Resources.Load("Explosions/smallsmoke"),
										  myparentBone.transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;

			go.transform.parent = myparentBone;
			Destroy(gameObject);
		}

	}
	 
	/// <summary>
	/// Register hits from  Pencils tagged "projectileTag" or "letterprojectile" <--- the latter may not be needed ... keep for now
	/// </summary>
	/// <param name="otherObj"></param>
	void OnTriggerEnter(Collider otherObj)
	{     
		if (otherObj.tag == "projectileTag" || otherObj.tag == "letterProjectile")
		{
			hitcount++;
		}

	}
	
}
