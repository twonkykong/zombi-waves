using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGrenade : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform throwPoint;

    public void Throw()
    {
        if (!isActiveAndEnabled) return;
        if ((grenadePrefab.name.Contains("molotov") && GetComponentInParent<Inventory>().molotov > 0) || (grenadePrefab.name.Contains("grenade") && GetComponentInParent<Inventory>().grenades > 0))
        {
            GameObject g = Instantiate(grenadePrefab, throwPoint.position, transform.rotation);
            g.GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse);

            if (grenadePrefab.name.Contains("grenade")) GetComponentInParent<Inventory>().grenades -= 1;
            else GetComponentInParent<Inventory>().molotov -= 1;
        }
    }
}
