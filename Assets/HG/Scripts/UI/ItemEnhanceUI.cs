using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEnhanceUI : MonoBehaviour
{
    List<ItemInfo> itemList; // 인벤토리 아이템 목록
    public Image itemImage; // 아이템 이미지
    public Text itemName; // 아이템 이름 텍스트
    public Text enhanceDescription; // 아이템 강화 설명 텍스트
    public Text price; // 강화비용 텍스트
    public GameObject refuse; // 강화불가 버튼
    public GameObject complete; // 강화 완료창
    public Button enhance; // 강화 버튼
    int select; // 선택 아이템 인덱스
    Player player; // 플레이어
    bool isEnhance = false; //코루틴 제어함수

    void Start()
    {
        itemList = Equipment.instance.itemList; // 인벤토리 아이템 목록 불러오기
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); // 플레이어 찾기
    }

    private void OnEnable()
    {
        itemList = Equipment.instance.itemList; // 인벤토리 아이템 목록 불러오기

        select = 0; // 인덱스 0으로 초기화
        Set(); // 페이지 새로고침
    }
    void Update()
    {
        // 화살표키로 페이지 넘기기

        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            if (select >= itemList.Count-1)
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
                select--;

            Set();
        }
    }

    public void EnhanceTry() // 강화 시도
    {
        if (itemList.Count > 0) // 인벤토리에 아이템이 있을시
        {
            bool isPossible = true; // 강화가능 판단변수

            if (player.gold < itemList[select].price) // 소지금이 부족할시
                isPossible = false; // 강화불가

            if (isPossible) // 강화가 가능할시 강화 코루틴 함수 호출
                StartCoroutine(Enhance());
        }
    }

    IEnumerator Enhance() // 강화 코루틴 함수
    {
        if (!isEnhance) // 코루틴 제어변수
        {
            isEnhance = true; // 코루틴 비활성화

            // 아이템 강화 인터페이스 불러오기
            IOnItemEnhance e = itemList[select].GetComponent<IOnItemEnhance>(); 

            if (e != null) // 강화 인터페이스가 있을시
            {
                e.Enhance(); // 강화함수 호출
                complete.SetActive(true); // 강화완료창 활성화
                itemList[select].isEnhanced = true; // Iteminfo 강화변수 true
                Set(); // 페이지 새로고침
                yield return new WaitForSecondsRealtime(1f); //1초 대기
                complete.SetActive(false); // 강화완료창 비활성화
            }

            // 아이템 설명 업데이트 인터페이스 불러오기
            IonItemRename r = itemList[select].GetComponent<IonItemRename>();

            if (r != null) // 인터페이스가 있을시
            {
                r.Rename(); // 아이템 설명 업데이트 (강화 후 내용)
            }
            isEnhance = false; // 코루틴 활성화
        }
    }

    void Set() // 페이지 새로고침
    {       
        if (itemList.Count <= 0) // 아이템이 없을시
        {
            itemImage.gameObject.SetActive(false); // 아이템 이미지 비활성화
            itemName.text = "-"; // 이름 제거
            enhanceDescription.text = "-"; // 내용 제거
            price.text = "-"; // 비용 제거
        }
        else
        {
            itemImage.gameObject.SetActive(true); // 아이템 이미지 활성화
            ItemInfo item = itemList[select]; // 선택중인 아이템 불러오기
            itemName.text = item.name; // 아이템 이름 업데이트
            itemImage.sprite = item.image; // 아이템 이미지 업데이트

            //이미 강화된 아이템이거나 강화 함수를 가지고있지 않을시
            if (item.isEnhanced || item.GetComponent<IOnItemEnhance>() == null)
            {
                price.text = "-"; // 비용 제거
                enhanceDescription.text = "아이템을 더이상 강화할 수 없습니다.";
                refuse.SetActive(true); // 강화불가 버튼 활성화
                enhance.gameObject.SetActive(false); //강화버튼 비활성화
            }
            else // 강화가 가능할시
            {
                enhanceDescription.text = item.enhanceDescription; //강화 설명 업데이트
                price.text = item.enhancePrice.ToString(); // 강화비용 업데이트
                refuse.SetActive(false); // 강화불가 버튼 비활성화
                enhance.gameObject.SetActive(true); // 강화버튼 활성화
            }
            switch (item.rank) // 아이템 등급 확인
            {
                //등급에 맞춰 이름 색상 변경

                case ItemInfo.Rank.Rare:
                    itemName.color = Color.yellow;
                    break;
                case ItemInfo.Rank.Unique:
                    itemName.color = new Color32(208, 57, 250, 255);
                    break;
                case ItemInfo.Rank.Legend:
                    itemName.color = Color.red;
                    break;
            }
        }
    }

    public void Close() // 창닫기
    {
        gameObject.SetActive(false);
    }
}
