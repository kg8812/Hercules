using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurabilityUI : MonoBehaviour
{
    public Image weaponImage;   // 무기 이미지 변수
    public Sprite[] weapons;    // 무기 스프라이트 저장용

    float value;    // 무기 내구도 저장변수

    void Update()
    {
        switch (PlayerPrefs.GetInt("PlayerWeapon"))
        {
            case 1:
                weaponImage.sprite = weapons[0];
                break;
            case 2:
                weaponImage.sprite = weapons[1];
                break;
            case 3:
                weaponImage.sprite = weapons[2];
                break;
            default:
                weaponImage.sprite = weapons[3];
                break;
        }
        value = PlayerPrefs.GetFloat("Weapon_Durability");
        weaponImage.color = new Color(1, value / 100, value / 100, 1);
    }
}
