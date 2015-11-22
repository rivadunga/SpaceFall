using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerCollision : MonoBehaviour
{
    public PlayerData playerData;

    private DataController data;
    private PlayerAudio    audio;


    void Start()
    {
        data  = GameObject.Find("lvl_controller").GetComponent<DataController>();
        audio = GetComponent<PlayerAudio>();
    }

    void OnTriggerEnter(Collider trigger)
    {
        int currentCheck = playerData.getCheckpoint();

        if (trigger.name == ("checkpoint_" + currentCheck)) {
            Debug.Log("Checkpoint");
            data.addScore(1000);
            audio.playPointAudio();
            playerData.addCheckpoint();
        }
    }
}
