using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodStatus : MonoBehaviour
{
    public Sprite[] godSprites; //신 초상화 저장용 변수
    public Image godImage;  //신 초상화
    public Text godName;    //신 이름
    public Text active; // 액티브 패시브
    public Text description;    // 능력 설명
    public Text skillName;  // 능력 이름
    public Text skillCd;    // 능력 쿨타임
    public Image skillImage;    // 능력 이미지
    public GameObject ui;   // 정보창
    public GameObject notSelectedUI;    // 오류창
    private void OnEnable()
    {
        SetPage();
    }

    void SetPage()
    {
        Skill godSkill = SkillInven.instance.godSkill;

        if (godSkill != null)
        {
            notSelectedUI.SetActive(false);
            ui.SetActive(true);
            switch (PlayerPrefs.GetInt("God"))
            {
                case 0:
                    godName.text = "제우스";
                    godImage.sprite = godSprites[0];
                    break;
                case 1:
                    godName.text = "아레스";
                    godImage.sprite = godSprites[1];

                    break;
                case 2:
                    godName.text = "하데스";
                    godImage.sprite = godSprites[2];

                    break;
                case 3:
                    godName.text = "헤르메스";
                    godImage.sprite = godSprites[3];

                    break;
                default:
                    godName.text = "";
                    break;
            }
            if (godSkill.isActive)
            {
                active.text = "액티브 스킬";
            }
            else
            {
                active.text = "패시브 스킬";
            }
            skillName.text = godSkill.skillName;
            if (godSkill.isActive)
            {
                skillCd.gameObject.SetActive(true);
                skillCd.text = godSkill.cooldown.ToString();
            }
            else
            {
                skillCd.gameObject.SetActive(false);
            }
            description.text = godSkill.description;
            skillImage.sprite = godSkill.skillImage;
        }
        else
        {
            ui.SetActive(false);
            notSelectedUI.SetActive(true);
        }
    }
}
