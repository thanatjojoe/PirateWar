using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] public Transform bullet;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] public LineRenderer line;

    [SerializeField] public float launchForce = 1.5f;
    [SerializeField] public float trajectoryTimeStep = 0.05f;
    [SerializeField] public int trajectoryStepCount = 15;
    
    public Vector2 velocity, startMousePos, currentMousePos;


    void Start()
    {
        TurnBase.PlayerShoot = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (TurnBase.turnPlayer == true &&  TurnBase.PlayerShoot == true)
            {
                FireProjectile(spawnPoint, velocity);
            }
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
                pos = lastPos + (pos - lastPos).normalized * maxDistance;
            }

            positions[i] = pos;
            lastPos = pos;

            if (i > 1 && distance > maxDistance)
            {
                line.positionCount = i + 1;
                break;
            }
        }
        line.SetPositions(positions);
    }
    void RotateLauncher()
    {
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void FireProjectile(Transform firePoint, Vector2 fireVelocity)
    {
        float maxDistance = 4f;
        float minVelocity = 5f;
        float maxVelocity = 50f;

        Vector3 lastPoint = line.GetPosition(line.positionCount - 1);
        float distance = Vector3.Distance(firePoint.position, lastPoint);
        float t = Mathf.Clamp01(distance / maxDistance);
        float velocityMagnitude = Mathf.Lerp(minVelocity, maxVelocity, t);

        Vector2 finalVelocity = fireVelocity.normalized * velocityMagnitude;

        Transform pr = Instantiate(bullet, firePoint.position, Quaternion.identity);
        pr.GetComponent<Rigidbody2D>().velocity = finalVelocity;

        TurnBase.turnPlayer = false;
        TurnBase.PlayerShoot = false;
    }
}