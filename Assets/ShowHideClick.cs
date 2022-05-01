using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideClick : MonoBehaviour
{
    public GameObject[] show, hide;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (GameObject obj in show) obj.SetActive(true);
            foreach (GameObject obj in hide) obj.SetActive(false);
        }
    }
}
