using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropKnife : MonoBehaviour
{
    public GameObject knifePrefab;
    public Transform throwPoint;

    public void Throw()
    {
        if (!isActiveAndEnabled) return;
        if ((knifePrefab.name.Contains("knife") && GetComponentInParent<Inventory>().knifes > 0) || (knifePrefab.name.Contains("shuriken") && GetComponentInParent<Inventory>().shurikens > 0))
        {
            GameObject g = Instantiate(knifePrefab, throwPoint.position, transform.rotation);

            if (knifePrefab.name.Contains("knife")) GetComponentInParent<Inventory>().knifes -= 1;
            else GetComponentInParent<Inventory>().shurikens -= 1;
        }
    }
}
