using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class Albert_force_controller : MonoBehaviour
{
	#region publics

	//All clips
	public AnimationClip walkAnimationClip;
	public AnimationClip idleAnimationClip;
	public AnimationClip fallAnimationClip;
	public AnimationClip wakBackAnimationClip;
	public AnimationClip throwAnimationClip;

	public GameObject ProjectilePrefab; //I should just put this projectile in /Resources and make this private as well..
	
	public string LetterBulletname="";
	public bool LetterMode = true;
#endregion

	#region privVar

	//the limits of mobility
	private float Limit_lefft = -7f;
	private float Limit_right = 7f;
	private float Limit_up = 6f;
	private float Limit_down = -4.9f;

	private GameObject Albert_explosion;

	private float playerspeed = 5f;
	//tilting variables
	private float smooth = 160.0F;
	private float tiltAngle = 50.0F;

	private Transform mytransform;
	private Transform myAmmoSpawn;

	private GameObject _aoeBall;

	private float Global_leftright;
	private float Global_updown;

	private float scaleFactorOfRworth=0.1f;
	private float OriginalScale = 0.5f;
	enum AlbertState
	{
		Playing,
		GotHit,
		Invincible
	}

	private AlbertState curr_state = AlbertState.Playing;

	private bool canwalk = true;
	#endregion


	void OnEnable()
	{
		Boss_3d_wordGen.OnMyGunsDied += ListenToBoss;
	}

	void OnDisable()
	{
		Boss_3d_wordGen.OnMyGunsDied -= ListenToBoss;
	}
	void ListenToBoss() { Debug.Log("Albert heard you"); LetterMode = true; }

	Quaternion Albert_originalRotation;
	/// <summary>
	/// loading all the animations. can be done in start() as well but i'm not sure what the difference is
	/// </summary>
	void Awake()
	{
		this.transform.localScale = new Vector3(OriginalScale, OriginalScale, OriginalScale);
		 Global_leftright=0f;
		 Global_updown=0f;
		Albert_originalRotation = this.transform.rotation;

		LetterBulletname = string.Empty;
		mytransform = this.transform;
		myAmmoSpawn = transform.Find("AmmoSpawnPoint");

		animation.wrapMode = WrapMode.Loop;
		animation.AddClip(walkAnimationClip, "walking");
		animation.AddClip(idleAnimationClip, "idleing");
		animation.AddClip(fallAnimationClip, "falling");
		animation.AddClip(wakBackAnimationClip, "walkingback");

		animation.wrapMode = WrapMode.Once;
		animation.AddClip(throwAnimationClip, "throwing");
	}

	

	/// <summary>
	/// only deal with checking input form keyboard, and Horizontal axis for tilting albert left to right down for walking backward
	/// </summary>
	void Update() {
		this.transform.localScale = new Vector3(OriginalScale + scaleFactorOfRworth, OriginalScale + scaleFactorOfRworth, OriginalScale + scaleFactorOfRworth);
		getKeyPressed(); //keep waiting for a key pressed to populate  string LetterBulletname

		 if    (curr_state == AlbertState.Playing) 
		 {
				 animation.Play("walking");
			   //  Debug.Log(rigidbody.velocity.normalized);
				 if (Input.GetKeyDown("space"))
				 {
					 animation.CrossFade("throwing");
					 doShooting();
				 }

				 float tiltAroundz = Input.GetAxis("Horizontal") * tiltAngle;
				 Quaternion target_rotation = Quaternion.Euler(0, tiltAroundz, 0);
				 mytransform.rotation = Quaternion.Slerp(mytransform.rotation, target_rotation, Time.deltaTime * smooth);

				 if (Input.GetKeyDown(KeyCode.DownArrow))
				{
				 //  Debug.Log("walking back now>?");
				   mytransform.rotation = Albert_originalRotation;
				} 
		 }	
	

	}


	void doShooting() {
	   
		Vector3 Load_position = new Vector3(myAmmoSpawn.position.x, myAmmoSpawn.position.y, myAmmoSpawn.position.z);

		if (!LetterMode)
		{
			// animation.CrossFade("throwing");
			fireApencil(Load_position);
		}
		else
		{
			if (LetterBulletname != string.Empty)
			{
				char input = LetterBulletname[0];
				
				fireAletter(Load_position, input);
			}

		}
	}

	/// <summary>
	/// instatciate the pecnist at locatoin
	/// </summary>
	/// <param name="fromwhere"></param>
	void fireApencil(Vector3 fromwhere)
	{
		// Fire projectile
		Albert_explosion = Instantiate(ProjectilePrefab, fromwhere, mytransform.rotation) as GameObject;
		Albert_explosion.rigidbody.AddForce(mytransform.forward * 1000.0f);
	}

	/// <summary>
	/// instantiate a letter projectile at location
	/// </summary>
	/// <param name="fromwhere"></param>
	/// <param name="input"></param>
	void fireAletter(Vector3 fromwhere, char input)
	{
		
		string CapitalLetter = input.ToString().ToUpper();


		Debug.Log("fiering a letter= " + CapitalLetter);
		if (Context.PlayerInventory.GetLetterCount(CapitalLetter) > 0)
	//		if (true)

		{
			Albert_explosion = Instantiate(Resources.Load("LettesProjectile/" + LetterBulletname), fromwhere, Quaternion.Euler(-90, 0, 0)) as GameObject;
			Albert_explosion.GetComponent<LetterProjectileScript>().isactive = true;// I can't change this here
			Albert_explosion.rigidbody.AddForce(mytransform.forward * 1000.0f);
			Context.PlayerInventory.take_letterAway(CapitalLetter);
		}

	}


	/// <summary>
	/// get keyboard press liste for all letters
	/// </summary>
	void getKeyPressed() {

        if (Input.GetKeyDown(KeyCode.A)) { LetterBulletname = "a_projectilePrefab"; Context.SelectedLetter = "A"; }
        if (Input.GetKeyDown(KeyCode.B)) { LetterBulletname = "b_projectilePrefab"; Context.SelectedLetter = "B"; }
        if (Input.GetKeyDown(KeyCode.C)) { LetterBulletname = "c_projectilePrefab"; Context.SelectedLetter = "C"; }
        if (Input.GetKeyDown(KeyCode.D)) { LetterBulletname = "d_projectilePrefab"; Context.SelectedLetter = "D"; }
        if (Input.GetKeyDown(KeyCode.E)) { LetterBulletname = "e_projectilePrefab"; Context.SelectedLetter = "E"; }
        if (Input.GetKeyDown(KeyCode.F)) { LetterBulletname = "f_projectilePrefab"; Context.SelectedLetter = "F"; }
        if (Input.GetKeyDown(KeyCode.G)) { LetterBulletname = "g_projectilePrefab"; Context.SelectedLetter = "G"; }
        if (Input.GetKeyDown(KeyCode.H)) { LetterBulletname = "h_projectilePrefab"; Context.SelectedLetter = "H"; }
        if (Input.GetKeyDown(KeyCode.I)) { LetterBulletname = "i_projectilePrefab"; Context.SelectedLetter = "I"; }
        if (Input.GetKeyDown(KeyCode.J)) { LetterBulletname = "j_projectilePrefab"; Context.SelectedLetter = "J"; }
        if (Input.GetKeyDown(KeyCode.K)) { LetterBulletname = "k_projectilePrefab"; Context.SelectedLetter = "K"; }
        if (Input.GetKeyDown(KeyCode.L)) { LetterBulletname = "l_projectilePrefab"; Context.SelectedLetter = "L"; }
        if (Input.GetKeyDown(KeyCode.M)) { LetterBulletname = "m_projectilePrefab"; Context.SelectedLetter = "M"; }
        if (Input.GetKeyDown(KeyCode.N)) { LetterBulletname = "n_projectilePrefab"; Context.SelectedLetter = "N"; }
        if (Input.GetKeyDown(KeyCode.O)) { LetterBulletname = "o_projectilePrefab"; Context.SelectedLetter = "O"; }
        if (Input.GetKeyDown(KeyCode.P)) { LetterBulletname = "p_projectilePrefab"; Context.SelectedLetter = "P"; }
        if (Input.GetKeyDown(KeyCode.Q)) { LetterBulletname = "q_projectilePrefab"; Context.SelectedLetter = "Q"; }
        if (Input.GetKeyDown(KeyCode.R)) { LetterBulletname = "r_projectilePrefab"; Context.SelectedLetter = "R"; }
        if (Input.GetKeyDown(KeyCode.S)) { LetterBulletname = "s_projectilePrefab"; Context.SelectedLetter = "S"; }
        if (Input.GetKeyDown(KeyCode.T)) { LetterBulletname = "t_projectilePrefab"; Context.SelectedLetter = "T"; }
        if (Input.GetKeyDown(KeyCode.U)) { LetterBulletname = "u_projectilePrefab"; Context.SelectedLetter = "U"; }
        if (Input.GetKeyDown(KeyCode.V)) { LetterBulletname = "v_projectilePrefab"; Context.SelectedLetter = "V"; }
        if (Input.GetKeyDown(KeyCode.W)) { LetterBulletname = "w_projectilePrefab"; Context.SelectedLetter = "W"; }
        if (Input.GetKeyDown(KeyCode.X)) { LetterBulletname = "x_projectilePrefab"; Context.SelectedLetter = "X"; }
        if (Input.GetKeyDown(KeyCode.Y)) { LetterBulletname = "y_projectilePrefab"; Context.SelectedLetter = "Y"; }
        if (Input.GetKeyDown(KeyCode.Z)) { LetterBulletname = "z_projectilePrefab"; Context.SelectedLetter = "Z"; }
	
	}



	void updateDirrection_fromKeypressed() {
	
   if(  Input.GetKey(KeyCode.UpArrow)) {
	   Global_updown = 1f;
	   if (Input.GetKey(KeyCode.RightArrow))  { Global_leftright = 1f;  }
	   if (Input.GetKey(KeyCode.LeftArrow)) { Global_leftright = -1f; }
   }
   else
	   if( Input.GetKey(KeyCode.DownArrow)){
		
		   Global_updown = -1f;
	   }
	   else
		   if (Input.GetKey(KeyCode.RightArrow)) {
			   Global_leftright = 1f;
			
		   }
		   else
			   if (Input.GetKey(KeyCode.LeftArrow)) {
				   Global_leftright = -1f;
			   }

			   else
			   { 
				 Global_leftright = 0f;
				 Global_updown = 0f;
			   }
	}




	/// <summary>
	/// keep albert inside the screen boundaries. must be used in FixedUpdate() since that is where the actual moving happens
	/// </summary>
	void keep_albert_within() {

		if (mytransform.position.x <= Limit_lefft)
			mytransform.position = new Vector3(Limit_lefft, transform.position.y, transform.position.z);
		else if (mytransform.position.x >= Limit_right)
			mytransform.position = new Vector3(Limit_right, transform.position.y, transform.position.z);

		// up and down player movement limitation
		if (mytransform.position.z > Limit_up)
			mytransform.position = new Vector3(gameObject.transform.position.x, transform.position.y, Limit_up);
		else if (mytransform.position.z < Limit_down)
			mytransform.position = new Vector3(gameObject.transform.position.x, transform.position.y, Limit_down);
	}

	/// <summary>
	/// this will deal with moving Albert's gidi body using Force and Velocity 
	/// Albert needs a rigid body to use power ups , because a power up need to register a collision with albert
	/// unlike all the other objects. those get a reaction from albert...
	/// </summary>
	void FixedUpdate() {

		if (Context.PlayerHealth.hasNoHealth()) Application.LoadLevel(3);
		if (curr_state == AlbertState.Playing)
		{

			updateDirrection_fromKeypressed();


		//	rigidbody.velocity = (Input.GetAxis("Horizontal") * mytransform.right + Input.GetAxis("Vertical") * mytransform.forward).normalized * playerspeed;
			rigidbody.velocity = (Global_leftright * mytransform.right + Global_updown * mytransform.forward).normalized * playerspeed;
			
			
			//this velocity must be changed into a globla velocity or else albert will not be able to strafe left and right while he is tilting
			// he would strafe in his local left and right      
			float xVal = rigidbody.velocity.x;
			Vector3 localVelo = mytransform.InverseTransformDirection(rigidbody.velocity);
			localVelo.x = 0.0f;
			Vector3 worldV = mytransform.TransformDirection(localVelo);
			worldV.x = xVal;
			rigidbody.velocity = worldV;

		}
		else
			rigidbody.velocity = Vector3.zero;
		


		//reset
		Global_leftright = 0f;
		Global_updown = 0f;

		keep_albert_within();
	}
	/// <summary>
	/// throw coroutine to have the full animation play 
	/// </summary>
	/// <returns></returns>
	IEnumerator DoThrow()
	{
		animation.Stop("walking");
		animation.CrossFade("throwing");
		yield return new WaitForSeconds(animation["throwing"].length);

	}

	void OnTriggerEnter(Collider otherObj)
	{
		//	Debug.Log(otherObj.tag + " is the tag registered");
		//just for funzies ..this will not stay unless we can see a good power up use
		scaleFactorOfRworth = scaleFactorOfRworth + 0.02f;

		if (otherObj.tag == "enemy" || otherObj.tag == "bossTag" || otherObj.tag == "bossProjectileTag")
		{
			curr_state = AlbertState.GotHit;
			//  Debug.Log("Vowel");
			Context.PlayerHealth.decreaseHealth(Context.DefaultPlayerHealthDecreaseFactor);
			Instantiate(Resources.Load("Explosions/blackStars1"),
									 transform.position,
									 Quaternion.Euler(-180, 0, 0));
		   
			StartCoroutine(Take_Damage_Routine());
		}


		//fyi, the pencil collids with albert when he instanciates it 
		// did we pickup a letter?
		if (otherObj.tag != "bossTag" && otherObj.tag != "bossProjectileTag" && otherObj.tag != "enemy" && otherObj.tag != "projectileTag")
		{
			if (otherObj.transform.GetComponent<SphereCollider>() != null)
			{
				otherObj.transform.GetComponent<SphereCollider>().isTrigger = true;
				Messenger<string>.Broadcast("picked up a letter", otherObj.name);   // Nabil: Added this so I can remove letters from my dict whenever Albert has picked them up to keep from overspawning
				// letters that he needs but already has -Paul
				Context.PlayerInventory.AddCollectedLetter(otherObj.name);
				GameObject go = otherObj.gameObject;
				Destroy(go);
			}

		}

		if (otherObj.tag == "letterProjectile")
		{
			//Debug.Log("name of letterpickedup " + otherObj.name);
			char input = otherObj.name[0];
			Messenger<string>.Broadcast("picked up a letter", otherObj.name);
			Context.PlayerInventory.AddCollectedLetter(input.ToString().ToUpper());

		}
		else
		{
			// animation.CrossFade("falling");
            // Context.PlayerHealth.decreaseHealth(Context.DefaultPlayerHealthDecreaseFactor);
		}

	}

	IEnumerator doFallanimation() {
		//Debug.Log("takingDama");

		canwalk = false;
		animation.CrossFade("falling");

		curr_state = AlbertState.Invincible;
	   // this.transform.GetComponent<CapsuleCollider>().enabled = false;
		float amtToMove = 50f * Time.deltaTime;
	   // if (mytransform.position.z > -2f)
	   // {
			//transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - amtToMove);
	   // }


		// curr_state = AlbertState.Invincible;
		yield return new WaitForSeconds( 1.5f * animation["falling"].length);
	}

	/// <summary>
	/// state machine 
	/// </summary>
	/// <returns></returns>
	IEnumerator Take_Damage_Routine()
	{

		yield return StartCoroutine(doFallanimation());
	
		/*
		Debug.Log("takingDama");
		animation.Stop("walking");
		animation.CrossFade("falling");

		curr_state = AlbertState.GotHit;
		this.transform.GetComponent<CapsuleCollider>().enabled = false;
		float amtToMove = 50f * Time.deltaTime;
		if (mytransform.position.z > -2f)
		{
			//transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - amtToMove);
		}
		*/
		
	   // curr_state = AlbertState.Invincible;
		//yield return new WaitForSeconds(2 * animation["falling"].length);
		
		//yield return new WaitForSeconds(1f);

		if (Context.PlayerHealth.hasHealth())
		{
			this.transform.GetComponent<CapsuleCollider>().enabled = true;
			curr_state = AlbertState.Playing;
		}

		yield return 0;
	}





}