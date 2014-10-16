using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour
{
    public enum State
    {
        Idle,
        Initialize,
        Setup,
        SpawnEnemy,
        Boss
    }
    public GameObject[] enemyPrefabs;       //array to hold all the prefabs of enemies we wish to spawn
    public GameObject[] spawnPoints;        //array to hold references to all spawn points
    public GameObject BossPrefab;           // boss of the game

    public State state;                     //local variable that holds current state

    private int smartSpawns = 4;
    private int dumbSpawns = 4;
    private int enemiesSpawned = 0;
    private bool bossSpawned = false;
    //private bool spawning = false;

    void Awake()
    {
        state = EnemyGenerator.State.Initialize;
    }
    // Use this for initialization
    IEnumerator Start()
    {
        while (true)
        {
            switch (state)
            {
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
                case State.Boss:
                    Boss();
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
        /*if (!spawning)
        {
            spawning = true;*/
        if (enemiesSpawned >= 20 && !bossSpawned)
        {
            state = EnemyGenerator.State.Boss;
        }
        else if (bossSpawned)
        {
            state = EnemyGenerator.State.Idle;
        }
        else
        {
            state = EnemyGenerator.State.SpawnEnemy;

        }
       /* }
        else
        {
            state = EnemyGenerator.State.Idle;
        }*/
    }

    private void SpawnEnemy()
    {
        /// <summary>
        /// The Enemy Spawn Manager makes the following decisions:
        /// 1) How many enemies are to be spawned right now?
        /// 2) What types of enemies are they going to be?
        /// 3) Which spawn points are they going to spawn from?
        /// </summary>
     
        GameObject[] gos = AvailableSpawnPoints();

        int spawnCount = SpawnQuantity(gos.Length);
        int spawn = Random.Range(0, gos.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            int enemyindex = Random.Range(0, enemyPrefabs.Length);

            GameObject go = Instantiate(enemyPrefabs[enemyindex],
                                            gos[spawn].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            go.name = enemyPrefabs[enemyindex].name;
            go.transform.parent = gos[spawn].transform;
            spawn = (spawn + 1) % gos.Length;
            enemiesSpawned++;

        }

        state = EnemyGenerator.State.Idle;
    }

    private void Idle()
    {
        state = EnemyGenerator.State.Setup;
    }

    private void Boss()
    {
       // GameObject go = Instantiate(BossPrefab,
         //                                   spawnPoints[2].transform.position, Quaternion.Euler(180, 0, 180)) as GameObject;

        GameObject go = Instantiate(BossPrefab,
                                           new Vector3(0,0,2), Quaternion.Euler(180, 0, 180)) as GameObject;


        bossSpawned = true;
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

    //genereate list of available spawnpoints that have less than three enemies as children
    private GameObject[] AvailableSpawnPoints()
    {
        List<GameObject> gos = new List<GameObject>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].transform.childCount == 0)
            {
                gos.Add(spawnPoints[i]);
            }

        }

        return gos.ToArray();
    }

    private int SpawnQuantity(int splen)
    {
        return Random.Range(0, splen);
    }

}
