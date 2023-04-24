using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Consumable : MonoBehaviour
{
    public static Consumable instance;
    public List<ConsumableInfo> itemList = new List<ConsumableInfo>();

    public GameObject[] items;
    public GameObject[] use;

    public Image[] itemImage;

    public Text[] description;
    public Text[] itemName;

    public Text pageText;

    public Text[] itemCount;

    ConsumableInfo select;
    int selNum;
    int page = 0;
    int itemNum = 4;

    int num;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        selNum = 0;

        page = 0;
        Page();
    }
    private void Update()
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

            use[selNum].SetActive(false);
            selNum++;

            Choice(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            use[selNum].SetActive(false);

            selNum--;

            Choice(1);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            use[selNum].SetActive(false);
            selNum -= 2;

            Choice(2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            use[selNum].SetActive(false);

            selNum += 2;
            Choice(2);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Use(select);
        }
    }
    public void Add(ConsumableInfo item)
    {
        if (itemList.Contains(item))
        {
            if (item.isBuff && item.count < 3 || !item.isBuff && item.count < 10)
                item.count++;
        }
        else
        {
            itemList.Add(item);
            item.count = 1;
        }
    }

    public int GetCount(ConsumableInfo item)
    {
        if (itemList.Contains(item))
        {
            return item.count;
        }
        else
        {
            return 0;
        }
    }
    public void Use(ConsumableInfo item)
    {
        if (itemList.Contains(item) && item.count > 0)
        {
            item.GetComponent<IOnItemUse>().Use();
            if (item.count <= 0)
            {
                itemList.Remove(item);
            }
            Page();
        }
    }
    void Page()
    {

        for (int i = 0; i < itemNum; i++)
        {
            use[i].SetActive(false);
        }
        selNum = 0;

        Choice(0);
        pageText.text = $"{page + 1} / {(itemList.Count - 1) / itemNum + 1}";
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
            itemCount[i].text = itemList[page * itemNum + i].count.ToString();
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
            use[selNum].SetActive(true);
        }

    }

    public void ResetItem()
    {
        foreach (ConsumableInfo x in itemList)
        {
            x.count = 0;
        }

        itemList.Clear();
    }
}
