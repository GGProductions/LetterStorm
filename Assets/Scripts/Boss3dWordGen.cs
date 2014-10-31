using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGProductions.LetterStorm.Data;
using GGProductions.LetterStorm.Data.Collections;

public class Boss_3d_wordGenBoss3dWordGen : MonoBehaviour
{


	public delegate void gunsDied();
	public static event gunsDied OnMyGunsDied;

	private int NumberOfGunsDestroyed = 0;
	public  void AllerAlbert() {
		NumberOfGunsDestroyed++;
	if(NumberOfGunsDestroyed==2)OnMyGunsDied(); 
	
	}

	//***********************************************************
	public delegate void bosshitwrongletter();
	public static event bosshitwrongletter OnBossWrong;


	private Transform _wordHook;
	private List<GameObject> List3dLetterGO = new List<GameObject>();
	private int wordGenerated_index_of_currLetterTosolve ;

	// Use this for initialization

	private float kerning=0.6f;

	EnemyGenerator _enemygen;
	
	//Context ctxt;
	

	//void Start() { bossmove = GameObject.Find("Boss1Prefab_V2").GetComponent<Boss1_move>(); }

	void Awake () {
	  //  theBossBody = GameObject.Find("Boss1Prefab_V2");
	   
		wordGenerated_index_of_currLetterTosolve = 0;
		fetchWordandConstruct();

		}
	


	
	// Update is called once per frame
	void Update () {
		//Debug.Log("name is " + List3dLetterGO[2].name);
	
	}
	void fetchWordandConstruct()
	{
	  //  ctxt = GameObject.Find("Context").GetComponent<Context>();
		_enemygen = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
		_wordHook = this.transform.FindChild("Boss_Word_hook");

	//	wordGenerated = "strawberry";
		int wordlength = Context.Word.Text.Length;
	//	Debug.Log(wordlength + " size");
		char letter;
		int cnt;
		for (cnt = 0; cnt < wordlength; cnt++)
		{

			letter = Context.Word.Text[cnt];

			float XpositionOfLetter=kerning*(((float)wordlength / 2) - ((float)cnt));
			GameObject letterGo = Instantiate(Resources.Load("Boss_letters/" + letter + "_boss"),
									   new Vector3(_wordHook.position.x - XpositionOfLetter, _wordHook.position.y, _wordHook.position.z),
									   Quaternion.Euler(-180, 0, 0)) as GameObject;

			letterGo.transform.parent = _wordHook;
			letterGo.name = letter.ToString();
			letterGo.GetComponent<MeshRenderer>().enabled = false;
			List3dLetterGO.Add(letterGo);
		}
	}

	void OnTriggerEnter(Collider otherObj) {
	 //   bossmove.getPissed();

		if (NumberOfGunsDestroyed == 2)
		{ //otherObj.tag == "projectileTag" || 
			if (otherObj.tag == "letterProjectile")
			{
				
				 char input = otherObj.name[0];
				// string LetterThatJustHitMe = input.ToString().ToUpper();
				 
				 //wordGenerated_index_of_currLetterTosolve++;
				// Debug.Log( input.ToString() +" "+ Context.Word.Text[wordGenerated_index_of_currLetterTosolve].ToString());
				 if (input == Context.Word.Text[wordGenerated_index_of_currLetterTosolve])
				 {
					 Debug.Log("ITS A MATCH)");
					 if (wordGenerated_index_of_currLetterTosolve < Context.Word.Text.Length + 1)
					 {
						 List3dLetterGO[wordGenerated_index_of_currLetterTosolve].GetComponent<MeshRenderer>().enabled = enabled;
						 wordGenerated_index_of_currLetterTosolve++;
						 Destroy(otherObj.gameObject);
						 //collisioncount++;
					 }
					 else {
						
						 Destroy(otherObj.gameObject);
					 }
					 if (wordGenerated_index_of_currLetterTosolve == Context.Word.Text.Length)
					 {


						 Context.PrepareForNextLevel();
						 Application.LoadLevel(2);
					 }


				 }
				 else
					 if (input != Context.Word.Text[wordGenerated_index_of_currLetterTosolve])
				 { 
					 //*************************************************************************************************************
						OnBossWrong();
						// Messenger<string>.Broadcast("hit wrong letter", otherObj.name);
					   
					 Debug.Log("iam the wromg letter"); }
				//////this.transform.GetComponent<Boss1_move>().getPissed("yo");
			}
			else
			   
			{
				
				
				char nameofletterthathit = otherObj.name[0];			
			}


		}
	
	
	}
}


