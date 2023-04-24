using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceUI : MonoBehaviour
{
    Player player;
    bool isEnhance = false;
    float atk;
    float nextAtk;
    float money;
    float charge = 200;
    float dmgIncrease = 20;
    float time = 1f;
    public Text atkText;
    public Text nextAtkText;
    public Text posText;
    public Text moneyText;
    public Text chargeText;
    float possiblity = 0.5f;
    public Text resultText;
    public Text evolutionResultText;
    bool isEvolution = false;
    int enchant;
    int evolutionLevel;
    int evolution;
    public Text eMoneyText;
    public Text eChargeText;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player").transform.Find("Heracles").GetComponent<Player>();
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        atk = player.atk;
        nextAtk = atk + dmgIncrease;
        money = player.gold;
    }
    // Update is called once per frame
    void Update()
    {
        atk = player.atk;
        nextAtk = atk + dmgIncrease;
        money = player.gold;
        enchant = PlayerPrefs.GetInt("WeaponEnhanced");
        atkText.text = $"{atk}";
        nextAtkText.text = $"{nextAtk}";
        posText.text = $"성공확률 : {possiblity * 100}%";
        moneyText.text = $"{money}";
        chargeText.text = $"{charge}";

        eMoneyText.text = $"{money}";
        eChargeText.text = $"{charge * 5}";
       
        evolution = PlayerPrefs.GetInt("WeaponEvolution");
        
        evolutionLevel = evolution * 5 + 10;
        if (money < charge)
        {
            chargeText.color = Color.red;
        }
        else
        {
            chargeText.color = Color.white;
        }
        if (money < charge * 5)
        {
            eChargeText.color = Color.red;
        }
        else
        {
            eChargeText.color = Color.white;
        }
    }


    public void EnhanceTry()
    {
        if (money < charge)
        {
            StartCoroutine(Fail());
        }
        else
        {
            StartCoroutine(Enhance());
        }
    }
    public void EvolutionTry()
    {
        
        if (money < charge * 5)
        {
            StartCoroutine(Fail());
        }
        else
        {
            StartCoroutine(Evolution());
        }
    }
    IEnumerator Fail()
    {
        resultText.color = Color.red;
        resultText.text = "드라크마가 부족합니다.";
        evolutionResultText.color = Color.red;
        evolutionResultText.text = "드라크마가 부족합니다.";
        yield return new WaitForSecondsRealtime(1f);
        resultText.text = "";
        evolutionResultText.text = "";
    }
    IEnumerator Enhance()
    {

       

        if (!isEnhance)
        {
            if (enchant >= 25)
            {
                isEnhance = true;
                resultText.color = Color.red;
                resultText.text = "이미 최대로 강화됐습니다.";
                yield return new WaitForSecondsRealtime(0.5f);
                resultText.text = "";
                isEnhance = false;

            }
            else if (enchant >= evolutionLevel)
            {
                isEnhance = true;
                resultText.color = Color.red;
                resultText.text = "현재 강화가능한 한계까지 강화했습니다.";
                yield return new WaitForSecondsRealtime(0.5f);
                resultText.text = "먼저 무기의 등급을 강화해주세요";
                yield return new WaitForSecondsRealtime(0.5f);
                resultText.text = "";
                isEnhance = false;
            }
            else
            {
                isEnhance = true;
                float rand = Random.Range(0f, 1f);

                resultText.color = Color.white;
                resultText.text = "강화중...";

                yield return new WaitForSecondsRealtime(time);
                if (rand <= possiblity)
                {
                    player.atk += dmgIncrease;
                    PlayerPrefs.SetFloat("PlayerAtk", player.atk);
                    player.atk = PlayerPrefs.GetFloat("PlayerAtk");
                    resultText.text = "강화성공!";
                    resultText.color = Color.green;
                    PlayerPrefs.SetInt("WeaponEnhanced", enchant + 1);
                }
                else
                {
                    resultText.text = "강화실패!";
                    resultText.color = Color.red;
                }
                player.gold -= charge;
                PlayerPrefs.SetFloat("Money", player.gold);

                yield return new WaitForSecondsRealtime(0.5f);
                resultText.text = "";
                isEnhance = false;
            }
           
        }


    }
    IEnumerator Evolution()
    {
        if (!isEvolution)
        {
            if (evolution >= 3)
            {
                isEvolution = true;
                evolutionResultText.color = Color.red;
                evolutionResultText.text = "이미 최고등급입니다.";
                yield return new WaitForSecondsRealtime(0.5f);
                evolutionResultText.text = "";
                isEvolution = false;
            }
            else
            {
                if (enchant < evolutionLevel)
                {
                    isEvolution = true;
                    evolutionResultText.color = Color.red;
                    evolutionResultText.text = "무기의 강화레벨이 낮습니다.";
                    yield return new WaitForSecondsRealtime(0.5f);
                    evolutionResultText.text = "먼저 무기를 강화해주세요";
                    yield return new WaitForSecondsRealtime(0.5f);
                    evolutionResultText.text = "";
                    isEvolution = false;
                }
                else
                {
                    isEvolution = true;

                    PlayerPrefs.SetInt("WeaponEvolution", evolution + 1);

                    yield return new WaitForSecondsRealtime(time);
                    evolutionResultText.text = "무기의 등급이 올라갔습니다.";
                    evolutionResultText.color = Color.green;
                    player.gold -= charge * 5;
                    PlayerPrefs.SetFloat("Money", player.gold);
                    yield return new WaitForSecondsRealtime(1.0f);
                    evolutionResultText.text = "";
                    isEvolution = false;
                }
            }
            

        }


    }
    public void Close()
    {
        if (!isEnhance)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}
