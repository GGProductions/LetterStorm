﻿using UnityEngine;
using System.Collections;

public class LetterProjectileScript : MonoBehaviour {

    private float currentRotationSpeed;
    // Use this for initialization
    void Start()
    {
        currentRotationSpeed = 5f;
    }
    private bool isCounting = false;
    // Update is called once per frame
    void Update()
    {
        //this.transform.position.z = 0;
        float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 2) * rotationSpeed);

        //  transform.Translate(new Vector3(0, 0, 0.5f), Space.World);

        //rigidbody.AddForce(Vector3.forward * 10);

        if (transform.position.z > 20 || transform.position.z < -10 || transform.position.x > 8 || transform.position.x < -8)
            Destroy(gameObject);

        if (startCounter) { timer += Time.deltaTime; }
        if (timer > 1f) {
            this.transform.GetComponent<SphereCollider>().isTrigger = true;
            timer = 0;
            startCounter = false;
          
        }
    }

    private bool startCounter = false;
    private float timer;
    void OnTriggerEnter(Collider otherObj)
    {

        
        if (otherObj.tag == "bossTag")
        {
            this.transform.GetComponent<SphereCollider>().isTrigger = false;
            startCounter=true;
            timer = 0;
           //  gameObject.rigidbody.AddForce(transform.up * 1100f);
            // Instantiate(boom, transform.position, Quaternion.Euler(270, 0, 0));
            //    Destroy(gameObject);

        }
        if (otherObj.tag == "AlbertPrefab2")
        {
            Debug.Log("I am letter --->albert");
            this.transform.GetComponent<SphereCollider>().isTrigger = true;
            Destroy(gameObject);
        }
           
    }



}