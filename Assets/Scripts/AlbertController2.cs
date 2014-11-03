﻿using UnityEngine;
using System.Collections;

public class AlbertController2 : MonoBehaviour {




    public float PlayerSpeed = 5f;
    public float rotateSpeed = 40.0f;

    private Transform mytransform;
    private Transform myAmmoSpawn;
    public AnimationClip walkAnimationClip;
    public AnimationClip idleAnimationClip;
    public AnimationClip fallAnimationClip;
    public AnimationClip wakBackAnimationClip;
    public AnimationClip throwAnimationClip;

    public float smooth = 110.0F;
    public float tiltAngle = 50.0F;

    public GameObject ProjectilePrefab;

    public bool LetterMode = false;

    public string LetterBulletname;

    void OnEnable(){
        Boss_3d_wordGen.OnMyGunsDied += ListenToBoss;}
    
    void OnDisable(){
        Boss_3d_wordGen.OnMyGunsDied -= ListenToBoss;
    }

    void ListenToBoss() { Debug.Log("Albert heard you"); LetterMode = true; }

    void Awake()
    {
        LetterBulletname = string.Empty;
        mytransform = this.transform;
        myAmmoSpawn = transform.Find("AmmoSpawnPoint");

        animation.wrapMode = WrapMode.Loop;
        animation.AddClip(walkAnimationClip, "walking");
        animation.AddClip(idleAnimationClip, "idleing");
        animation.AddClip(fallAnimationClip, "falling");
        animation.AddClip(wakBackAnimationClip, "walkingback");

        animation.wrapMode = WrapMode.Once;
        animation.AddClip(throwAnimationClip, "throwing");


        
    }

    void Start()
    {/*
        animation.wrapMode = WrapMode.Loop;
        animation["idleing"].layer = 0;
        animation["walking"].layer = 0;
      
        animation["walkingback"].layer = 0;
        

        animation.wrapMode = WrapMode.Default;
        animation["throwing"].layer = 3;
        animation["falling"].layer = 0;
        animation.Stop();
      * */
    }
    private GameObject poof;
    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.A) ) { LetterBulletname = "a_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.B)) { LetterBulletname = "b_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.C)) { LetterBulletname = "c_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.D)) { LetterBulletname = "d_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.E)) { LetterBulletname = "e_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.F)) { LetterBulletname = "f_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.G)) { LetterBulletname = "g_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.H)) { LetterBulletname = "h_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.I)) { LetterBulletname = "i_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.J)) { LetterBulletname = "j_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.K)) { LetterBulletname = "k_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.L)) { LetterBulletname = "l_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.M)) { LetterBulletname = "m_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.N)) { LetterBulletname = "n_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.O)) { LetterBulletname = "o_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.P)) { LetterBulletname = "p_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.Q)) { LetterBulletname = "q_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.R)) { LetterBulletname = "r_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.S)) { LetterBulletname = "s_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.T)) { LetterBulletname = "t_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.U)) { LetterBulletname = "u_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.V)) { LetterBulletname = "v_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.W)) { LetterBulletname = "w_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.X)) { LetterBulletname = "x_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.Y)) { LetterBulletname = "y_projectilePrefab"; }
           if (Input.GetKeyDown(KeyCode.Z)) { LetterBulletname = "z_projectilePrefab"; }
        */

         /*
        if (Input.GetKeyDown("space"))
        {
           // animation.CrossFade("throwing");
            StartCoroutine(DoThrow());
        }
       // moveabout();
        */
//SwitchCaseMove ( gettirrection());
        oldupdate();


