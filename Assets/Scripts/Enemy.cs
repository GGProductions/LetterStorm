using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    #region Fields
    public float MinSpeed;
    public float MaxSpeed;

    private float MinRotateSpeed = 60f;
    private float MaxRotateSpeed = 120f;

    private float MinScale = 0.8f;
    private float MaxScale = 2f;

    private float currentRotationSpeed;
    private float currentScaleX;
    private float currentScaleY;
    private float currentScaleZ;

    private float currentSpeed;
    private float x, y, z;
    #endregion

    #region Properties

    #endregion

    #region Functions
    void Start()
    {
        SetPositionAndSpeed();
    }

    void Update()
    {
        float rotationSpeed = currentRotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed);

        float amtToMove = currentSpeed * Time.deltaTime;
        transform.Translate(Vector3.back * amtToMove, Space.World);

        if (transform.position.z <= -6.2f)
        {
            SetPositionAndSpeed();
            Player.Missed++;
        }
    }

    public void SetPositionAndSpeed()
    {
        currentRotationSpeed = Random.Range(MinRotateSpeed, MaxRotateSpeed);

        currentScaleX = Random.Range(MinScale, MaxScale);
        currentScaleY = Random.Range(MinScale, MaxScale);
        currentScaleZ = Random.Range(MinScale, MaxScale);

        currentSpeed = Random.Range(MinSpeed, MaxSpeed);
        x = Random.Range(-6f, 6f);
        y = 0.0f;
        z = 7.0f;

        transform.position = new Vector3(x, y, z);
        transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
    }
    #endregion
}
