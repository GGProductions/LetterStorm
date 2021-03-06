﻿using UnityEngine;
using System.Collections;
using GGProductions.LetterStorm.InGameHelpClasses;

// TODO: give albert invincibility timer on wakeup from hit
// also letter projectile need to be triggered off if boss loaded is bigboss  while bigboss is in closedclaw stated 
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
	public GameObject SecondaryProjectilePrefab; //I should just put this projectile in /Resources and make this private as well..
	public GameObject HurricaneProjectile; //same with this

	public string LetterBulletname = "";
	public bool LetterMode = true;
	

	#endregion

	#region privVar

	//the limits of mobility
	private float Limit_lefft = -8.6f;
	private float Limit_right = 8.6f;
	private float Limit_up = 7f;
	private float Limit_down = -4.4f;

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

	private float scaleFactorOfRworth = 0.1f;
	private float OriginalScale = 1f;

	enum AlbertState
	{
		Playing,
		GotHit,
		Invincible
	}

	private AlbertState curr_state = AlbertState.Playing;

	private bool canwalk = true;
	#endregion

	private Transform MeshObject;
	private SkinnedMeshRenderer SMR;
	private float THETIME = 0f;

	private float blinkRate = 0.1f;
	private int numberOfTimesToBlink = 10;
	private int blinkCount = 0;

	void OnEnable()
	{
		Boss_3d_wordGen.OnMyGunsDied += ListenToBoss;
	}

	void OnDisable()
	{
		Boss_3d_wordGen.OnMyGunsDied -= ListenToBoss;
	}
	void ListenToBoss()
	{
		// Debug.Log("Albert heard you"); 
		LetterMode = true;
	}

	Quaternion Albert_originalRotation;

    public AudioSource MusicAudioSource;
	/// <summary>
	/// loading all the animations. can be done in start() as well but i'm not sure what the difference is
	/// </summary>
	void Awake()
	{
		MeshObject = this.transform.GetChild(1).GetChild(0);
		SMR = MeshObject.GetComponent<SkinnedMeshRenderer>();
		this.transform.localScale = new Vector3(OriginalScale, OriginalScale, OriginalScale);
		Global_leftright = 0f;
		Global_updown = 0f;
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


        MusicAudioSource = (AudioSource)gameObject.AddComponent("AudioSource");
        myAudioclip = (AudioClip)Resources.Load("SFX/albertOuch");
        
	}

	/// <summary>
	/// only deal with checking input form keyboard, and Horizontal axis for tilting albert left to right down for walking backward
	/// </summary>
	void Update()
	{
		//  Debug.Log(MeshObject.name);
		THETIME += Time.deltaTime;

		//Debug.Log("update " +curr_state.ToString() + THETIME);

		this.transform.localScale = new Vector3(OriginalScale + scaleFactorOfRworth, OriginalScale + scaleFactorOfRworth, OriginalScale + scaleFactorOfRworth);
		getKeyPressed(); //keep waiting for a key pressed to populate  string LetterBulletname

		if (curr_state != AlbertState.GotHit)
		{
			//this.transform.GetComponent<CapsuleCollider>().enabled = true;
			animation.Play("walking");
			//  Debug.Log(rigidbody.velocity.normalized);
			if (Input.GetKeyDown("space"))
			{
				animation.CrossFade("throwing");
				doShooting();
			}


			if (Input.GetAxis("Vertical") > -0.1)
			{
				float tiltAroundz = Input.GetAxis("Horizontal") * tiltAngle;
				Quaternion target_rotation = Quaternion.Euler(0, tiltAroundz, 0);
				mytransform.rotation = Quaternion.Slerp(mytransform.rotation, target_rotation, Time.deltaTime * smooth);
			}
			else
			{
				mytransform.rotation = Albert_originalRotation;
				// Debug.Log("doing the else");
			}

			// mytransform.rotation = Quaternion.Slerp(mytransform.rotation, Quaternion.identity, 1);

			/*
						if (Input.GetKeyDown(KeyCode.DownArrow))
						{
							//  Debug.Log("walking back now>?");
							mytransform.rotation = Albert_originalRotation;
						}
						*/
		}
		else if (curr_state == AlbertState.Invincible)
		{
			//   this.transform.GetComponent<CapsuleCollider>().enabled = false;
		}


	}

	void FixedUpdate()
	{

		if (Context.PlayerHealth.HasNoHealth()) Application.LoadLevel("Lose");
		if (curr_state == AlbertState.Playing || curr_state == AlbertState.Invincible)
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


	#region one

	void doShooting()
	{

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
	/// Fire projectile at specified location
	/// </summary>
	/// <param name="fromwhere"></param>
	void fireApencil(Vector3 fromwhere)
	{
		if (Context.PlayerInventory.HasPowerUp("DualPencils") &&
			Context.PlayerInventory.DualPencil.HasExpired() == false)
		{
			// Fire dual pencil projectile
			GameObject ProjectileGameObject = Instantiate(SecondaryProjectilePrefab, fromwhere, mytransform.rotation) as GameObject;

			// Make sure Game Object has children (the two pencils)
			if (ProjectileGameObject.transform.childCount > 0)
			{
				// Get the transform of each pencil projectile
				Transform pencil1 = ProjectileGameObject.transform.GetChild(0);
				Transform pencil2 = ProjectileGameObject.transform.GetChild(1);

				// Propel each of the pencil projetiles
				pencil1.gameObject.rigidbody.AddForce(new Vector3(-0.75f, 0, 1) * 500.0f);
				pencil2.gameObject.rigidbody.AddForce(new Vector3(0.75f, 0, 1) * 500.0f);
				//ProjectileGameObject.rigidbody.AddForce(mytransform.forward * 500.0f);

				//pencil1.gameObject.rigidbody.AddForce(mytransform.forward * 500.0f);
				//pencil2.gameObject.rigidbody.AddForce(mytransform.forward * 500.0f);
			}
		}
		else
		{
			// Fire single pencil projectile
			GameObject ProjectileGameObject = Instantiate(ProjectilePrefab, fromwhere, mytransform.rotation) as GameObject;

			ProjectileGameObject.rigidbody.AddForce(mytransform.forward * 700.0f);
		}

	}

	/// <summary>
	/// instantiate a letter projectile at location
	/// </summary>
	/// <param name="fromwhere"></param>
	/// <param name="input"></param>
	void fireAletter(Vector3 fromwhere, char input)
	{
		string CapitalLetter = input.ToString().ToUpper();

		//	Debug.Log("fiering a letter= " + CapitalLetter);
		if (Context.PlayerInventory.GetLetterCount(CapitalLetter) > 0)
		{
			GameObject go = Instantiate(Resources.Load("LettesProjectile/" + LetterBulletname), fromwhere, Quaternion.Euler(-90, 0, 0)) as GameObject;
			go.GetComponent<LetterProjectileScript>().isactive = true;// I can't change this here
			go.rigidbody.AddForce(mytransform.forward * 1000.0f);
			//go.collider.isTrigger = false;
			Context.PlayerInventory.DecrementLetter(CapitalLetter);
		}
	}


	/// <summary>
	/// get keyboard press liste for all letters
	/// </summary>
	void getKeyPressed()
	{

		if (Context.SelectedLetter == "A") { LetterBulletname = "a_projectilePrefab"; }
		if (Context.SelectedLetter == "B") { LetterBulletname = "b_projectilePrefab"; }
		if (Context.SelectedLetter == "C") { LetterBulletname = "c_projectilePrefab"; }
		if (Context.SelectedLetter == "D") { LetterBulletname = "d_projectilePrefab"; }
		if (Context.SelectedLetter == "E") { LetterBulletname = "e_projectilePrefab"; }
		if (Context.SelectedLetter == "F") { LetterBulletname = "f_projectilePrefab"; }
		if (Context.SelectedLetter == "G") { LetterBulletname = "g_projectilePrefab"; }
		if (Context.SelectedLetter == "H") { LetterBulletname = "h_projectilePrefab"; }
		if (Context.SelectedLetter == "I") { LetterBulletname = "i_projectilePrefab"; }
		if (Context.SelectedLetter == "J") { LetterBulletname = "j_projectilePrefab"; }
		if (Context.SelectedLetter == "K") { LetterBulletname = "k_projectilePrefab"; }
		if (Context.SelectedLetter == "L") { LetterBulletname = "l_projectilePrefab"; }
		if (Context.SelectedLetter == "M") { LetterBulletname = "m_projectilePrefab"; }
		if (Context.SelectedLetter == "N") { LetterBulletname = "n_projectilePrefab"; }
		if (Context.SelectedLetter == "O") { LetterBulletname = "o_projectilePrefab"; }
		if (Context.SelectedLetter == "P") { LetterBulletname = "p_projectilePrefab"; }
		if (Context.SelectedLetter == "Q") { LetterBulletname = "q_projectilePrefab"; }
		if (Context.SelectedLetter == "R") { LetterBulletname = "r_projectilePrefab"; }
		if (Context.SelectedLetter == "S") { LetterBulletname = "s_projectilePrefab"; }
		if (Context.SelectedLetter == "T") { LetterBulletname = "t_projectilePrefab"; }
		if (Context.SelectedLetter == "U") { LetterBulletname = "u_projectilePrefab"; }
		if (Context.SelectedLetter == "V") { LetterBulletname = "v_projectilePrefab"; }
		if (Context.SelectedLetter == "W") { LetterBulletname = "w_projectilePrefab"; }
		if (Context.SelectedLetter == "X") { LetterBulletname = "x_projectilePrefab"; }
		if (Context.SelectedLetter == "Y") { LetterBulletname = "y_projectilePrefab"; }
		if (Context.SelectedLetter == "Z") { LetterBulletname = "z_projectilePrefab"; }
	}

	void updateDirrection_fromKeypressed()
	{

		if (Input.GetKey(KeyCode.UpArrow))
			Global_updown = 1f;
		if (Input.GetKey(KeyCode.DownArrow))
			Global_updown = -1f;
		if (Input.GetKey(KeyCode.RightArrow))
			Global_leftright = 1f;
		if (Input.GetKey(KeyCode.LeftArrow))
			Global_leftright = -1f;
	}




	/// <summary>
	/// keep albert inside the screen boundaries. must be used in FixedUpdate() since that is where the actual moving happens
	/// </summary>
	void keep_albert_within()
	{

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

	 void TakeABeating()
    {
            MusicAudioSource.clip = myAudioclip;
            MusicAudioSource.Play();
			//Debug.Log("collisioat " + THETIME);
			curr_state = AlbertState.GotHit;
			//  Debug.Log("Vowel");
			Context.PlayerHealth.DecreaseHealth();
			Instantiate(Resources.Load("Explosions/blackStars1"),
									 transform.position,
									 Quaternion.Euler(-180, 0, 0));
			StartCoroutine(doFallanimation1());
			StartCoroutine(doFallanimation2());

		}

	void GainHealth()
	{
		Context.PlayerHealth.IncreaseHealth();
	//	Instantiate(Resources.Load("Explosions/blackStars1"),
		//            transform.position,
		//            Quaternion.Euler(-180, 0, 0));
	}



      AudioClip myAudioclip;

	void OnTriggerEnter(Collider otherObj)
	{

	  // old_Trigerenter(otherObj);

		if (otherObj.tag == "bossTag" || 
			otherObj.tag == "bossProjectileTag" || 
			otherObj.tag == "enemy" ||
			 otherObj.tag == "smartProjectile" ||
			 otherObj.tag == "clawTag"
			)
		{
		//	Debug.Log("ran intoboss");
			TakeABeating();
		}

		if (otherObj.tag == "dualpencilProjectile") {
			Context.PlayerInventory.IncrementPowerUp("DualPencils");
			Destroy(otherObj);
		}

		if (otherObj.tag == "poisonousMushroomPowerUp"){
			TakeABeating();
		}
		//HeathShroomTag

		if (otherObj.tag == "HeathShroomTag"){
			GainHealth();
		}

		if (otherObj.tag == "slowDown") {
			Messenger<string>.Broadcast("slowing down time", otherObj.name);
		}


		if (otherObj.tag == "letterPickup")
		{


			otherObj.transform.GetComponent<SphereCollider>().isTrigger = true;
			Messenger<string>.Broadcast("picked up a letter", otherObj.name);   // Nabil: Added this so I can remove letters from my dict whenever Albert has picked them up to keep from overspawning
			// Grant the user points for collecting the letter
			Context.CurrentScore.Increase(ScoreKeeper.PlayerAchievement.CollectLetter);

			char input = otherObj.name[0];
			Messenger<string>.Broadcast("picked up a letter", otherObj.name);
			Context.PlayerInventory.AddCollectedLetter(input.ToString().ToUpper());
			Destroy(otherObj.gameObject);
		}
	}



	void old_Trigerenter(Collider otherObj)
	{

		//fyi, the pencil collides with albert when he instantiates it 
		// did we pickup a letter?
		if (otherObj.tag != "bossTag" && otherObj.tag != "bossProjectileTag" && otherObj.tag != "enemy" && otherObj.tag != "projectileTag")
		{
			if (otherObj.transform.GetComponent<SphereCollider>() != null)
			{
				otherObj.transform.GetComponent<SphereCollider>().isTrigger = true;
				Messenger<string>.Broadcast("picked up a letter", otherObj.name);   // Nabil: Added this so I can remove letters from my dict whenever Albert has picked them up to keep from overspawning
				// letters that he needs but already has -Paul
				Context.PlayerInventory.AddCollectedLetter(otherObj.name);

				// Grant the user points for collecting the letter
				Context.CurrentScore.Increase(ScoreKeeper.PlayerAchievement.CollectLetter);

				GameObject go = otherObj.gameObject;


				Destroy(go);
				Debug.Log("itshappeneing");
			}

		}
		else
			TakeABeating();

		if (otherObj.tag == "letterProjectile")
		{
			Debug.Log("itshappeneing again");
			//Debug.Log("name of letterpickedup " + otherObj.name);
			char input = otherObj.name[0];
			Messenger<string>.Broadcast("picked up a letter", otherObj.name);
			Context.PlayerInventory.AddCollectedLetter(input.ToString().ToUpper());

		}


		if (otherObj.tag == "dualpencilProjectile")
		{
			Context.PlayerInventory.IncrementPowerUp("DualPencils");
			Destroy(otherObj);
		}

		if (otherObj.tag == "poisonousMushroomPowerUp")
		{
			TakeABeating();
		}

	//	if (otherObj.tag == "HealthShroomPowerUp")
	//	{
	//		GainHealth();
//		}
	//
		if (otherObj.tag == "slowDown")
		{
			Messenger<string>.Broadcast("slowing down time", otherObj.name);
		
		}
	}


	#endregion



	IEnumerator doFallanimation1()
	{
		//	Debug.Log("in dofall1 " + THETIME);

		canwalk = false;
		animation.CrossFade("falling");


		//Debug.Log("GOThitt in fall1 at " + THETIME);
		this.transform.GetComponent<CapsuleCollider>().enabled = false;
		yield return new WaitForSeconds(1.5f * animation["falling"].length);

		curr_state = AlbertState.Invincible;
	}




	IEnumerator doFallanimation2()
	{
		//Debug.Log("in dofall-2 " + THETIME);
		//Debug.Log("isinvis in fall02 at " + THETIME);

		while (blinkCount < numberOfTimesToBlink)
		{
			SMR.enabled = !SMR.enabled;

			if (SMR.enabled == true)
				blinkCount++;

			yield return new WaitForSeconds(blinkRate);
		}
		blinkCount = 0;
		curr_state = AlbertState.Playing;
		this.transform.GetComponent<CapsuleCollider>().enabled = true;
		// Debug.Log("in dofal-2 end of func" + THETIME);
	}






}