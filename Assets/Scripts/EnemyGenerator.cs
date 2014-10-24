using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GGProductions.LetterStorm.Data;
using GGProductions.LetterStorm.Data.Collections;

public class EnemyGenerator : MonoBehaviour
{
    private enum State
    {
        Idle,
        Initialize,
        Setup,
        SpawnEnemy,
        Boss
    }

    private Dictionary<char, int> LetterDict = new Dictionary<char, int>();
    private char[] alphabet;

    public GameObject[] enemyPrefabs;       //array to hold all the prefabs of enemies we wish to spawn
    public GameObject[] spawnPoints;        //array to hold references to all spawn points
    public GameObject BossPrefab;           // boss of the game
    public bool WordSolvingStage = false;  //hey Paul: this coul be a state ... I just needed a quick way to lett Albert know what stage it is
                                            //when albert kills the boss guns, the boss informs the enemy generator that it is now Word solving time
                                            // labert  checks what stage it is , and when it is word sloving time, he can start shoting letters


    private State state;                     //local variable that holds current state

    private int smartSpawns = 4;
    private int dumbSpawns = 4;
    private int enemiesSpawned = 0;
    private bool bossSpawned = false;


    private Inventory inv;
    private Word word;
    private List<char> letterList;

    private char reqLetter;
 

    //private bool spawning = false;

    

    void Awake()
    {
        state = State.Initialize;

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

        inv = Context.PlayerInventory;
        word = Context.Curriculum.Lessons[0].Words.GetRandomWord();
        letterList = new List<char>(word.Text);


        alphabet = Context.Alphabet;

        for (int i = 0; i < alphabet.Length; i++)
        {
            LetterDict.Add(Char.ToLower(alphabet[i]), i);
        }

            state = State.Setup;
    }

    private void Setup()
    {


        reqLetter = letterList[UnityEngine.Random.Range(0, letterList.Count)];


        if (enemiesSpawned >= 20 && !bossSpawned)
        {
            state = State.Boss;
        }
        else if (bossSpawned)
        {
            state = State.Idle;
        }
        else
        {
            state = State.SpawnEnemy;
        }
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
     //   Debug.Log(gos.Length);

        int spawnMax = SpawnQuantity(gos.Length);


        int reqSpawn = UnityEngine.Random.Range(0, gos.Length);
        //Debug.Log("reqSpawn" + reqSpawn);
        if (spawnMax > 0)
        {
            GameObject req = Instantiate(enemyPrefabs[LetterDict[reqLetter]],

                                gos[reqSpawn].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            req.transform.parent = gos[reqSpawn].transform;
        }
        

        gos = AvailableSpawnPoints();
        spawnMax = SpawnQuantity(gos.Length);
        int spawn = UnityEngine.Random.Range(0, gos.Length);

        for (int i = 0; i < spawnMax; i++)
        {
            int enemyindex = UnityEngine.Random.Range(0, enemyPrefabs.Length);

            GameObject go = Instantiate(enemyPrefabs[enemyindex],
                                            gos[spawn].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            go.name = enemyPrefabs[enemyindex].name;
            go.transform.parent = gos[spawn].transform;
            spawn = (spawn + 1) % gos.Length;
            enemiesSpawned++;

        }

        state = State.Setup;
    }

    private void Idle()
    {
        state = State.Setup;
    }

    private void Boss()
    {

        GameObject go = Instantiate(BossPrefab,
                                           new Vector3(0,0,2), Quaternion.Euler(180, 0, 180)) as GameObject;


        bossSpawned = true;
        state = State.Idle;
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
        return UnityEngine.Random.Range(0, splen);
    }

}
