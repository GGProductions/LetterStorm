using UnityEngine;
using System.Collections;
using GGProductions.LetterStorm.InGameHelpClasses;


public class Vowel : MonoBehaviour
{

    public GameObject theDrop;


    void chance() {


        int rndChance = Random.Range(0, 100);
        Debug.Log(" ???" + rndChance);

        if (rndChance <= 50)
        {
            GameObject go = Instantiate(theDrop, transform.position, Quaternion.Euler(270, 0, 0)) as GameObject;
            go.name = theDrop.name;
            Debug.Log("made a letter");
        }
        else
            if (rndChance > 50 && rndChance <= 60)
            {
                Instantiate(Resources.Load("PowerUpsResources/dualPen_pickup"),
                       transform.position, Quaternion.Euler(90, 0, 0));

                Debug.Log("made a dualpen");
            }
            else
                if (rndChance > 60 && rndChance <= 75)
                {
                    Instantiate(Resources.Load("PowerUpsResources/Hurricane_pickup"),
                    transform.position, Quaternion.Euler(90, 0, 0));
                    Debug.Log("made a hurricane");

                }

                else
                    if (rndChance > 75 )
                    {
                        Instantiate(Resources.Load("PowerUpsResources/PoisonousMushroomPowerUpPrefab"),
                    transform.position, Quaternion.Euler(-45, 0, 0));
                      //  Debug.Log("made a l");

                    }
      



            /*
        {
            rndChance = Random.Range(0, 100);
            if (rndChance > 50)
                Instantiate(Resources.Load("PowerUpsResources/dualPen_pickup"),
                    transform.position, Quaternion.Euler(90, 0, 0));
            else
                Instantiate(Resources.Load("PowerUpsResources/PoisonousMushroomPowerUpPrefab"),
                    transform.position, Quaternion.Euler(-45, 0, 0));
        }
              */
    }


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
            chance();

            /*
            int rndChance = Random.Range(0, 100);
            
            if (rndChance > 25)
            {
                GameObject go = Instantiate(theDrop, transform.position, Quaternion.Euler(270, 0, 0)) as GameObject;
                go.name = theDrop.name;
            }
            else
            {
                rndChance = Random.Range(0, 100);
                if (rndChance > 50)
                    Instantiate(Resources.Load("PowerUpsResources/dualPen_pickup"),
                        transform.position, Quaternion.Euler(90, 0, 0));
                else
                    Instantiate(Resources.Load("PowerUpsResources/PoisonousMushroomPowerUpPrefab"),
                        transform.position, Quaternion.Euler(-45, 0, 0));
            }


            */

            // Grant the user points for defeating the enemy
            Context.CurrentScore.Increase(ScoreKeeper.PlayerAchievement.DefeatSmartEnemy);

            //keep the follwoing line if we want vowels to destroy aoe . pecile gets auto destroyed.
            //Destroy(otherObj.gameObject);
            Messenger.Broadcast("vowel died");
            Destroy(gameObject);

        }

    }



}
