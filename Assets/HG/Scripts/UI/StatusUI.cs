using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    Player player;
    float curHp;
    float maxHp;
    float att;
    float def;
    float critProb;

    public Text attText;
    public Text HpText;
    public Text defText;
    public Text critProbText;
    public Text critDmgText;
    public Text goldGainText;
    public Text skillDmgText;
    public Text speedText;
    public Text durabilityText;
    public Text atkSpeedText;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform.Find("Heracles").GetComponent<Player>();
    }

    private void OnEnable()
    {
        player = GameObject.Find("Player").transform.Find("Heracles").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        curHp = Mathf.Round(player.currHp);
        maxHp = Mathf.Round(player.maxHp);
        att = Mathf.Round(player.dmg);
        def = Mathf.Round(player.def);
        critProb= Mathf.Round(player.critProb);
        Mathf.Clamp(critProb, 0, 1);

        HpText.text = $"체력 : {curHp}/{maxHp}";
        attText.text = $"공격력 : {att}";
        defText.text = $"방어력 : {def}";
        critProbText.text = $"크리티컬 확률 : {critProb * 100}%";
        critDmgText.text = $"크리티컬 데미지 : {player.critDmg * 100}%";
        goldGainText.text = $"골드 획득량 : {player.goldGain * 100}%";
        skillDmgText.text = $"스킬 데미지 : {player.skillDmg}%";
        speedText.text = $"이동속도 : {player.speed*100/15}%";
        durabilityText.text = $"내구도 : {player.weapon_Durability}/100";
        atkSpeedText.text = $"공격속도 : {player.atkSpeed * 100}%";
    }

}
