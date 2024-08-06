using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager instance;
    public int chosenSkinId;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SetSkinId(int id)
    {
        chosenSkinId = id;
    }

    public int getSkinId()
    {
        return chosenSkinId;
    }
}
