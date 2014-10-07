using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {
	public enum State
	{
		Idle,
		Initialize,
		Setup,
		SpawnEnemy
	}
	public GameObject[] enemyPrefabs;       //array to hold all the prefabs of enemies we wish to spawn
	public GameObject[] spawnPoints;        //array to hold references to all spawn points

	public State state;                     //local variable that holds current state

	void Awake()
	{
		state = EnemyGenerator.State.Initialize;
	}
	// Use this for initialization
	IEnumerator Start () {
		while (true)
		{
			switch(state) {
				case State.Initialize:
					Initialize();
					break;
				case State.Setup:
					Setup();
					break;
				case State.SpawnEnemy:
					SpawnEnemy();
					break;
                case State.Idle:
                    Idle();
                    break;

			}

			yield return 0;
		}
	}

	private void Initialize()
	{
		if (!CheckEnemyPrefabs())
		{
			return;
		}

		if (!CheckSpawnPoints())
		{
			return;
		}
		state = EnemyGenerator.State.Setup;
	}

	private void Setup()
	{
		state = EnemyGenerator.State.SpawnEnemy;
	}

	private void SpawnEnemy()
	{
		GameObject[] gos = AvailableSpawnPoints();

		int enemyindex = 0;
		for (int i = 0; i < gos.Length; i++)
		{

			enemyindex = Random.Range(0, enemyPrefabs.Length);

		   // GameObject go = Instantiate(enemyPrefabs[enemyindex],
			 //                               gos[i].transform.position, Quaternion.identity) as GameObject;
			GameObject go = Instantiate(enemyPrefabs[enemyindex],
											gos[i].transform.position, Quaternion.Euler(270, 0, 0)) as GameObject;

			go.name = enemyPrefabs[enemyindex].name;
			go.transform.parent = gos[i].transform;
		 
		}
		state = EnemyGenerator.State.Idle;
	}


    private void Idle()
    {
        state = EnemyGenerator.State.SpawnEnemy;
    }


	//make sure we have at least one enemy prefab to spawn
	private bool CheckEnemyPrefabs()
	{
		if (enemyPrefabs.Length > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//make sure we have at least one spawn point
	private bool CheckSpawnPoints()
	{
		if (spawnPoints.Length > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//genereate list of available spawnpoints that have no enemies as children
	private GameObject[] AvailableSpawnPoints()
	{
		List<GameObject> gos = new List<GameObject>();

		for (int i = 0; i < spawnPoints.Length; i++)
		{
			if (spawnPoints[i].transform.childCount == 0)
			{
				//Debug.Log("***Spawn Point Available");
				gos.Add(spawnPoints[i]);
			}
		}

		return gos.ToArray();
	}


}

 