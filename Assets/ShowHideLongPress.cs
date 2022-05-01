using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideLongPress : MonoBehaviour
{
    public ShowHide sh;
    public float timer, endTimer;

    private void FixedUpdate()
    {
        if (timer != 0)
        {
            timer += 0.1f;
            if (timer >= endTimer)
            {
                sh.Press();
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
