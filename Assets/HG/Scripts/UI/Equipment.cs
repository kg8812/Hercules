using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public static Equipment instance;   //�̱���� �ν��Ͻ� ����
    public List<ItemInfo> itemList = new List<ItemInfo>();  // ��� ������ ����Ʈ
    public GameObject[] items;  // ������ ������Ʈ (Ȱ��ȭ, ��Ȱ��ȭ��)
    public GameObject[] sell;   // ������ �Ǹ��ϱ� ������Ʈ (Ȱ��ȭ, ��Ȱ��ȭ��)
    public Image[] itemImage;   // ������ �̹���
    public Image[] sellButton;  // �Ǹ� ����Ű �̹���
    public Text[] description;  // ������ ����
    public Text[] itemName; // ������ �̸�
    public Text[] sellPrice;    // ������ ���ȱ� ����
    public Text pageText;   // ������
    public Text moneyText;  // ������
    public List<ItemInfo> blueprint = new List<ItemInfo>();
    Player player;  // �÷��̾�

    int page = 0;   // ���� ������
    int itemNum = 4;    // �������� ������ ����
    ItemInfo select;    // �������� ������
    int selNum; // �������� �������� �ε���
    int num; // ���� ������ ������ ����ϱ����� ����
    bool isSell = false;    // �Ǹ� �ڷ�ƾ �����Լ�

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
