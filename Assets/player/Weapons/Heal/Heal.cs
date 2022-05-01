using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public GameObject player;
    public int hp;

    public float timer, endTimer;

    public RectTransform healingThing;
    public GameObject healingObj;

    public void HealClick()
    {
        if (((name == "bandages" && GetComponentInParent<Inventory>().bandages > 0) || (name == "firstAid" && GetComponentInParent<Inventory>().firstAid > 0)) && player.GetComponent<Player>().hp < 100)
        {
            if (timer == 0)
            {
                timer = 0.1f;
                healingObj.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (timer != 0)
        {
            timer += 0.1f;

            if (player.GetComponent<PlayerMove>().joystickMove.transform.localPosition != Vector3.zero) StopHealing();
            healingThing.sizeDelta = new Vector2(timer/endTimer* 82.58012f, healingThing.sizeDelta.y);
            if (timer >= endTimer)
            {
                player.GetComponent<Player>().hp += hp;
                if (name == "bandages") GetComponentInParent<Inventory>().bandages -= 1;
                else GetComponentInParent<Inventory>().firstAid -= 1;
                StopHealing();
            }
        }
    }

    public void StopHealing()
    {
        timer = 0;
        healingThing.sizeDelta = new Vector2(0, healingThing.sizeDelta.y);
        healingObj.SetActive(false);
    }
}
