﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGProductions.LetterStorm.Data;
using GGProductions.LetterStorm.Data.Collections;

public class Boss_3d_wordGen : MonoBehaviour {

   
    public delegate void BossgunsDied();
    public static event BossgunsDied OnMyGunsDied;

    private int NumberOfGunsDestroyed = 0;
    public void AllerAlbert()
    {
        NumberOfGunsDestroyed++;
        if (NumberOfGunsDestroyed == 2) OnMyGunsDied();

    }

    //***********************************************************
    public delegate void wrongLetterCollision();
    public static event wrongLetterCollision OnWrongCollision;

    private Transform _wordHook;
    private List<GameObject> List3dLetterGO = new List<GameObject>();
    private int wordGenerated_index_of_currLetterTosolve;

    private float kerning = 0.6f;
    EnemyGenerator _enemygen;

 

    void Awake()
    {
        wordGenerated_index_of_currLetterTosolve = 0;
        fetchWordandConstruct();

    }
    void Update()
    {
        //Debug.Log("name is " + List3dLetterGO[2].name);

    }


    /// <summary>
    /// Grabs the word to be guessed from Context.Word.Text , and builds
    /// the word using 3d Letters loaded from resources
    /// </summary>
    void fetchWordandConstruct()
    {
        //  ctxt = GameObject.Find("Context").GetComponent<Context>();
        _enemygen = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        _wordHook = this.transform.FindChild("Boss_Word_hook");
        if (_wordHook == null) Debug.Log("not correct");

        //	wordGenerated = "strawberry";
        int wordlength = Context.Word.Text.Length;
        //	Debug.Log(wordlength + " size");
        char letter;
        int cnt;
        for (cnt = 0; cnt < wordlength; cnt++)
        {

            letter = Context.Word.Text[cnt];
           // Debug.Log(letter);
            float XpositionOfLetter = kerning * (((float)wordlength / 2) - ((float)cnt));
            GameObject letterGo = Instantiate(Resources.Load("Boss_letters/" + letter + "_boss"),
                                       new Vector3(_wordHook.position.x - XpositionOfLetter, _wordHook.position.y, _wordHook.position.z),
                                       Quaternion.Euler(-180, 0, 0)) as GameObject;

            letterGo.transform.parent = _wordHook;
            letterGo.name = letter.ToString();
            letterGo.GetComponent<MeshRenderer>().enabled = false;
            List3dLetterGO.Add(letterGo);
        }
    }

    void OnTriggerEnter(Collider otherObj){
        if (NumberOfGunsDestroyed == 2){
            if (otherObj.tag == "letterProjectile") {
                if (otherObj.GetComponent<LetterProjectileScript>().isactive){
                    char input = otherObj.name[0];
                    if (input == Context.Word.Text[wordGenerated_index_of_currLetterTosolve]) {
                        Debug.Log("ITS A MATCH)");
                        if (wordGenerated_index_of_currLetterTosolve < Context.Word.Text.Length + 1) {
                            List3dLetterGO[wordGenerated_index_of_currLetterTosolve].GetComponent<MeshRenderer>().enabled = enabled;
                            wordGenerated_index_of_currLetterTosolve++;
                            Destroy(otherObj.gameObject);
                        }
                        else{
                            Destroy(otherObj.gameObject);
                        }
                        if (wordGenerated_index_of_currLetterTosolve == Context.Word.Text.Length) {
                            Context.PrepareForNextLevel();
                            Application.LoadLevel(2);
                        }
                    }
                    else
                        if (input != Context.Word.Text[wordGenerated_index_of_currLetterTosolve]) {
                            OnWrongCollision();
                        }      
                }          
            }
            else{
                char nameofletterthathit = otherObj.name[0];
            }
        }


    }
}
