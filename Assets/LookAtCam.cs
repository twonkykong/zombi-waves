using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    private void Update()
    {
         transform.LookAt(Camera.main.transform.position);
    }
}
