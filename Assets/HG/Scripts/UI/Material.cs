using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Material : MonoBehaviour
{
    public static Material instance;

    public List<MaterialInfo> itemList = new List<MaterialInfo>();

    public GameObject[] items;

    public Image[] itemImage;

    public Text[] description;
    public Text[] itemName;

    public Text pageText;

    public Text[] itemCount;
    
    int page = 0;
    int itemNum = 6;   

    int num;


    void Awake()
    {
        if (instance == null)
        instance = this;
    }

    private void OnEnable()
    {
        
        page = 0;
        Page();

    }

    void Update()
    {
        if (!Input.GetKey(KeyCode.Z))
        {

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (page * itemNum + itemNum < itemList.Count - 1)
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

        }



    }
    public void Add(MaterialInfo item)
    {
        if (itemList.Contains(item))
        {
            item.count++;
        }
        else
        {
            item.count = 1;
            itemList.Add(item);
        }
    }

    public int GetItemCount(MaterialInfo item)
    {
        if(itemList.Contains(item))
        {
            return item.count;
        }
        else
        {
            return 0;
        }
    }
    public bool Use(int num, MaterialInfo item)
    {
        if (itemList.Contains(item))
        {
            if (item.count >= num)
            {
                item.count -= num;
                if (item.count == 0)
                {
                    itemList.Remove(item);
                    Page();
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    public void Remove(MaterialInfo item)
    {
        itemList.Remove(item);
        item.count = 0;
    }
    void Page()
    {

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


}
