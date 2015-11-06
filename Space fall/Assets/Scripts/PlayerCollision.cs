using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerCollision : MonoBehaviour
{
    private DataController data;
    private PlayerAudio    audio;

    void Start()
    {
        data  = GameObject.Find("lvl_controller").GetComponent<DataController>();
        audio = GetComponent<PlayerAudio>();
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.name.StartsWith("obj_point")) {
            data.addScore(1000);
            audio.playPointAudio();
        }
    }
}
