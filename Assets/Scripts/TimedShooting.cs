using UnityEngine;
using System.Collections;

public class TimedShooting : MonoBehaviour {

	public GameObject theShot;
	private float shotDelay;

	void Awake() {


	   Debug.Log("Levelis"+ Context.LevelNum);

	   float levelFloat = (float)Context.LevelNum;
	   float divisor = Mathf.Pow(1.2f, levelFloat);
	   shotDelay = (1.7f / divisor )+ Context.EnemyDifficulty.BossCannonProjectilesPerSecond;
	

	   Debug.Log("shot delay " + shotDelay + "level " + levelFloat);
		}

	IEnumerator Start() 
	{
		while(  gameObject)	
		{
			Vector3 here= new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);	
			{Instantiate (theShot, here, transform.rotation );
			yield return new WaitForSeconds(shotDelay);
			}
		}
	}	
	
}

