using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Text[] description;
    public Text[] skillName;
    public Text[] skillCd;
    public Image[] skillImage;
    int page;
    int skillNum = 2;
    public GameObject[] skill;

    private void OnEnable()
    {
        page = 0;
        SetPage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Next();
           
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Before();  
        }
    }
    public void Next()
    {
        if (page * skillNum + skillNum < SkillInven.instance.skills.Count - 1)
        {
            page++;
        }
        else
        {
            page = 0;
        }
        SetPage();
    }

    public void Before()
    {
        if (page <= 0)
        {
            page = (SkillInven.instance.skills.Count - 1) / skillNum;
        }
        else
            page--;

        SetPage();
    }
    void SetPage()
    {
        int num = skillNum;

        
        if (skillNum > SkillInven.instance.skills.Count - page * skillNum)
        {
            num = SkillInven.instance.skills.Count - page * skillNum;
        }

        for(int i= 0; i < num; i++)
        {
            skill[i].SetActive(true);
            description[i].text = SkillInven.instance.skills[page*skillNum+i].description;
            skillName[i].text = SkillInven.instance.skills[page*skillNum+i].skillName;
            skillCd[i].text = SkillInven.instance.skills[page*skillNum+i].cooldown.ToString() + "초";
            description[i].text = SkillInven.instance.skills[page*skillNum+i].description;
            skillImage[i].sprite = SkillInven.instance.skills[page * skillNum + i].skillImage;
        }

        for(int i = num; i < skillNum; i++)
        {
            skill[i].SetActive(false);
        }
    }
}
