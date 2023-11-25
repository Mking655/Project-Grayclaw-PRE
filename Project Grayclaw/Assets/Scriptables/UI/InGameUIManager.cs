using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIManager : Singleton <InGameUIManager>
{
    public TMP_Text reticle;
    public TMP_Text room;
    private void Start()
    {
        resetReticle();
    }
    public void updateReticle(string text)
    {
        reticle.text = text;
    }
    public void resetReticle()
    {
        reticle.text = ".";
    }
}
