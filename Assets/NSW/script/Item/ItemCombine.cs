using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCombine : MonoBehaviour
{
    public List<MaterialInfo> materials;
    public Button[] RareBtn;
    public Transform Result;
    public Transform ResultImage;
    public Text ResultText;
    Player player;
    public Button[] material;
    Image ResultSprite;
    public Sprite defaultSprite;
    public GameObject CombineObject;
    int materialNum = 0; //재료 숫자 
    // Start is called before the first frame update
    void Start()
    {
        Result.gameObject.SetActive(false);
        ResultSprite = ResultImage.GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        for (int i = 0; i < material.Length; i++)
        {

            material[i].GetComponent<Image>().sprite = defaultSprite;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(ItemInfo item) // 재료 넣기 
    {
        if (item.isMaterial || !Equipment.instance.itemList.Contains(item)) return;
        for (int i = 0; i < material.Length; i++)
        {
            if (material[i].GetComponent<Image>().sprite == defaultSprite)
            {
                material[i].GetComponent<Image>().sprite = item.image;
                material[i].GetComponent<MaterialBtn>().currentItem = item;
                item.isMaterial = true;
                break;
            }
        }
        materialNum++;
    }
    public void DeleteMaterial(int index) //재료빼기 
    {
        {
            material[index].GetComponent<Image>().sprite = defaultSprite;
            material[index].GetComponent<MaterialBtn>().currentItem.isMaterial = false;
            material[index].GetComponent<MaterialBtn>().currentItem = null;
            materialNum--;
        }
    }
    public void Combine() // 조합 
    {
        if (materialNum < 3) return; // 재료 숫자가 3개가 아니라면 리턴 
        int ran = Random.Range(0, materials.Count);


        Material.instance.Add(materials[ran]);

        Result.gameObject.SetActive(true);
        ResultSprite.sprite = materials[ran].image;
        ResultText.text = materials[ran].name;


        for (int i = 0; i < material.Length; i++)
        {
            Equipment.instance.Remove(material[i].GetComponent<MaterialBtn>().currentItem);
            material[i].GetComponent<MaterialBtn>().currentItem.isMaterial = false;
            material[i].GetComponent<MaterialBtn>().currentItem = null;

            material[i].GetComponent<Image>().sprite = defaultSprite;
        }
    }
    public void ClosedCombine()
    {
        CombineObject.SetActive(false);
    }

}
