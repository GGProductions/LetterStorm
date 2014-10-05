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
            }

            yield return 0;
        }
	}

    private void Initialize()
    {
        Debug.Log("***We are in Initialize function***");

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
        Debug.Log("***We are in Setup function***");
        state = EnemyGenerator.State.SpawnEnemy;
    }

    private void SpawnEnemy()
    {
        Debug.Log("***SpawnEnemy***");
        GameObject[] gos = AvailableSpawnPoints();

        for (int i = 0; i < gos.Length; i++)
        {
            GameObject go = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)],
                                            gos[i].transform.position, Quaternion.identity) as GameObject;
            go.transform.parent = gos[i].transform;
        }
        state = EnemyGenerator.State.Idle;
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
                Debug.Log("***Spawn Point Available");
                gos.Add(spawnPoints[i]);
            }
        }

        return gos.ToArray();
    }
}

 