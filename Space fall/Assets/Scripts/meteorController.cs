using UnityEngine;
using System.Collections;

public class meteorController : MonoBehaviour
{
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.name == "model") {
            Rigidbody rigidBody = trigger.gameObject.transform.parent.parent.GetComponent<Rigidbody>();
            rigidBody.velocity = -rigidBody.velocity / rigidBody.velocity.magnitude * 30;
        }
    }
}
