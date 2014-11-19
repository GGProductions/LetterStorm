using UnityEngine;
using System.Collections;

public class HurricaneOuterProjectile1 : MonoBehaviour {

    private Transform CenterPoint;
    private float xSpeedAwayFromCenter;
    private float ySpeedAwayFromCenter;
    private float zSpeedAwayFromCenter;

    public GameObject DestroyedExplosion;

	// Use this for initialization
	void Start () {
        CenterPoint = this.transform.parent.transform;
        xSpeedAwayFromCenter = 0.05f;
        ySpeedAwayFromCenter = 0.05f;
        zSpeedAwayFromCenter = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {

        /*
        float xDistanceFromCenter;
        float yDistanceFromCenter;
        float zDistanceFromCenter;
        xDistanceFromCenter = this.transform.position.x - CenterPoint.position.x;
        if (xDistanceFromCenter < 0f)
        {
            if (xSpeedAwayFromCenter > 0f)
            {
                xSpeedAwayFromCenter *= -1f;
            }
        }

        yDistanceFromCenter = this.transform.position.y - CenterPoint.position.y;
        if (yDistanceFromCenter < 0f)
        {
            if (ySpeedAwayFromCenter > 0f)
            {
                ySpeedAwayFromCenter *= -1f;
            }
        }

        zDistanceFromCenter = this.transform.position.z - CenterPoint.position.z;
        if (zDistanceFromCenter < 0f)
        {
            if (zSpeedAwayFromCenter > 0f)
            {
                zSpeedAwayFromCenter *= -1f;
            }
        }

        transform.Translate(
            this.transform.position.x * xSpeedAwayFromCenter * Time.deltaTime,
            this.transform.position.y * ySpeedAwayFromCenter * Time.deltaTime,
            this.transform.position.z * zSpeedAwayFromCenter * Time.deltaTime);*/

        /*
        transform.Translate(Vector3.forward * speedAwayFromCenter * Time.deltaTime);
        transform.Translate(Vector3.back * speedAwayFromCenter * Time.deltaTime);
        transform.Translate(Vector3.right * speedAwayFromCenter * Time.deltaTime);
        transform.Translate(Vector3.left * speedAwayFromCenter * Time.deltaTime);*/
        //speedAwayFromCenter += 0.1f;
	}

    /// <summary>
    /// Projectile reacts to only two types of adverse tags: enemy and bossTag
    /// </summary>
    /// <param name="otherObj"></param>
    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == "enemy" || otherObj.tag == "bossTag")
        {
            Instantiate(DestroyedExplosion, transform.position, Quaternion.Euler(270, 0, 0));
            //Destroy(gameObject);
        }
    }
}
