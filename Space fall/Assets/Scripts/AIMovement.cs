using UnityEngine;
using System.Collections;


public class AIMovement : MonoBehaviour
{
    public Transform transformBody;
    public Rigidbody playerPhysics;
    public AI        aI;
    void Start()
    {
        playerPhysics = GetComponent<Rigidbody>();
        aI            = GetComponent<AI>();
    }

    void Update()
    {
        Vector3 nextDir = aI.getNextDir();
        Vector3 dir     = nextDir.magnitude != 0 ? nextDir / nextDir.magnitude : Vector3.zero;

        move(dir);
    }

    void move(Vector3 dir)
    {
        Quaternion currentRotation = transformBody.rotation;
        Quaternion newRotation     = Quaternion.Euler(new Vector3(-dir.y * 40, 0, -dir.x * 50));

        transformBody.rotation = Quaternion.Lerp(currentRotation, newRotation, Time.deltaTime / 4);

        Vector3 vel = Vector3.Lerp(playerPhysics.velocity / 15, dir, Time.deltaTime / 10);
        playerPhysics.velocity = vel * 15;
    }
}
