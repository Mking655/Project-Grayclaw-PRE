using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LaptopIcon : MonoBehaviour
{
    //icon to represent if the Tufftop is in the player's inventory
    void updateIcon()
    {
        if(GamemodeManager.hasTufftop)
        {
            GetComponent<Image>().enabled = true;
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }
    }
    void Update()
    {
        updateIcon();
    }
}
