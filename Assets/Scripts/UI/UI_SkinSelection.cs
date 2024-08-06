using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkinSelection : MonoBehaviour
{
    [SerializeField] private int currentIndex;
    [SerializeField] private Animator skinDisplay;
    [SerializeField] private int maxIndex;


    public  void NextSkin()
    {
        currentIndex++;
        if (currentIndex > maxIndex)
            currentIndex = 0;

        UpdateSkinDisplay();
    }

    public void PreviousSkin()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = maxIndex;

        UpdateSkinDisplay();
    }

    private void UpdateSkinDisplay()
    {
        for (int i = 0; i < skinDisplay.layerCount; i++)
        {
            skinDisplay.SetLayerWeight(i, 0); // set weight to 0 for all layers
        }

        skinDisplay.SetLayerWeight(currentIndex, 1); //set weight to 1 for desired skin
    }

    public void SelectSkin()
    {
        SkinManager.instance.SetSkinId(currentIndex);
    }


}
