using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GGProductions.LetterStorm.Data;
using GGProductions.LetterStorm.Data.Collections;

public class EnemyGenerator : MonoBehaviour
{
    #region Public Variables ---------------------------------------------
    // Create the public variables that store data which can be accessed through the Unity editor

    public GameObject[] enemyPrefabs;
    public GameObject[] spawnPoints;
    public GameObject BossPrefab;

    public bool WordSolvingStage = false;  //hey Paul: this coul be a state ... I just needed a quick way to lett Albert know what stage it is
    //when albert kills the boss guns, the boss informs the enemy generator that it is now Word solving time
    // labert  checks what stage it is , and when it is word sloving time, he can start shoting letters

    #endregion Public Variables ---------------------------------------------

    #region Private Variables ---------------------------------------------
    // Create the private variables that stores the data for this class's properties

    // The EnemyGenerator maintains an FSM. Depending on which state the machine is in, the spawning behaviors will change.
    // This enum lists all possible states for the FSM
    private enum State
    {
        Idle,
        Initialize,
        Setup,
        SpawnEnemy,
        Boss
    }

    // A dictionary that maps the letters of the alphabet to their respective indices in the public enemyPrefabs array
    private Dictionary<char, int> LetterDict = new Dictionary<char, int>();

    //local variable that holds current state
    private State state;
                     
    private int enemiesSpawned = 0;
    private bool bossSpawned = false;

    private List<char> letterList;

    private char reqLetter;

    //private bool spawning = false;
    #endregion Private Variables ---------------------------------------------

    #region Event Handlers ---------------------------------------------
    void Awake()
    {
        state = State.Initialize;
    }
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

    #endregion Event Handlers ---------------------------------------------

    #region Finite State Machine ---------------------------------------------
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

        letterList = new List<char>(Context.Word.Text);

        for (int i = 0; i < Context.Alphabet.Length; i++)
        {
            LetterDict.Add(Char.ToLower(Context.Alphabet[i]), i);
        }

            state = State.Setup;
    }

    private void Setup()
    {
        
        reqLetter = letterList[UnityEngine.Random.Range(0, letterList.Count)];

        if (enemiesSpawned >= 15 && !bossSpawned)
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
        // Array of spawnpoints that have no children
        GameObject[] emptySpawns = AvailableSpawnPoints();
        
        // The number of enemies we will spawn, selected randomly
        int numOfEnemies = SpawnQuantity(emptySpawns.Length);

        // the index of the spawnpoint for the letter we must generate, as long as there are required letters to generate
        int reqSpawn = UnityEngine.Random.Range(0, emptySpawns.Length);

        if (numOfEnemies > 0)
        {
            GameObject req = Instantiate(enemyPrefabs[LetterDict[reqLetter]],

                                emptySpawns[reqSpawn].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            req.transform.parent = emptySpawns[reqSpawn].transform;
        }
        
        emptySpawns = AvailableSpawnPoints();
        numOfEnemies = SpawnQuantity(emptySpawns.Length);
        int spawn = UnityEngine.Random.Range(0, emptySpawns.Length);

        for (int i = 0; i < numOfEnemies; i++)
        {
            int enemyindex = UnityEngine.Random.Range(0, enemyPrefabs.Length);

            GameObject go = Instantiate(enemyPrefabs[enemyindex],
                                            emptySpawns[spawn].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;

            go.name = enemyPrefabs[enemyindex].name;
            go.transform.parent = emptySpawns[spawn].transform;
            spawn = (spawn + 1) % emptySpawns.Length;
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

    #endregion Finite State Machine ---------------------------------------------

    #region Helper Methods ---------------------------------------------
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

    //genereate list of available spawnpoints that have no children
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

    public void OnEnable()
    {
        Messenger<char>.AddListener("letter projectile died", InventoryCheck);
        Messenger<char>.AddListener("picked up a letter", RequirementCheck);
    }
    public void OnDisable()
    {
        Messenger<char>.RemoveListener("letter projectile died", InventoryCheck);
        Messenger<char>.RemoveListener("picked up a letter", RequirementCheck);
    }


    public void InventoryCheck(char c)
    {
        Debug.Log("A good letter has died today: " + c);
        if (Context.PlayerInventory.GetLetterCount(c.ToString()) < 1) {
            int enemyindex = UnityEngine.Random.Range(0, enemyPrefabs.Length);
            GameObject go = Instantiate(enemyPrefabs[LetterDict[c]],
                                            spawnPoints[3].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
    }

    public void RequirementCheck(char c)
    {
        Debug.Log("I picked somethingi up");
        if (Context.PlayerInventory.GetLetterCount(c.ToString()) > 0 && letterList.Contains(c))
        {
            letterList.Remove(c);
            Debug.Log(letterList.Count);
        }
    }
    #endregion Helper Methods ---------------------------------------------
}
