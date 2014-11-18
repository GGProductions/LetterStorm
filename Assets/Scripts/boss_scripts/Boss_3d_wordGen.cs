using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GGProductions.LetterStorm.Data;
using GGProductions.LetterStorm.Data.Collections;
using GGProductions.LetterStorm.InGameHelpClasses;

public class Boss_3d_wordGen : MonoBehaviour {


    /// <summary>
    /// setting up the event that will broadcast to Boss_Motion_animation.cs which will start Chaaaarge()
    /// I was experomenting with events ... 
    /// Chaaarge() could probably be accesed from here by getting a ref to Boss_Motion_animation.cs since it and this script are both attached to the same object
    /// </summary>
    public delegate void wrongLetterCollision();
    public static event wrongLetterCollision OnWrongCollision;

  
   
    public delegate void BossgunsDied();
    public static event BossgunsDied OnMyGunsDied;



    private Transform armature;

    private int HowmanyChildrenHave_Boss_Canon_script = 0;
    /// <summary>
    /// make sure to check how many guns this boss has attached to him
    /// when a gun dies (using Boss_eye_gun.cs line 67), it informs this script by accessing AllertAlbert ()
    /// ...wow this needs a beeter way 
    /// </summary>
    private int NumberOfGunsDestroyed = 0;
   

    public void AcannonHasDied() {
        NumberOfGunsDestroyed++;     
        Debug.Log("BOSS_calling albert");
        if (NumberOfGunsDestroyed == HowmanyChildrenHave_Boss_Canon_script) OnMyGunsDied();
    }


 

    private Transform _wordHook;
    private List<GameObject> List3dLetterGO = new List<GameObject>();
    private int wordGenerated_index_of_currLetterTosolve;

    private float kerning = 0.6f;
    EnemyGenerator _enemygen;

 
    /// <summary>
    /// fetch the current word from context
    /// initialize index that keeps track of current letter to solve
    /// </summary>
    void Awake()
    {
       fetchWordandConstruct();
        wordGenerated_index_of_currLetterTosolve = 0;
        HowmanyChildrenHave_Boss_Canon_script = findHowMany_Cannons();

        Debug.Log("I have" + HowmanyChildrenHave_Boss_Canon_script + "cannons");
        findEyes();

    }



   // Boss_Scaly_eye_prefab

    private Boss_Eye_script leftEyescript;
    private Boss_Eye_script rightEyescript;
    private List<Transform> eyeList;

    void findEyes(){

        rightEyescript = transform.FindChild("boss_eye_R_pivot").GetChild(0).transform.GetComponent<Boss_Eye_script>();
        leftEyescript = transform.FindChild("boss_eye_L_pivot").GetChild(0).transform.GetComponent<Boss_Eye_script>();
    
    }

    /// <summary>
    /// search up the chain of children transform through the entire skeleton to find how many cannons are attached . 
    /// this only searches objects that have Boss_Cannon_script.cs attached to it
    /// ps: this cannot find multiple guns on the same branch , it stops searching a branch as soon as it finds  what it's looking for
    /// </summary>
    /// <returns></returns>
    int findHowMany_Cannons()
    {
        int cannonsFound = 0;
        armature = transform.FindChild("Armature");

        foreach (Transform childTransf in this.transform)
        {
            if (childTransf.GetComponentInChildren<Boss_Cannon_script>() != null)
            {
                cannonsFound++;
                // Debug.Log("fond1");
            }
            // Debug.Log("bone i have child-->" + childTransf.name);
            foreach (Transform subChild in childTransf)
            {

                if (subChild.GetComponentInChildren<Boss_Cannon_script>() != null)
                {
                    cannonsFound++;
                    // Debug.Log("fond1");
                }
            }
        }
        return cannonsFound;


    }



    public void AllerAlbert()
    {
        NumberOfGunsDestroyed++;
        if (NumberOfGunsDestroyed == 2) OnMyGunsDied();
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
       // _enemygen = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
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


    /// <summary>
    /// Handles collisions with projectile letters
    /// </summary>
    /// <param name="otherObj"></param>
    void OnTriggerEnter(Collider otherObj){
       // Debug.Log("Collison");

        if (NumberOfGunsDestroyed == HowmanyChildrenHave_Boss_Canon_script)
        {
            if (otherObj.tag == "letterProjectile") {
 //               Debug.Log("Collison with letter");
                if (otherObj.GetComponent<LetterProjectileScript>().isactive){
   //                 Debug.Log("Collison with ACTIVE letter");
                    char input = otherObj.name[0];
                    if (input == Context.Word.Text[wordGenerated_index_of_currLetterTosolve]) {
     //                   Debug.Log("ITS A MATCH)");
                        if (wordGenerated_index_of_currLetterTosolve < Context.Word.Text.Length + 1) {
                            List3dLetterGO[wordGenerated_index_of_currLetterTosolve].GetComponent<MeshRenderer>().enabled = enabled;
                            wordGenerated_index_of_currLetterTosolve++;
                            Destroy(otherObj.gameObject);
                        }
                        else{
                            Destroy(otherObj.gameObject);
                        }
                        if (wordGenerated_index_of_currLetterTosolve == Context.Word.Text.Length) {

                            // Grant the user points for defeating the boss
                            Context.CurrentScore.Increase(ScoreKeeper.PlayerAchievement.DefeatBoss);

                            Context.PrepareForNextLevel();
                            //Fade out boss coroutine 
                            Application.LoadLevel("Win");
                        }
                    }
                    else
                        if (input != Context.Word.Text[wordGenerated_index_of_currLetterTosolve]) {

                            OnWrongCollision();
                            rightEyescript.DoFlashEye();
                            leftEyescript.DoFlashEye();
                        }      
                }          
            }
            else{
                char nameofletterthathit = otherObj.name[0];
            }
        }


    }
}