//Debug.Log(x.ToString());
    }

    void SwitchCaseMove(int x) {
        float coef = 0.05f;
        switch (x)
        {
            case 1:
                mytransform.Translate(Vector3.right * -1 * coef, Space.World);
                mytransform.Translate(Vector3.forward * -1 * coef);
                Quaternion target = Quaternion.Euler(0, -1, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

                break;
            case 2:
                mytransform.Translate(Vector3.forward * -1 * coef);
                animation.CrossFade("walkingback");
                break;
            case 3:
                mytransform.Translate(Vector3.right * 1 * coef, Space.World);
                mytransform.Translate(Vector3.forward * -1 * coef);
                Quaternion target1 = Quaternion.Euler(0, 1, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target1, Time.deltaTime * smooth);
                break;
            case 4:
                mytransform.Translate(Vector3.right * -1 * coef, Space.World);
                Quaternion target2 = Quaternion.Euler(0, -1, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target2, Time.deltaTime * smooth);
                break;

            case 6:
                mytransform.Translate(Vector3.right * 1 * coef, Space.World);
                Quaternion target3 = Quaternion.Euler(0, 1, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target3, Time.deltaTime * smooth);
                break;
            case 7:
                mytransform.Translate(Vector3.right * 1 * coef, Space.World);
                mytransform.Translate(Vector3.forward * -1 * coef);
                Quaternion target4 = Quaternion.Euler(0, -1, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target4, Time.deltaTime * smooth);
                break;
            case 8:
                mytransform.Translate(Vector3.forward * 1 * coef);
                break;
            case 9:
                mytransform.Translate(Vector3.right * 1 * coef, Space.World);
                mytransform.Translate(Vector3.forward * 1 * coef);
                Quaternion target5 = Quaternion.Euler(0, 1, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target5, Time.deltaTime * smooth);

                break;
            default:
                animation.Stop("walking");
                break;
        }
    
    
    }
    IEnumerator DoThrow()
    {
        animation.Stop("walking");
        animation.CrossFade("throwing");
        yield return new WaitForSeconds(animation["throwing"].length);

    }



    int gettirrection(){

        int dir=0;
        float leftRight = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
        float forwardBackward = Input.GetAxis("Vertical") * PlayerSpeed * Time.deltaTime;

        //9
        if (leftRight > 0 && forwardBackward > 0) { dir = 9; }
        else
        if (leftRight < 0 && forwardBackward > 0) { dir = 7; }
        else
        if (leftRight > 0 && forwardBackward < 0) { dir = 3; }
        else
        if (leftRight < 0 && forwardBackward < 0) { dir = 1; }
        else
        if (forwardBackward > 0) { dir = 8; }
        else
        if (forwardBackward < 0) { dir = 2; }
        else
        if (leftRight < 0) { dir = 4; }
        else
        if (leftRight > 0) { dir = 6; }



        return dir;
    }
    
    void moveabout() {
        animation.CrossFade("walking");
        float tiltAroundz = Input.GetAxis("Horizontal") * tiltAngle;

        float leftRight = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
        float forwardBackward = Input.GetAxis("Vertical") * PlayerSpeed * Time.deltaTime;
        
        Debug.Log("leftright " + leftRight.ToString() + "    updown "+forwardBackward );
        if (forwardBackward > 0) {
            mytransform.Translate(Vector3.forward * forwardBackward);
        }
        if (forwardBackward < 0) {
            mytransform.Translate(Vector3.forward * forwardBackward);
            animation.CrossFade("walkingback");
        }
        if (leftRight < 0) {
            mytransform.Translate(Vector3.right * leftRight, Space.World);
            //tilting
//            float tiltAroundz = Input.GetAxis("Horizontal") * tiltAngle;
            Quaternion target = Quaternion.Euler(0, tiltAroundz, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        }
        if (leftRight > 0) {
            mytransform.Translate(Vector3.right * leftRight, Space.World);
            //tilting
          //  float tiltAroundz = Input.GetAxis("Horizontal") * tiltAngle;
            Quaternion target = Quaternion.Euler(0, tiltAroundz, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
       
        }


        //if ( forwardBackward != 0){mytransform.Translate(Vector3.forward * forwardBackward);}

            /*
        else
            if (leftRight != 0 || forwardBackward != 0)
            {
                if (forwardBackward < 0)
                    animation.CrossFade("walkingback");
                else
                    animation.CrossFade("walking");
                // Move the player
                mytransform.Translate(Vector3.right * leftRight, Space.World);
                mytransform.Translate(Vector3.forward * forwardBackward);

                //tilting
                float tiltAroundz = Input.GetAxis("Horizontal") * tiltAngle;
                Quaternion target = Quaternion.Euler(0, tiltAroundz, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
            else
            {
                animation.CrossFade("walking");
                //animation.CrossFade("idleing");
                //mytransform.Translate(Vector3.back * 0.018f);
            }


        if (mytransform.position.x <= -7f)
            mytransform.position = new Vector3(-7f, transform.position.y, transform.position.z);
        else if (mytransform.position.x >= 7)
            mytransform.position = new Vector3(7f, transform.position.y, transform.position.z);

        // up and down player movement limitation
        if (mytransform.position.z > 6f)
            mytransform.position = new Vector3(gameObject.transform.position.x, transform.position.y, 6f);
        else if (mytransform.position.z < -4.9f)
            mytransform.position = new Vector3(gameObject.transform.position.x, transform.position.y, -4.9f);
         * */

    }

    void oldupdate()
    {


        if (Input.GetKeyDown(KeyCode.A)) { LetterBulletname = "a_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.B)) { LetterBulletname = "b_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.C)) { LetterBulletname = "c_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.D)) { LetterBulletname = "d_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.E)) { LetterBulletname = "e_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.F)) { LetterBulletname = "f_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.G)) { LetterBulletname = "g_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.H)) { LetterBulletname = "h_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.I)) { LetterBulletname = "i_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.J)) { LetterBulletname = "j_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.K)) { LetterBulletname = "k_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.L)) { LetterBulletname = "l_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.M)) { LetterBulletname = "m_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.N)) { LetterBulletname = "n_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.O)) { LetterBulletname = "o_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.P)) { LetterBulletname = "p_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.Q)) { LetterBulletname = "q_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.R)) { LetterBulletname = "r_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.S)) { LetterBulletname = "s_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.T)) { LetterBulletname = "t_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.U)) { LetterBulletname = "u_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.V)) { LetterBulletname = "v_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.W)) { LetterBulletname = "w_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.X)) { LetterBulletname = "x_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.Y)) { LetterBulletname = "y_projectilePrefab"; }
        if (Input.GetKeyDown(KeyCode.Z)) { LetterBulletname = "z_projectilePrefab"; }




        if (Input.GetKeyDown("space"))
        {
            animation.CrossFade("throwing");
            Vector3 position = new Vector3(myAmmoSpawn.position.x, myAmmoSpawn.position.y, myAmmoSpawn.position.z);

            if (!LetterMode)
            {
                // animation.CrossFade("throwing");
                // Fire projectile

                poof = Instantiate(ProjectilePrefab, position, this.transform.rotation) as GameObject;
                poof.rigidbody.AddForce(transform.forward * 1000.0f);
            }
            else
            {
                if (LetterBulletname != string.Empty)
                {
                    animation.CrossFade("throwing");
                    // Fire projectile
                    char input = LetterBulletname[0];
                    string LettercChosen = input.ToString().ToUpper();

                    // Debug.Log(" letname is "+LetterBulletname);%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%5
                    //&&&&&&&&&&&&&&&&&&&&&&&&& GARENTEE A SHOT OF LETTER... if ( >0) only shoots if you have that lettter 
                    if (Context.PlayerInventory.GetLetterCount(LettercChosen) > 0)
                    {
                        poof = Instantiate(Resources.Load("LettesProjectile/" + LetterBulletname), position, Quaternion.Euler(-90, 0, 0)) as GameObject;
                        poof.GetComponent<LetterProjectileScript>().isactive = true;// I can't change this here
                        poof.rigidbody.AddForce(transform.forward * 1000.0f);
                        Context.PlayerInventory.take_letterAway(LettercChosen);
                    }


                }

            }

        }

        float leftRight = Input.GetAxis("Horizontal") * PlayerSpeed * Time.deltaTime;
        float forwardBackward = Input.GetAxis("Vertical") * PlayerSpeed * Time.deltaTime;
        if (leftRight != 0 && forwardBackward != 0)
        {

            //slw speed
            mytransform.Translate(Vector3.right * leftRight / 2, Space.World);
            mytransform.Translate(Vector3.forward * forwardBackward);

        }

        else
            if (leftRight != 0 || forwardBackward != 0)
            {
                if (forwardBackward < 0)
                    animation.CrossFade("walkingback");
                else
                    animation.CrossFade("walking");
                // Move the player
                mytransform.Translate(Vector3.right * leftRight, Space.World);
                mytransform.Translate(Vector3.forward * forwardBackward);

                //tilting
                float tiltAroundz = Input.GetAxis("Horizontal") * tiltAngle;
                Quaternion target = Quaternion.Euler(0, tiltAroundz, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            }
            else
            {
                animation.CrossFade("walking");
                //animation.CrossFade("idleing");
                //mytransform.Translate(Vector3.back * 0.018f);
            }


        if (mytransform.position.x <= -7f)
            mytransform.position = new Vector3(-7f, transform.position.y, transform.position.z);
        else if (mytransform.position.x >= 7)
            mytransform.position = new Vector3(7f, transform.position.y, transform.position.z);

        // up and down player movement limitation
        if (mytransform.position.z > 6f)
            mytransform.position = new Vector3(gameObject.transform.position.x, transform.position.y, 6f);
        else if (mytransform.position.z < -4.9f)
            mytransform.position = new Vector3(gameObject.transform.position.x, transform.position.y, -4.9f);


    }

    void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag != "bossTag" && otherObj.tag != "bossProjectileTag")
        {

            otherObj.transform.GetComponent<SphereCollider>().isTrigger = true;
            Messenger<string>.Broadcast("picked up a letter", otherObj.name);   // Nabil: Added this so I can remove letters from my dict whenever Albert has picked them up to keep from overspawning
            // letters that he needs but already has -Paul
            Context.PlayerInventory.AddCollectedLetter(otherObj.name);
            
            GameObject go = otherObj.gameObject;

            Destroy(go);



        }

        if (otherObj.tag == "letterProjectile") {
            Debug.Log("name of letterpickedup " + otherObj.name);
            char input=otherObj.name[0];
            Messenger<string>.Broadcast("picked up a letter", otherObj.name);
            Context.PlayerInventory.AddCollectedLetter(input.ToString().ToUpper()); 
        }
        else
        {
           // animation.CrossFade("falling");
        }
   
    }


}
