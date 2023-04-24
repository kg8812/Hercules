using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager:MonoBehaviour
{
    public static SkillManager instance;

    public Skill[] aresSkill;
    public Skill[] zeusSkill;
    public Skill[] hadesSkill;
    public Skill[] hermesSkill;
    public Skill lionRoar;
    public Skill dragonBreath;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

}
