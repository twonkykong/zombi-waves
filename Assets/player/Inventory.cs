using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float pistolAmmo = 16, shotgunAmmo = 12, rifleAmmo = 80, crossbowAmmo = 10, grenades = 1, molotov = 1, knifes = 1, shurikens = 1, bandages = 1, firstAid = 1, wood = 6, autoCrossbow = 2, walls;
    public Text grenadeText, molotovText, knifeText, shurikenText, bandagesText, firstAidText, wallText, autoCrossbowText, woodText;

    private void Update()
    {
        grenadeText.text = "" + grenades;
        molotovText.text = "" + molotov;
        knifeText.text = "" + knifes;
        shurikenText.text = "" + shurikens;
        bandagesText.text = "" + bandages;
        firstAidText.text = "" + firstAid;
        wallText.text = "" + walls;
        autoCrossbowText.text = "" + autoCrossbow;
        woodText.text = "wood: " + wood;
    }

    public void CraftWall()
    {
        if (wood >= 3)
        {
            wood -= 3;
            walls += 1;
        } 
    }
}
