using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    #region Fields
    public float MinSpeed;
    public float MaxSpeed;

    //randomly generated number to decide which pathing algorithm to take
    
    //Min/Max speed
    private float MinRotateSpeed = 60f;
    private float MaxRotateSpeed = 120f;
    
    //Min/Max scale
    private float MinScale = 0.8f;
    private float MaxScale = 2f;

    //Current rotation speed/scale
    private float currentRotationSpeed;
    private float currentScaleX;
    private float currentScaleY;
    private float currentScaleZ;

    //Current speed and direction
    private float currentSpeed;
    private float x, y, z;
    #endregion

    #region Properties
    
    private int path;


    #endregion

    #region Functions
    void Start()
    {
        
        SetScaleAndSpeed();
        path = Random.Range(0, 4);
    }

    void Update()
    {
        float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed);

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
        currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);

        currentScaleX = Random.Range(MinScale, MaxScale);
        currentScaleY = Random.Range(MinScale, MaxScale);
        currentScaleZ = Random.Range(MinScale, MaxScale);

        currentSpeed = Random.Range(MinSpeed, MaxSpeed);
        x = Random.Range(-6f, 6f);
        y = 0.0f;
        z = 7.0f;

        transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
        
    }
    public void findPath(float atm)
    {
        switch (path)
        {
            case 0:
                transform.Translate(Vector3.back * atm, Space.World);
                break;
            case 1:
                transform.Translate(new Vector3(0.5f, 0f, -1f) * atm, Space.World);
                break;
            case 2:
                transform.Translate(new Vector3(-0.5f, 0f, -1f) * atm, Space.World);
                break;
            case 3:
                transform.Translate(new Vector3(0f, 0f, -2f) * atm, Space.World);
                break;
        }
   } 
    #endregion
}
