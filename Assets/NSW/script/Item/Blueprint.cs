using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Blueprint : MonoBehaviour
{
    Rigidbody2D body;
    int rarity;
    SpriteRenderer spriteRenderer;
    public Sprite[] image;
    public List<ItemInfo> Legendary;
    public List<ItemInfo> Rare;
    public List<ItemInfo> Normal;
    private void Awake()
    {
        
        float r = Random.Range(0, 100);
        Debug.Log(r);
        rarity = 2;

       
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = this.GetComponent<Rigidbody2D>();
        body.AddForce(Vector2.up * 1600);
        Destroy(this.gameObject, 5f);
        spriteRenderer.sprite = image[rarity];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(rarity == 2)
            {
                int ran = Random.Range(0, Legendary.Count);
                if ( Equipment.instance.itemList.Contains(Legendary[ran]))
                {


                }
                else
                {
                    Legendary[ran].bluePrint = true;
                    Debug.Log(Legendary[ran].name);
                    Equipment.instance.blueprint.Add(Legendary[ran]);
                }
            }
            else if(rarity == 1)
            {
                int ran = Random.Range(0, Rare.Count);
                if (Rare[ran].bluePrint || Equipment.instance.itemList.Contains(Rare[ran]))
                {


                }
                else
                {
                    Rare[ran].bluePrint = true;
                    Equipment.instance.blueprint.Add(Rare[ran]); 
                    Debug.Log(Rare[ran].name);
                }
            }
            else
            {
                int ran = Random.Range(0, Normal.Count);
                if (Normal[ran].bluePrint || Equipment.instance.itemList.Contains(Normal[ran]))
                {


                }
                else
                {
                    Normal[ran].bluePrint = true;
                    Equipment.instance.blueprint.Add(Normal[ran]);
                    Debug.Log(Normal[ran].name);
                }
            }
           

            Destroy(this.gameObject);
            

        }
    }
    
}
