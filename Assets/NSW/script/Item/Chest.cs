using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Chest : MonoBehaviour
{
    public Sprite openChest;    // 열린 상자 스프라이트
    public GameObject hpPortion;    // 즉시 사용 포션
    SpriteRenderer image;   // 스프라이트 렌더러 (이미지 관리 컴포넌트)
    BoxCollider2D boxCollider2D;    // 콜라이더
    public GameObject bluePrint;    // 설계도
    public GameObject atkSpellBook; // 공격 주문서
    public GameObject crateSpellBook; // 크리티컬 확률 주문서
    public GameObject cdmgSpellBook; // 크리티컬 데미지 주문서
    public GameObject[] consumablePotion;   // 소모 포션
    public GameObject[] ambrosia;   // 버프아이템

    // Start is called before the first frame update
    void Start()
    {

        image = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
      
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boxCollider2D.enabled = false;
            image.sprite = openChest;
            this.gameObject.layer = 19;
            Destroy(this.gameObject, 3f);

            int r = Random.Range(0, 100);
            if (r >= 90)
            {
                Instantiate(hpPortion, new Vector2(this.transform.position.x, this.transform.position.y + 10), this.transform.rotation);
            }
            else if (r >= 80)
            {
                Instantiate(bluePrint, new Vector2(this.transform.position.x, this.transform.position.y + 10), this.transform.rotation);
            }
            else if (r >= 70)
            {
                Instantiate(atkSpellBook, new Vector2(this.transform.position.x, this.transform.position.y + 10), this.transform.rotation);
            }
            else if (r >= 60)
            {
                Instantiate(crateSpellBook, new Vector2(this.transform.position.x, this.transform.position.y + 10), this.transform.rotation);
            }
            else if(r>=50)
            {
                Instantiate(cdmgSpellBook, new Vector2(this.transform.position.x, this.transform.position.y + 10), this.transform.rotation);
            }
            else if(r>=25)
            {
                Instantiate(consumablePotion[Random.Range(0, consumablePotion.Length)], new Vector2(this.transform.position.x, this.transform.position.y + 10), this.transform.rotation);
            }
            else
            {
                Instantiate(ambrosia[Random.Range(0, ambrosia.Length)], new Vector2(this.transform.position.x, this.transform.position.y + 10), this.transform.rotation);
            }
        }



    }
}
