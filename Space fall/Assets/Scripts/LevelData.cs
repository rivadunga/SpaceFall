using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour
{
    private int   score = 0;
    private float time  = 0;

    public int getScore()
    {
        return score;
    }

    public float getTime()
    {
        return time;
    }

    public void setScore(int score)
    {
        this.score = score;
    }

    public void setTime(float time)
    {
        this.time = time;
    }
}
