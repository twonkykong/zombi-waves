using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopHealLongPress : MonoBehaviour
{
    public float timer, endTimer = 10;
    public Heal heal;

    private void FixedUpdate()
    {
        if (timer != 0)
        {
            timer += 0.1f;
            if (timer >= endTimer-0.01f)
            {
                heal.StopHealing();
                timer = 0;
            }
        }
    }

    public void Press()
    {
        timer = 0.1f;
    }

    public void Release()
    {
        timer = 0;
    }
}
