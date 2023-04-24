using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Repair : MonoBehaviour
{
    Player player;
    float money;
    float charge = 200;
   
    public Text DurabityText;
    float Durabity;
    
    public Text moneyText;
    public Text chargeText;
    bool isRepair = false;
    public Text resultText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Durabity = PlayerPrefs.GetFloat("Weapon_Durability");
        charge = (100 - Durabity)*50;
        money = player.gold;
    }
    // Update is called once per frame
    void Update()
    {
        Durabity = PlayerPrefs.GetFloat("Weapon_Durability");
        DurabityText.text = $"{Durabity}";
        money = player.gold;
        moneyText.text = $"{money}";
        chargeText.text = $"{charge}";
        if (money < charge)
        {
            chargeText.color = Color.red;
        }
        else
        {
            chargeText.color = Color.white;
        }
    }
    public void RepairStart()
    {
        if (isRepair) return;
        if (money < charge || Durabity>= 100)
        {
            StartCoroutine(Fail());
        }
        else
        {
            StartCoroutine(TryRepair());
        }
    }
    IEnumerator Fail()
    {
        if(Durabity >= 100)
        {
            resultText.color = Color.green;
            resultText.text = "이미 내구도가 최대치입니다.";
        }
        else
        {
            resultText.color = Color.red;
            resultText.text = "드라크마가 부족합니다.";
        }
        
        
        yield return new WaitForSecondsRealtime(1f);
        resultText.text = "";
       
    }
    IEnumerator TryRepair()
    {
        isRepair = true;
        resultText.color = Color.white;
        resultText.text = "수리중...";
        yield return new WaitForSecondsRealtime(1.0f);
        resultText.color = Color.green;
        resultText.text = "수리성공!";
        PlayerPrefs.SetFloat("Weapon_Durability",100);
        Durabity = PlayerPrefs.GetFloat("Weapon_Durability");
        player.gold -= charge;
        PlayerPrefs.SetFloat("Money", player.gold);
        yield return new WaitForSecondsRealtime(0.5f);
        resultText.text = "";
        isRepair = false;
    }
    public void Close()
    {
        if (!isRepair)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
