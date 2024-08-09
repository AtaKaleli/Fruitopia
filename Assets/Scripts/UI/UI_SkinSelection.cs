using TMPro;
using UnityEngine;


[System.Serializable]
public struct Skin
{
    public string skinName;
    public int skinPrice;
    public bool unlocked;

}

public class UI_SkinSelection : MonoBehaviour
{
    [SerializeField] private int skinIndex;
    [SerializeField] private Animator skinDisplay;
    [SerializeField] private int maxIndex;

    [SerializeField] private Skin[] skinList;
    [SerializeField] private TextMeshProUGUI bankAmount;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI buySelectText;

    private UI_LevelSelection levelSelectionUI;
    private UI_MainMenu mainMenuUI;


    private void Start()
    {
        
        UpdateSkinDisplay();
        LoadSkinUnlocks();

        mainMenuUI = GetComponentInParent<UI_MainMenu>();
        levelSelectionUI = mainMenuUI.GetComponentInChildren<UI_LevelSelection>(true);
    }

    public void NextSkin()
    {
        skinIndex++;
        if (skinIndex > maxIndex)
            skinIndex = 0;

        UpdateSkinDisplay();
    }

    public void PreviousSkin()
    {
        skinIndex--;
        if (skinIndex < 0)
            skinIndex = maxIndex;

        UpdateSkinDisplay();
    }

    private void UpdateSkinDisplay()
    {

        bankAmount.text = FruitsInBank().ToString();

        for (int i = 0; i < skinDisplay.layerCount; i++)
        {
            skinDisplay.SetLayerWeight(i, 0); // set weight to 0 for all layers
        }

        skinDisplay.SetLayerWeight(skinIndex, 1); //set weight to 1 for desired skin

        if (skinList[skinIndex].unlocked)
        {
            priceText.transform.parent.gameObject.SetActive(false);
            buySelectText.text = "Select";
        }
        else
        {
            priceText.transform.parent.gameObject.SetActive(true);
            priceText.text = "Price:   " + skinList[skinIndex].skinPrice.ToString();
            buySelectText.text = "Buy";

        }

    }

    private void LoadSkinUnlocks()
    {
        for (int i = 0; i < skinList.Length; i++)
        {
            string skinName = skinList[i].skinName;
            bool skinUnlocked = PlayerPrefs.GetInt(skinName + "Unlocked", 0) == 1;


            if (skinUnlocked || i== 0)
                skinList[i].unlocked = true;

        }
    }

    private void BuySkin(int index)
    {
        
        if (HaveEnoguhFruits(skinList[index].skinPrice) == false)
        {
            print("not enough fruits");
            return;
        }

        
        string skinName = skinList[index].skinName;
        skinList[index].unlocked = true;

        PlayerPrefs.SetInt(skinName + "Unlocked", 1);
    }

    public void SelectSkin()
    {
        if (skinList[skinIndex].unlocked == false)
            BuySkin(skinIndex);
        else
        {
            SkinManager.instance.SetSkinId(skinIndex);
            mainMenuUI.SwicthToUI(levelSelectionUI.gameObject);

        }

        UpdateSkinDisplay();
    }

    public int FruitsInBank()
    {
        return PlayerPrefs.GetInt("TotalFruitsAmount");
    }

    private bool HaveEnoguhFruits(int price)
    {
        if(FruitsInBank() > price)
        {
            PlayerPrefs.SetInt("TotalFruitsAmount", FruitsInBank() - price);
            return true;
        }

        return false;
    }
}
