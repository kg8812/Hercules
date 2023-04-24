using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager:MonoBehaviour
{
    public static EffectManager instance;
    public GameObject landEffect1;
    public GameObject landEffect2;
    public GameObject shieldEffect;
    public GameObject autoAttackEffect;
    public GameObject bonusAttackEffect;
    public GameObject thunderRushEffect;
    public GameObject thunderEffect1;
    public GameObject shockWaveEffect;
    public GameObject thunderArrowEffect;
    public GameObject stingEffect;
    public GameObject stingRushEffect;
    public GameObject FireArrowEffect;
    public GameObject FireBowEffect;
    public GameObject tornadoEffect;
    public GameObject bloodEffect;
    public GameObject cureseEffect;
    public AnimationClip curseAni;
    public GameObject dmgText;
    public GameObject ThunderBow;
    public GameObject dragonBreathEffect;
    public GameObject LionEffect;
    public GameObject stunEffect;
    public GameObject poisonEffect;
    public GameObject chest;
    private void Awake()
    {     
        instance = this;
    }
}
