using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    #region Fields
    public float MinSpeed;
    public float MaxSpeed;
    public int Path;

    
    private float MinRotateSpeed = 60f;
    private float MaxRotateSpeed = 120f;
    
    private float MinScale = 0.8f;
    private float MaxScale = 1f;

    //Current rotation speed/scale
    private float currentRotationSpeed;
    private float currentScaleX;
    private float currentScaleY;
    private float currentScaleZ;

    //Current speed and direction
    private float currentSpeed;    

    #endregion

    #region Properties

    public float Speed
    {
        get { return currentSpeed; }

        set { currentSpeed = value; }
    }

    #endregion

    #region Functions
    public virtual void Start()
    {
        
        SetScaleAndSpeed();
        Path = Random.Range(0, 4);
    }

    public virtual void Update()
    {
        //float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        //transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed);

        float amtToMove = currentSpeed * Time.deltaTime;
        findPath(amtToMove);

        if (transform.position.z <= -6.2f)
        {
            Destroy(this.gameObject);
            Player.Missed++;
        }
    }

    public void SetScaleAndSpeed()
    {
        //currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);

        currentScaleX = Random.Range(MinScale, MaxScale);
        currentScaleY = Random.Range(MinScale, MaxScale);
        currentScaleZ = Random.Range(MinScale, MaxScale);
       
        currentSpeed = Random.Range(MinSpeed, MaxSpeed);

        transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
        
    }

    public virtual void findPath(float atm){}
 
    #endregion


    /// <summary>
    /// thsi will allow both types of enemies to react to aoe. 
    /// aoe itself has the same tag as the pecil 
    /// look at consShellScript.cs and Letter projectile.cs
    /// </summary>
    /// <param name="otherObj"></param>
    void OnTriggerEnter(Collider otherObj)
    {

        if (otherObj.name == "aoeSpherePrefab(Clone)")
        {
            Instantiate(Resources.Load("Explosions/ExplosionGreen"),
                                      transform.position,
                                      Quaternion.Euler(-180, 0, 0));
        }
      

    }

}
