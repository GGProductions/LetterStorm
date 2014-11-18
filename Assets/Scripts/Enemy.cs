using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    #region Fields
    public float MinSpeed;
    public float MaxSpeed;
    
    protected int Path;

    
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
        Path = FindPath();
    }

    public virtual void Update()
    {
        //float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        //transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed);

        float amtToMove = currentSpeed * Time.deltaTime;
        MoveEnemy(amtToMove);

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
       
        currentSpeed = Random.Range(MinSpeed, MaxSpeed) * Context.EnemyDifficulty.GameSpeed;
        Debug.Log("Game speed: " + Context.EnemyDifficulty.GameSpeed);

        transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
        
    }
    private int FindPath()
    {
        if (transform.parent.name == "Spawnpoint1" || transform.parent.name == "Spawnpoint2")
        {
            return Random.Range(0, 2);
        }
        else if (transform.parent.name == "Spawnpoint4" || transform.parent.name == "Spawnpoint5")
        {
            return Random.Range(1, 3);
        }
        else
        {
            return Random.Range(0, 3);
        }
    }
    public virtual void MoveEnemy(float atm){}
 
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
