using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Fungus;

public class GameManager : MonoBehaviour
{
    
    public GameManager instance;   
    private static GameManager _instance;  
    public int token;

    public static GameManager Instance // 게임매니저 싱글턴 패턴
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return _instance;
        }
        set
        {

        }
    }
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // 인스턴스 생성
        }
        else if (_instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); //인스턴스가 파괴되지 않도록
        
    }
      
   
    void Update()
    {
        token = PlayerPrefs.GetInt("Token");
       
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.PausedBTN();
        }
    }

    public static void Reset() // 플레이어 능력치등 수치 초기화
    {
        if (PlayerPrefs.GetInt("DearHorn") == 1)
        {
            PlayerPrefs.SetFloat("PlayerMaxHp", 2000);
        }
        else
        {
            PlayerPrefs.SetFloat("PlayerMaxHp", 1000);
        }
        PlayerPrefs.SetFloat("PlayerCurrHp", PlayerPrefs.GetFloat("PlayerMaxHp"));
        PlayerPrefs.SetFloat("PlayerAtk", 5);
        PlayerPrefs.SetFloat("PlayerAtkSpeed",1);
        PlayerPrefs.SetFloat("PlayerAtkRatio", 1);
        PlayerPrefs.SetFloat("PlayerAtkDebuff", 0);
        PlayerPrefs.SetFloat("PlayerDef", 5);
        PlayerPrefs.SetFloat("CurrStage", 0.0f);
        PlayerPrefs.SetFloat("Money", 100000);
        PlayerPrefs.SetFloat("PlayerSpeed", 15);
        PlayerPrefs.SetInt("PlayerWeapon", 0);
        PlayerPrefs.SetFloat("PlayerDashCd", 0.5f);
        PlayerPrefs.SetFloat("PlayerDashPower", 300f);
        PlayerPrefs.SetFloat("PlayerCritProb", 0.3f);
        PlayerPrefs.SetFloat("PlayerCritDmg", 1.5f);
        PlayerPrefs.SetFloat("PlayerGoldGain", 1);
        PlayerPrefs.SetFloat("Weapon_Durability", 100);
        PlayerPrefs.SetFloat("PlayerSkillDmg", 100);
        PlayerPrefs.SetInt("JumpMax", 2);
        PlayerPrefs.SetInt("DashMax", 1);
        PlayerPrefs.SetInt("Achilleus", 0);
        PlayerPrefs.SetInt("Ajax", 0);
        PlayerPrefs.SetInt("God", -1);
        PlayerPrefs.SetInt("Hades", 0);
        PlayerPrefs.SetInt("WingBoots", 0);
        PlayerPrefs.SetInt("HydraPoison", 0);
        PlayerPrefs.SetInt("Dragon",0);
        PlayerPrefs.SetInt("Lion",0);      
        PlayerPrefs.SetInt("WeaponEnhanced", 0);
        PlayerPrefs.SetInt("WeaponEvolution", 0);
        PlayerPrefs.SetInt("Hermes", 0);
        SkillInven.instance.godSkill = null;

    }

}
