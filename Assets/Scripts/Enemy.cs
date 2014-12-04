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
      //  Debug.Log("Game speed: " + Context.EnemyDifficulty.GameSpeed);

        transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
        
    }

    /// <summary>
    /// A method that determines which movement trajectory an enemy will take depending on its spawn point.
    /// Smart and dumb enemies have different movement patterns determined by their respective versions of this method.
    /// </summary>
    /// <returns>Returns an integer corresponding to a movement trajectory.</returns>
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
    /// <summary>
    /// A virtual method implemented in both SmartEnemy.cs and DumbEnemy.cs. The Path determined in FindPath() is used to execute
    /// a corresponding movement algorithm in whichever enemy has spawned.
    /// </summary>
    /// <param name="atm">The distance an enemy moves every frame update.</param>
    public virtual void MoveEnemy(float atm){}
 
    #endregion


    /// <summary>
    /// Allow both types of enemies to react to AOE attack. 
    /// AOE itself has the same tag as the pecil, 
    /// See consShellScript.cs and Letter projectile.cs
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
