using UnityEngine;
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
        Boss3dWordGen.OnMyGunsDied += ListenToBoss;}
    
    void OnDisable(){
        Boss3dWordGen.OnMyGunsDied -= ListenToBoss;
    }

    void ListenToBoss() { Debug.Log("Albert heard you"); LetterMode = true; }

    void Awake()
    {
        LetterBulletname = string.Empty;
        mytransform = this.transform;
        myAmmoSpawn = transform.Find("AmmoSpawnPoint");
        animation.AddClip(walkAnimationClip, "walking");
        animation.AddClip(idleAnimationClip, "idleing");
        animation.AddClip(fallAnimationClip, "falling");
        animation.AddClip(wakBackAnimationClip, "walkingback");
        animation.AddClip(throwAnimationClip, "throwing");
    }

    void Start()
    {
        animation.wrapMode = WrapMode.Loop;
        animation["idleing"].layer = 1;
        animation["walking"].layer = 1;
      
        animation["walkingback"].layer = 1;
        

        animation.wrapMode = WrapMode.Default;
        animation["throwing"].layer = 0;
        animation["falling"].layer = 1;
    }
    private GameObject poof;
    void Update()
    {


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




        if (Input.GetKeyDown("space"))
        {
            Vector3 position = new Vector3(myAmmoSpawn.position.x, myAmmoSpawn.position.y, myAmmoSpawn.position.z);

            if (!LetterMode)
            {
                animation.CrossFade("throwing");
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
                    if (Context.PlayerInventory.GetLetterCount(LettercChosen)  > 0 ) {
                        poof = Instantiate(Resources.Load("LettesProjectile/" + LetterBulletname), position, Quaternion.Euler(-90, 0, 0)) as GameObject;
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
            mytransform.Translate(Vector3.right * leftRight /2 , Space.World);
            mytransform.Translate(Vector3.forward * forwardBackward);

        }

        else
        if (leftRight != 0 || forwardBackward != 0)
        {
            if (forwardBackward<0)
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

            Context.PlayerInventory.AddCollectedLetter(otherObj.name);
        
            GameObject go = otherObj.gameObject;

            Destroy(go);



        }

        if (otherObj.tag == "letterProjectile") {
            Debug.Log("name of letterpickedup " + otherObj.name);
            char input=otherObj.name[0];

            Context.PlayerInventory.AddCollectedLetter(input.ToString().ToUpper()); 
        }
        else
        {
           // animation.CrossFade("falling");
        }
   
    }


}
