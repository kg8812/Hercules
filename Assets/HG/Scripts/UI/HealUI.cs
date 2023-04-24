using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealUI : MonoBehaviour
{
    public ConsumableInfo[] potion;
    public ConsumableInfo[] ambrosia;


    public Image healSelect;
    public Text healPrice;

    public Image buffSelect;
    public Text buffPrice;

    float money;
    public Text moneyText;

    bool isHeal;
    bool isBuy = false;
    bool isYes = true;

    Player player;

    public GameObject complete;
    public Text completeText;
    public GameObject y;
    public GameObject n;

    int price = 200;

    private void Awake()
    {
        player = GameObject.Find("Player").transform.Find("Heracles").GetComponent<Player>();
    }


    void Start()
    {

    }
    private void OnEnable()
    {
        Time.timeScale = 0;
        isYes = true;
        isHeal = true;
        healPrice.text = price.ToString();
        buffPrice.text = price.ToString();
    }
    void Update()
    {
        money = player.gold;
        moneyText.text = $"{money}";

        if (isHeal)
        {
            healSelect.gameObject.SetActive(true);
            buffSelect.gameObject.SetActive(false);
        }
        else
        {
            healSelect.gameObject.SetActive(false);
            buffSelect.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            Change();

        if (Input.GetKey(KeyCode.Z))
        {
            StartCoroutine(Buy());

        }

        if (isYes)
        {
            y.gameObject.SetActive(true);
            n.gameObject.SetActive(false);
        }
        else
        {
            y.gameObject.SetActive(false);
            n.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isYes) isYes = false;
            else isYes = true;
        }

    }


    void Change()
    {
        isYes = true;
        if (isHeal)
        {
            isHeal = false;
        }
        else
        {
            isHeal = true;
        }
    }

    IEnumerator Buy()
    {
        if (!isBuy)
        {
            if (isYes)
            {
                if (price <= player.gold)
                {
                    isBuy = true;
                    ConsumableInfo rand;

                    if (isHeal)
                    {
                        rand = potion[Random.Range(0, potion.Length)];                     
                    }
                    else
                    {
                        rand = ambrosia[Random.Range(0, ambrosia.Length)];
                    }
                    completeText.text = $"{rand.name}을(를) 제작하였습니다.";
                    complete.gameObject.SetActive(true);
                    player.gold -= price;
                    PlayerPrefs.SetFloat("Money", player.gold);
                    Consumable.instance.Add(rand);

                    yield return new WaitForSecondsRealtime(1f);
                    complete.gameObject.SetActive(false);
                    isBuy = false;
                }
            }
            else
                Close();
        }
    }

    public void Close()
    {

        Time.timeScale = 1;
        gameObject.SetActive(false);

    }
}
