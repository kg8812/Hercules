using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpellBookEnchance : MonoBehaviour
{
    public Image spellBook;
    int index = 0;
    public Sprite[] spellBookSprite;
    public Text contents;
    public string[] contetsText;
    public Text spellBookNum;
    public Text Result;
    string resultContents;

    public MaterialInfo[] books;

    float num;
    // Start is called before the first frame update
    void Start()
    {
        Result.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
            if (index == 3) index = 0;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
            if (index == -1) index = 2;
        }

        spellBook.sprite = spellBookSprite[index];
        contents.text = contetsText[index];
        if (index == 2)
        {

            spellBookNum.text = PlayerPrefs.GetInt("CrateSpellBook").ToString();

        }
        else if (index == 1)
        {
            spellBookNum.text = PlayerPrefs.GetInt("CdmgSpellBook").ToString();

        }
        else
        {
            spellBookNum.text = PlayerPrefs.GetInt("AtkSpellBook").ToString();

        }

    }
    public void SpellBook()
    {
        Result.gameObject.SetActive(true);
        if (index == 2)
        {
            resultContents = "크리티컬 확률이";
            num = Random.Range(0.05f, 0.1f);
        }
        else if (index == 1)
        {

            resultContents = "크리티컬 데미지가";
            num = Random.Range(0.1f, 0.5f);
        }
        else
        {

            resultContents = "공격력이";
            num = Random.Range(0.5f, 1.5f);
        }
        if (int.Parse(spellBookNum.text) > 0)
        {
            if (index == 0)
            {
                PlayerPrefs.SetInt("AtkSpellBook", int.Parse(spellBookNum.text) - 1);
                PlayerPrefs.SetFloat("PlayerAtk", PlayerPrefs.GetFloat("PlayerAtk") + num);
                Material.instance.Use(1, books[0]);
            }

            else if (index == 1)
            {
                PlayerPrefs.SetInt("CdmgSpellBook", int.Parse(spellBookNum.text) - 1);
                PlayerPrefs.SetFloat("PlayerCritDmg", PlayerPrefs.GetFloat("PlayerCritDmg") + num);
                Material.instance.Use(1, books[1]);

            }

            else
            {
                PlayerPrefs.SetInt("CrateSpellBook", int.Parse(spellBookNum.text) - 1);
                PlayerPrefs.SetFloat("PlayerCritProb", PlayerPrefs.GetFloat("PlayerCritProb") + num);
                Material.instance.Use(1, books[2]);
            }

            Result.text = "축하합니다" + resultContents + " " + num.ToString("F3") + "만큼 증가했습니다.";

        }


        else
            Result.text = "주문서를 소지하고 있지 않습니다.";
    }
}
