using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceWall : MonoBehaviour
{
    public GameObject wallPrefab;

    public void Place()
    {
        if ((wallPrefab.name.Contains("wall") && GetComponentInParent<Inventory>().walls > 0) || (wallPrefab.name.Contains("auto crossbow") && GetComponentInParent<Inventory>().autoCrossbow > 0))
        {
            if (wallPrefab.name.Contains("wall")) GetComponentInParent<Inventory>().walls -= 1;
            else GetComponentInParent<Inventory>().autoCrossbow -= 1;

            Instantiate(wallPrefab, transform.position + transform.forward, transform.rotation);
        }
    }
}
