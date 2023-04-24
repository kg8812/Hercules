using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public static Equipment instance;   //싱글톤용 인스턴스 변수
    public List<ItemInfo> itemList = new List<ItemInfo>();  // 장비 아이템 리스트
    public GameObject[] items;  // 아이콘 오브젝트 (활성화, 비활성화용)
    public GameObject[] sell;   // 아이템 판매하기 오브젝트 (활성화, 비활성화용)
    public Image[] itemImage;   // 아이템 이미지
    public Image[] sellButton;  // 판매 단축키 이미지
    public Text[] description;  // 아이템 설명
    public Text[] itemName; // 아이템 이름
    public Text[] sellPrice;    // 아이템 되팔기 가격
    public Text pageText;   // 페이지
    public Text moneyText;  // 소지금
    public List<ItemInfo> blueprint = new List<ItemInfo>();
    Player player;  // 플레이어

    int page = 0;   // 현재 페이지
    int itemNum = 4;    // 페이지당 아이템 개수
    ItemInfo select;    // 선택중인 아이템
    int selNum; // 선택중인 아이템의 인덱스
    int num; // 남은 아이템 개수를 계산하기위한 변수
    bool isSell = false;    // 판매 코루틴 제어함수

    void Awake()
    {

        if (instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        if (GameObject.FindWithTag("Player") != null)
            player = GameObject.FindWithTag("Player").GetComponent<Player>();

        foreach (ItemInfo x in itemList)
        {
            IonItemRename r = x.GetComponent<IonItemRename>();

            if (r != null)
            {
                r.Rename();
            }
        }

        selNum = 0;
        page = 0;
        Page();
    }

    void Update()
    {
        moneyText.text = player.gold.ToString();
        if (!Input.GetKey(KeyCode.Z))
        {

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (page * itemNum + itemNum < itemList.Count)
                {
                    page++;
                }
                else
                {
                    page = 0;
                }

                Page();

            }
            else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (page <= 0)
                {
                    page = (itemList.Count - 1) / itemNum;
                }
                else
                    page--;

                Page();
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                sell[selNum].SetActive(false);
                selNum++;

                Choice(1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                sell[selNum].SetActive(false);

                selNum--;

                Choice(1);
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                sell[selNum].SetActive(false);
                selNum -= 2;

                Choice(2);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                sell[selNum].SetActive(false);

                selNum += 2;
                Choice(2);
            }

        }

        if (Input.GetKey(KeyCode.Z) && !isSell)
        {
            sellButton[selNum].fillAmount += Time.unscaledDeltaTime * 2;
            if (sellButton[selNum].fillAmount >= 1)
            {
                StartCoroutine(Sell());
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            sellButton[selNum].fillAmount = 0;
        }

    }

    IEnumerator Sell()
    {
        if (!isSell)
        {

            isSell = true;
            IOnItemSell s = select.GetComponent<IOnItemSell>();
            if (s != null)
            {
                s.ItemSell();
                
                player.gold += select.reSellPrice;
                sellButton[selNum].fillAmount = 0;
                itemList.Remove(select);
                select.isResold = true;
                select.isEnhanced = false;
                select.isOwn = false;
                Page();
                yield return new WaitForSecondsRealtime(1f);
            }
            isSell = false;
        }
    }

    public void ResetItem()
    {
        int legendCount = 0;

        foreach (ItemInfo item in itemList)
        {
            if (item.rank == ItemInfo.Rank.Legend)
            {
                legendCount++;
            }
        }
        while (itemList.Count - legendCount > 0)
        {
            foreach (ItemInfo item in itemList)
            {
                if (item.rank != ItemInfo.Rank.Legend)
                {
                    Remove(item);
                    break;
                }
            }
        }
    }
    public void Remove(ItemInfo item)
    {
        IOnItemSell s = item.GetComponent<IOnItemSell>();
        if (s != null)
        {
            s.ItemSell();
            item.isEnhanced = false;
            item.isResold = true;
            item.isOwn = false;
            itemList.Remove(item);
            Page();
        }

    }
    public void Add(ItemInfo item)
    {
        itemList.Add(item);
        item.isOwn = true;
        item.isSold = true;
    }

    void Page()
    {
        for(int i = 0; i < itemNum; i++)
        {
            sell[i].SetActive(false);
        }
        
        selNum = 0;

        Choice(0);
        pageText.text = $"{page + 1} / {(itemList.Count - 1) / itemNum + 1}";
        num = itemNum;
        if (itemList.Count - page * itemNum < num)
        {
            num = itemList.Count - page * itemNum;
        }

        for (int i = 0; i < num; i++)
        {

            items[i].SetActive(true);
            itemImage[i].sprite = itemList[page * itemNum + i].image;
            itemName[i].text = itemList[page * itemNum + i].name;
            description[i].text = itemList[page * itemNum + i].description;
            sellPrice[i].text = itemList[page * itemNum + i].reSellPrice.ToString();

            switch (itemList[page * itemNum + i].rank)
            {
                case ItemInfo.Rank.Rare:
                    itemName[i].color = Color.yellow;
                    break;
                case ItemInfo.Rank.Unique:
                    itemName[i].color = new Color32(208, 57, 250, 255);
                    break;
                case ItemInfo.Rank.Legend:
                    itemName[i].color = Color.red;
                    break;
            }
        }

        for (int i = num; i < itemNum; i++)
        {
            items[i].SetActive(false);
        }
    }

    void Choice(int n)
    {

        if (selNum >= num)
        {
            if (num <= n)
                selNum -= n;
            else
                selNum -= num;
        }
        if (selNum < 0)
        {
            if (num <= n)
                selNum += n;
            else
                selNum += num;
        }

        if (itemList.Count > 0)
        {
            select = itemList[selNum + page * itemNum];
            sell[selNum].SetActive(true);
        }

    }

    public void UseItems()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].reUseNeeded)
            {
                itemList[i].GetComponent<IOnItemUse>().Use();
            }
        }
    }


}
