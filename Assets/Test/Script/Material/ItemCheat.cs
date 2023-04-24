using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class ItemCheat : MonoBehaviour
{
    public Button[] upBtn;
    public Button[] downBtn;
    public InputField[] itemValue;

    void Start()
    {
       

        for (int i = 0; i < upBtn.Length; i++)
        {
            int temp = i;
            upBtn[temp].onClick.AddListener(() => ItemAdd(temp));
        }

        for (int i = 0; i < downBtn.Length; i++)
        {
            int temp = i;
            downBtn[temp].onClick.AddListener(() => ItemSub(temp));
        }
    }

    public void AddAll()
    {
        MaterialInfo[] matArr = ItemManager.instance.mats;

        for(int i = 0; i < 12; i++)
        {
            Material.instance.Add(matArr[i]);
        }
    }
    void ItemAdd(int x)
    {
        MaterialInfo mat;
        MaterialInfo[] matArr = ItemManager.instance.mats;
        mat = matArr[x];

        if (int.TryParse(itemValue[x].text, out int z))
        {
            for (int i = 0; i < z; i++)
            {
                
                Material.instance.Add(mat);
            }

            switch (x)
            {
                case 12:
                    PlayerPrefs.SetInt("AtkSpellBook", Material.instance.GetItemCount(mat));
                    break;
                case 13:
                    PlayerPrefs.SetInt("CrateSpellBook", Material.instance.GetItemCount(mat));
                    break;
                case 14:
                    PlayerPrefs.SetInt("CdmgSpellBook", Material.instance.GetItemCount(mat));
                    break;
            }
        }      
    }

    void ItemSub(int x)
    {
        MaterialInfo mat;
        MaterialInfo[] matArr = ItemManager.instance.mats;
        mat = matArr[x];

        if (int.TryParse(itemValue[x].text, out int z))
        {
            if (z > Material.instance.GetItemCount(mat))
            {
                Material.instance.Remove(mat);
            }
            else
            {
                Material.instance.Use(z, mat);
            }
        }
    }
}
