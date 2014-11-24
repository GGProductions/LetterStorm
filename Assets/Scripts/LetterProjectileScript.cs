using UnityEngine;
using System.Collections;

public class LetterProjectileScript : MonoBehaviour {

    private float currentRotationSpeed;

    void Awake() {
    //    isactive = true; 
    }
    // Use this for initialization
    void Start()
    {
       
        currentRotationSpeed = 5f;
    }
    // Update is called once per frame
    void Update()
    {
        //this.transform.position.z = 0;
        float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 2) * rotationSpeed);

        //  transform.Translate(new Vector3(0, 0, 0.5f), Space.World);

        //rigidbody.AddForce(Vector3.forward * 10);

        if (transform.position.z > 20 || transform.position.z < -10 || transform.position.x > 8 || transform.position.x < -8)
        {
            Messenger<char>.Broadcast("letter projectile died", this.transform.name[0]);
          //  Debug.Log("yo im dead");
            Destroy(gameObject);

        }

        if (startCounter) { timer += Time.deltaTime; }
        if (timer > 1f) {
            isactive = false; 
            this.transform.GetComponent<SphereCollider>().isTrigger = true;
            timer = 0;
            startCounter = false;
          
        }
    }

    private bool startCounter = false;
    private float timer;
    public bool isactive;

    void OnTriggerEnter(Collider otherObj)
    {

  
        
        if (otherObj.tag == "bossTag")
        {
            this.transform.GetComponent<SphereCollider>().isTrigger = false;
            startCounter=true;
            timer = 0;
           // isactive = false;     
           //  gameObject.rigidbody.AddForce(transform.up * 1100f);
            // Instantiate(boom, transform.position, Quaternion.Euler(270, 0, 0));
            //    Destroy(gameObject);

        }
        if (otherObj.tag == "AlbertPrefab2"  )
        {
          // if(isactive)

           Destroy(gameObject);
        }
           
    }




}
