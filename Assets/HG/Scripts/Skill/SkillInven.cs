using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInven : MonoBehaviour
{
    public static SkillInven instance;

    public List<Skill> skills = new List<Skill>();
    public Skill godSkill;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Add(Skill skill)
    {
        if (skill.isGod)
        {
            godSkill = skill;
        }
        else
        {
            if (!skills.Contains(skill))
                skills.Add(skill);
        }
    }

    public void Remove(Skill skill)
    {
        if (skills.Contains(skill))
            skills.Remove(skill);
    }

    public void Clear()
    {
        skills.Clear();
    }

    public void NewGame()
    {
        Clear();
        godSkill = null;
    }
}
