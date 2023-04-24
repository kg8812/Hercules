using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillCdUI : MonoBehaviour
{
    public GameObject[] icons;
    public Image[] skillImages;
    public Image[] coolTimes;

    float firstCd;
    float afterFirstSkill = 0;
    float secondCd;
    float afterSecondSkill = 0;

    float thirdCd;
    float afterThirdSkill = 0;

    // Start is called before the first frame update
    void Start()
    {
        afterFirstSkill = 10000;
        afterSecondSkill = 10000;
        afterThirdSkill = 10000;
    }


    // Update is called once per frame
    void Update()
    {
        if (SkillInven.instance.godSkill == null)
        {
            icons[0].SetActive(false);
        }
        else
        {
            icons[0].SetActive(true);

            skillImages[0].sprite = SkillInven.instance.godSkill.skillImage;
            firstCd = SkillInven.instance.godSkill.cooldown;

            if (Zeus.cdWork || Ares.cdWork)
            {
                afterFirstSkill += Time.deltaTime;
            }
            else
            {
                afterFirstSkill = 0;
            }
            if (firstCd > 0)
                coolTimes[0].fillAmount = (firstCd - afterFirstSkill) / firstCd;
            else
                coolTimes[0].fillAmount = 0;
        }
        
        if (SkillInven.instance.skills.Count > 0)
        {
            skillImages[1].sprite = SkillInven.instance.skills[0].skillImage;
            secondCd = SkillInven.instance.skills[0].cooldown;
            icons[1].SetActive(true);
            if (SkillInven.instance.skills[0].cdWork)
            {
                afterSecondSkill += Time.deltaTime;
            }

            else
            {
                afterSecondSkill = 0;
            }

            coolTimes[1].fillAmount = (secondCd - afterSecondSkill) / secondCd;

            
        }
        else
        {
            icons[1].SetActive(false);
        }

        if (SkillInven.instance.skills.Count > 1)
        {
            skillImages[2].sprite = SkillInven.instance.skills[1].skillImage;
            thirdCd = SkillInven.instance.skills[1].cooldown;
            icons[2].SetActive(true);
            if (SkillInven.instance.skills[1].cdWork)
            {
                afterThirdSkill += Time.deltaTime;
            }
            else
            {
                afterThirdSkill = 0;
            }
            coolTimes[2].fillAmount = (thirdCd - afterThirdSkill) / thirdCd;

        }
        else
        {
            icons[2].SetActive(false);
        }


    }
}
