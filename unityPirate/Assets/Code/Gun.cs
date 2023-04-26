using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] public Transform bullet;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] public LineRenderer line;

    [SerializeField] public float launchForce = 1.5f;
    [SerializeField] public float trajectoryTimeStep = 0.05f;
    [SerializeField] public int trajectoryStepCount = 15;

    public Vector2 velocity, startMousePos, currentMousePos;
    
    
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            FireProjectile();
        }

        
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        velocity = ((startMousePos - currentMousePos) * -1) * launchForce;
        
        DrawTrajectory();
        RotateLauncher();
        
        
    }

    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = (Vector2)spawnPoint.position + velocity * t + 0.5f * Physics2D.gravity * t * t;
            positions[i] = pos;
        }

        line.positionCount = trajectoryStepCount;
        line.SetPositions(positions);
    }

    void RotateLauncher()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x);
    }

    void FireProjectile()
    {
        Transform pr = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        pr.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}
