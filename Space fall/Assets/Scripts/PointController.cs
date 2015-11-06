using UnityEngine;
using System.Collections;

/**
 * Handles the behavior of the checkpoint prefab
 */

public class PointController : MonoBehaviour
{
    public Material visitedMaterial;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.name == "Player_body") {
            MeshRenderer render = GetComponent<MeshRenderer> ();
            render.material = visitedMaterial;
        }
    }
}
