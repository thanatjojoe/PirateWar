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
        
        {
            line.enabled = true;
        }
        currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        
        velocity = ((startMousePos - currentMousePos) * -1) * launchForce;

        DrawTrajectory();
        RotateLauncher();
    }
    void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        Vector3 lastPos = spawnPoint.position;
        float maxDistance = 3f;

        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = (Vector2)spawnPoint.position + velocity * t + 0.5f * Physics2D.gravity * t * t;
            float distance = Vector3.Distance(lastPos, pos);

            if (distance > maxDistance)
            {
                positions[i] = lastPos + (pos - lastPos).normalized * maxDistance;
                line.positionCount = i + 1;
                break;
            }

            positions[i] = pos;
            lastPos = pos;
        }

        line.SetPositions(positions);
    }
    void RotateLauncher()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void FireProjectile()
    {
        float maxDistance = 4f;
        float minVelocity = 5f;
        float maxVelocity = 50f;

        Vector3 lastPoint = line.GetPosition(line.positionCount - 1);
        float distance = Vector3.Distance(spawnPoint.position, lastPoint);
        float t = Mathf.Clamp01(distance / maxDistance);
        float velocityMagnitude = Mathf.Lerp(minVelocity, maxVelocity, t);

        Vector2 finalVelocity = velocity.normalized * velocityMagnitude;

        Transform pr = Instantiate(bullet, spawnPoint.position, Quaternion.identity);
        pr.GetComponent<Rigidbody2D>().velocity = finalVelocity;
    }
    
}
