using UnityEngine;
using System.Collections;
using GGProductions.LetterStorm.InGameHelpClasses;


public class Vowel : MonoBehaviour
{

    public GameObject theDrop;

    // Use this for initialization
    void Start()
    {
        this.transform.GetComponent<BoxCollider>().enabled = true;
        this.transform.GetComponent<BoxCollider>().isTrigger = true;
    }


    void OnTriggerEnter(Collider otherObj)
    {

        if (otherObj.tag == "projectileTag")
        {
            int rndChance = Random.Range(0, 50);
            
            if (rndChance > 1)
            {
                GameObject go = Instantiate(theDrop, transform.position, Quaternion.Euler(270, 0, 0)) as GameObject;
                go.name = theDrop.name;
            }
            else
            {
                Instantiate(Resources.Load("PowerUpsResources/dualPen_pickup"),
                                          transform.position, Quaternion.Euler(90, 0, 0)) ;

            }

            // Grant the user points for defeating the enemy
            Context.CurrentScore.Increase(ScoreKeeper.PlayerAchievement.DefeatSmartEnemy);

            //keep the follwoing line if we want vowels to destroy aoe . pecile gets auto destroyed.
            //Destroy(otherObj.gameObject);
            Destroy(gameObject);

        }

    }



}
