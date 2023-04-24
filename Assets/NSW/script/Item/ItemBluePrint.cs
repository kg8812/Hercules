using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemBluePrint : MonoBehaviour
{
    public List<ItemInfo> itemList;
    Player player;
    public Image itemImage;
    public Text itemName;
    int select;
    bool isBluePrint = false;
    public Text priceTxt;
    public GameObject Result;
    public Text ResultTxt;
    int price=0;
    // Start is called before the first frame update
    void Start()
    {
        itemList = Equipment.instance.blueprint;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        select = 0;
        Set();
        Result.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (select >= itemList.Count - 1)
                select = 0;
            else
                select++;

            Set();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (select <= 0)
            {
                if (itemList.Count > 0)
                    select = itemList.Count - 1;
                else
                    select = 0;
            }
            else
            {
                select--;
                if (select < 0)
                    select = 0;
            }
               

            Set();
        }
        
    }
    private void OnEnable()
    {
        itemList = Equipment.instance.blueprint;

        select = 0;
        Set();
    }
   

    void Set()
    {
        
        if (itemList.Count <= 0)
        {
            itemImage.gameObject.SetActive(false);
            itemName.text = "-";
            price = 0;
            
        }
        else
        {
            itemImage.gameObject.SetActive(true);
            ItemInfo item = itemList[select];
            itemName.text = item.name;
            itemImage.sprite = item.image;

            if (item.isEnhanced)
            {
              
            }
            else
            {
             
            }
            switch (item.rank)
            {
                case ItemInfo.Rank.Rare:
                    itemName.color = Color.yellow;
                    price = 500;
                    break;
                case ItemInfo.Rank.Unique:
                    itemName.color = new Color32(208, 57, 250, 255);
                    price = 1000;
                    break;
                case ItemInfo.Rank.Legend:
                    itemName.color = Color.red;
                    price = 10000;
                    break;
                default:
                    price = 0;
                    break;
            }
        }
        priceTxt.text = price.ToString();
    }
    public void UseBlueprint()
    {
        if (itemList.Count > 0)
        {
            bool isPossible = true;

            if (player.gold < itemList[select].price)
                isPossible = false;

            if (isPossible)
                StartCoroutine(BluePrint());
        }

    }
    public void Close()
    {
        gameObject.SetActive(false);
    }
    IEnumerator BluePrint()
    {
        if (!isBluePrint)
        {
            isBluePrint = true;
            ItemInfo item = itemList[select];

            if (item != null )
            {
                
                if (player.gold >= price)
                {
                    Result.SetActive(true);
                    ResultTxt.color = Color.white;
                    ResultTxt.text = item.name + " 제작중....";
                    yield return new WaitForSecondsRealtime(1f);
                    ResultTxt.color = Color.green;
                    ResultTxt.text = item.name + " 제작에 성공했습니다!";
                    Equipment.instance.Add(item);
                    item.GetComponent<IOnItemUse>().Use();
                    item.bluePrint = false;
                    Equipment.instance.blueprint.Remove(item);
                    player.gold -= price;
                    PlayerPrefs.SetFloat("Money", player.gold);
                    Set();
                }
                else
                {
                    Result.SetActive(true);
                    ResultTxt.color = Color.red;
                    ResultTxt.text = "드라크마가 부족합니다.";
                }
                
               
            }
            
            yield return new WaitForSecondsRealtime(1f);
            Result.SetActive(false);
            isBluePrint = false;
        }
    }

}
