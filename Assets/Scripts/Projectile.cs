using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float ProjectileSpeed;
    public GameObject ExplosionPrefab;

    private Transform myTransform;
    private string enemyType;
    private Enemy enemy;

	// Use this for initialization
	void Start () 
    {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
    {
        float amtToMove = ProjectileSpeed * Time.deltaTime;
        myTransform.Translate(Vector3.forward * amtToMove);

        if (myTransform.position.z > 5.7f)
            Destroy(this.gameObject);
	}

    void OnTriggerEnter(Collider otherObject)
    {
        enemyType = otherObject.name;
        Debug.Log(enemyType);
        enemy = (Enemy)GameObject.Find(enemyType).GetComponent("Enemy");
        if (otherObject.tag == "enemy")
        {
            Instantiate(ExplosionPrefab, enemy.transform.position, enemy.transform.rotation);

            enemy.MinSpeed += 1.2f;
            enemy.MaxSpeed += 1.9f;

            enemy.SetPositionAndSpeed();

            Destroy(gameObject);
            Player.Score += 100;

            if (Player.Score >= 1000)
                Application.LoadLevel(3);
        }
    }
}
