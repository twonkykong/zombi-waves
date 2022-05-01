using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    public GameObject[] guns, buttons;
    public int selected;

    public void Change(int number)
    {
        guns[selected].SetActive(false);
        buttons[selected].SetActive(false);

        selected += number;
        if (selected > guns.Length - 1) selected = 0;
        else if (selected < 0) selected = guns.Length - 1;

        guns[selected].SetActive(true);
        buttons[selected].SetActive(true);
    }
}
