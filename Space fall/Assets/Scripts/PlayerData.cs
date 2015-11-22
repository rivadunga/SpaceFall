using UnityEngine;
using System.Collections;


public class PlayerData : MonoBehaviour
{
    private float health     = 100;
    private float energy     = 100;
    private int   checkpoint = 1;

    public float getHealth()
    {
        return health;
    }

    public float getEnergy()
    {
        return energy;
    }

    public int getCheckpoint()
    {
        return checkpoint;
    }

    public void addHealth(float health)
    {
        this.health += health;
    }

    public void addEnergy(float energy)
    {
        this.energy += energy;
    }

    public void addCheckpoint()
    {
        this.checkpoint += 1;
    }
}
