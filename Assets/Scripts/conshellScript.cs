using UnityEngine;
using System.Collections;
using GGProductions.LetterStorm.InGameHelpClasses;

public class conshellScript : MonoBehaviour {

	public GameObject conson;

/// <summary>
/// make a drop and disapear 
/// </summary>
/// <param name="otherObj"></param>
	void OnTriggerEnter(Collider otherObj)
	{



		if (otherObj.tag == "projectileTag")
		{
			GameObject go = Instantiate(conson, transform.position, Quaternion.Euler(270, 0, 0)) as GameObject;
			go.name = conson.name;

            // Grant the user points for defeating the enemy
            Context.CurrentScore.Increase(ScoreKeeper.PlayerAchievement.DefeatDumbEnemy);

			Destroy(gameObject);
		}


	}
}
