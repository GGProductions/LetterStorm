using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss3dWordGen : MonoBehaviour {

	private Transform _wordHook;
	private List<GameObject> List3dLetterGO = new List<GameObject>();
	// Use this for initialization

	EnemyGenerator _enemygen;


	void Awake () {
		_enemygen = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
		_wordHook = this.transform.FindChild("Boss_Word_hook");

		int wordlength = _enemygen.TheWord.Length;
		Debug.Log(wordlength + " size");
		 char letter ;
		int cnt;
		for( cnt=0; cnt<wordlength; cnt ++) {

			letter =_enemygen.TheWord[cnt];

			GameObject go = Instantiate(Resources.Load("Boss_letters/" + letter + "_boss"),
									   new Vector3 (_wordHook.position.x + (float)cnt,_wordHook.position.y,_wordHook.position.z),
									   Quaternion.Euler(-180, 0, 0)) as GameObject;

			go.transform.parent = _wordHook;
			go.name = letter.ToString();
            go.GetComponent<MeshRenderer>().enabled = false;
			List3dLetterGO.Add(go);

		}
	}

	
	
	// Update is called once per frame
	void Update () {

		List3dLetterGO[2].GetComponent<MeshRenderer>().enabled = enabled;
		Debug.Log("name is " + List3dLetterGO[2].name);
	
	}
}
