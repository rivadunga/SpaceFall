using UnityEngine;
using System.Collections;

/**
 * Handle all the actions related to the player movement
 */

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerBody;

    private Rigidbody   playerPhysics;
    private Transform   tranformBody;
    private PlayerAudio playerAudio;


    void Start()
    {
        playerPhysics = GetComponent<Rigidbody>();
        tranformBody  = playerBody.GetComponent<Transform>();
        playerAudio   = playerBody.GetComponent<PlayerAudio>();
    }

    void Update()
    {
        updateMovement();
        updateParticles();
    }

    void updateMovement()
    {
        Vector3 fwd    = tranformBody.TransformDirection(Vector3.up);
        Vector3 normal = tranformBody.TransformDirection(-Vector3.forward);

        float GRAVITY = 1f;

        /*
         * playerPhysics.AddForce(new Vector3(0, fwd.y, 0) * 1);
         * playerPhysics.AddForce(new Vector3(0, 0, fwd.z) * (-fwd.y) * 4f);
         * playerPhysics.AddForce(new Vector3(normal.x, 0, 0) * 7);
         * playerPhysics.AddForce(Vector3.down * GRAVITY);
         */
        Vector3 actVel = playerPhysics.velocity;
        Vector3 newVel = new Vector3(normal.x, fwd.y - GRAVITY, fwd.z * (-fwd.y) * 4f) * 2;

        playerPhysics.velocity = Vector3.Lerp(actVel, newVel, Time.deltaTime * 10);
        Debug.DrawLine(tranformBody.position, normal * 1000, Color.green);
        Debug.DrawLine(tranformBody.position, fwd * 1000, Color.cyan);
        Debug.DrawLine(tranformBody.position, newVel * 1000, Color.magenta);
    }

    public void rotatePlayer(Vector3 dir)
    {
        tranformBody.Rotate(dir);
    }

    bool maxSpeed = false;
    private void updateParticles()
    {
        if ((GetComponent<Rigidbody> ().velocity.z > 10) && !maxSpeed) {
            GameObject.Find("Player_effect").GetComponent<ParticleSystem>().emissionRate = 1000;
            GameObject.Find("Player_effect").GetComponent<ParticleSystem>().startColor   = new Color(1, 1, 1, 0.03f);
            GameObject.Find("Player_effect").GetComponent<ParticleSystem>().startSpeed   = GetComponent<Rigidbody>().velocity.z * 20f;
            playerAudio.playMaxSpeedAudio();
            maxSpeed = true;
        }
        else {
            if ((GetComponent<Rigidbody> ().velocity.z < 10)) {
                GameObject.Find("Player_effect").GetComponent<ParticleSystem>().emissionRate = 20;
                GameObject.Find("Player_effect").GetComponent<ParticleSystem>().startColor   = new Color(1, 1, 1, 0.005f);
                GameObject.Find("Player_effect").GetComponent<ParticleSystem>().startSpeed   = GetComponent<Rigidbody>().velocity.z * 2f;
                maxSpeed = false;
            }
        }
    }
}
