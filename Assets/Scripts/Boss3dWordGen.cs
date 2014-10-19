using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss3dWordGen : MonoBehaviour {

	private Transform _wordHook;
	private List<GameObject> List3dLetterGO = new List<GameObject>();
	private string wordGenerated;

	// Use this for initialization

	private float kerning=0.6f;

	EnemyGenerator _enemygen;

	//Context ctxt;
	private int collisioncount = 0;

	void Awake () {
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

		 wordGenerated = Context.Curriculum.Lessons[0].Words.GetRandomWord().Text ;
	//	wordGenerated = "strawberry";
		int wordlength = wordGenerated.Length;
		Debug.Log(wordlength + " size");
		char letter;
		int cnt;
		for (cnt = 0; cnt < wordlength; cnt++)
		{

			letter = wordGenerated[cnt];

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

		if (otherObj.tag == "projectileTag") {

			collisioncount++;

			if (collisioncount < wordGenerated.Length + 1)
			{
				List3dLetterGO[collisioncount - 1].GetComponent<MeshRenderer>().enabled = enabled;

			}
		
		}
	
	}
}


