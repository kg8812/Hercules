using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="",menuName = "Scriptable/Skill")]
public class Skill:ScriptableObject
{
    public float cooldown;  // 쿨타임
    public Sprite skillImage;   // 스킬 이미지
    public string description;  // 스킬 설명
    public string skillName;    // 스킬 이름
    public bool isGod = false;  // 신능력인지 확인
    public bool isActive;   // 액티브스킬인지 확인
    public bool cdWork = true;  // 쿨타임중인지 확인
}
