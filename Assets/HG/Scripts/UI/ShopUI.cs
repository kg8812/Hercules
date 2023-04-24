using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public ItemInfo[] iteminfo; // 상점 아이템 전체목록
    public Text[] nameText; // 아이템 이름 텍스트
    public Text[] priceText; // 아이템 가격 텍스트
    public Text[] priceText2; // 설명창내 아이템 가격 텍스트
    public Text[] descriptionText; // 아이템 설명 텍스트
    public Image[] itemImage; // 아이템 이미지
    public Image[] arrow; // 선택 화살표 이미지
    public GameObject[] description; // 설명창 오브젝트
    public Player player; // 플레이어
    int[] num = new int[4]; // 현재 표시중인 아이템들의 전체 아이템 배열내 인덱스
    List<int> list = new List<int>(); // 현재 표시중인 아이템 리스트
    float money; // 소지금
    float rerollPrice = 500; // 새로고침 비용
    public Text moneyText; // 소지금 텍스트
    public Text rerollText; // 새로고침 비용 텍스트
    public Text completeText; // 구매 완료 텍스트
    public GameObject complete; // 구매 완료창
    public GameObject rerollFail; // 리롤 실패창
    public GameObject purchaseFail; // 구매 실패창

    ItemInfo selected; // 현재 선택된 아이템
    int sNum; // 선택된 아이템의 인덱스
    bool isBuy = false; // 구매 코루틴 제어 변수
    bool isFail = false; // 실패 코루틴 제어 변수
    int soldItem = 0;

    private void Awake()
    {
        // 전체 아이템 비용 설정 및 판매된 아이템 개수 확인
        for (int i = 0; i < iteminfo.Length; i++)
        {
            iteminfo[i].PriceSet();
            if (iteminfo[i].isSold) soldItem++;
        }
        Reroll(); // 새로고침
    }


    private void OnEnable()
    {
        // 모든 아이템 설명 새로고침
        foreach (ItemInfo x in iteminfo)
        {
            IonItemRename r = x.GetComponent<IonItemRename>();

            if (r != null)
            {
                r.Rename(); // 설명 새로고침 함수 호출
            }
        }

        Time.timeScale = 0; // 시간 정지
        sNum = 0; // 인덱스 0으로 초기화

        //플레이어 찾기
        player = GameObject.Find("Player").transform.Find("Heracles").GetComponent<Player>();
    }


    void Update()
    {
        money = player.gold; // 소지금 불러오기
        moneyText.text = $"{money}"; // 소지금 텍스트 업데이트
        rerollText.text = $"{rerollPrice}"; // 새로고침비용 텍스트 업데이트

        ChangeColor(); // 아이템 이름 색상 변경
        Move(); // 아이템 선택 함수 호출 (화살표를 누를시 선택 아이템 변경)

        if (Input.GetKeyDown(KeyCode.B)) //B를 누를시 새로고침시도
            Rerolltry();

        selected = iteminfo[num[sNum]]; // 선택중인 아이템 저장
        arrow[sNum].gameObject.SetActive(true); //선택중인 아이템의 화살표 활성화

        if (!selected.isSold) // 판매된 아이템이 아닐시
            description[sNum].gameObject.SetActive(true); // 아이템 설명 활성화

        if (Input.GetKeyDown(KeyCode.Z)) // Z키를 누를시 구매 시도
        {
            BuyTry();
        }
    }

    void BuyTry() // 아이템 구매 시도
    {
        bool isPos = true; // 아이템 구매가능 판단 변수

        if (selected.price > player.gold) // 소지금이 부족할시 false
            isPos = false;

        else if (selected.isSold) //이미 판매된 아이템일시 false
            isPos = false;

        if (isPos) //아이템 구매
            StartCoroutine(Buy());
    }

    void ChangeColor() // 텍스트 색상 변경
    {
        if (rerollPrice > player.gold) // 리롤비용이 부족할시
            rerollText.color = Color.red; // 리롤비용 색상 붉은색으로 변경
        else // 충분하면 흰색으로 변경
            moneyText.color = Color.white;

        for (int i = 0; i < num.Length; i++) //표시중인 아이템들
        {
            // 소지금이 부족할시 비용 색상 붉은색으로 변경
            if (iteminfo[num[i]].price > player.gold && !iteminfo[num[i]].isSold)
            {
                priceText[i].color = Color.red;
                priceText2[i].color = Color.red;
            }
            else // 충분할시 흰색으로 변경
            {
                priceText[i].color = Color.white;
                priceText2[i].color = Color.white;
            }
        }
    }

    void Move() // 아이템 선택 함수
    {
        // 화살표키를 누를시 선택중인 아이템 변경
        // 선택중인 아이템의 설명창과 선택 화살표 활성화

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            description[sNum].gameObject.SetActive(false);
            arrow[sNum].gameObject.SetActive(false);
            sNum += 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            description[sNum].gameObject.SetActive(false);
            arrow[sNum].gameObject.SetActive(false);
            sNum -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            description[sNum].gameObject.SetActive(false);
            arrow[sNum].gameObject.SetActive(false);
            sNum -= 2;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            description[sNum].gameObject.SetActive(false);
            arrow[sNum].gameObject.SetActive(false);
            sNum += 2;
        }

        //배열의 범위를 벗어날시 값 변경
        if (sNum < 0) sNum += 4;
        else if (sNum > 3) sNum -= 4;
    }

    IEnumerator Buy() // 아이템 구매
    {
        if (!isBuy) // 코루틴 제어 변수
        {
            isBuy = true; // 코루틴 비활성화

            // 아이템 사용하기
            IOnItemUse item = selected.GetComponent<IOnItemUse>();

            if (item != null)
            {
                item.Use();
            }

            // 재판매된 아이템이 아닐시 인벤토리에 아이템 추가
            if (!selected.isResell)
            {
                soldItem++; // 판매된 아이템 개수 증가
                Equipment.instance.Add(selected); // 인벤토리에 아이템 추가
            }

            nameText[sNum].color = Color.white; // 이름 색상 흰색으로 변경
            player.gold -= selected.price; // 판매비용만큼 소지금 감소
            PlayerPrefs.SetFloat("Money", player.gold); // PlayerPref에 소지금 저장
            completeText.text = $"{selected.name}을(를) 구매하였습니다!"; // 구매완료창 텍스트 업데이트
            complete.SetActive(true); // 구매창 활성화
                     
            selected.isSold = true; //구매된 아이템으로 체크 (상점에서 안나옴)

            description[sNum].SetActive(false); // 설명창 비활성화 
            nameText[sNum].text = "-"; // 이름 텍스트 비움
            itemImage[sNum].gameObject.SetActive(false); // 아이템 이미지 비활성화
            priceText[sNum].text = "-"; // 판매비용 텍스트 비움
            yield return new WaitForSecondsRealtime(1f); // 1초대기
            complete.SetActive(false); // 구매완료창 비활성화

            isBuy = false; // 코루틴 활성화

        }
    }
    public void Close() // 상점 창 닫기
    {
        if (!isBuy) // 구매중이 아닐시 (구매중에 종료하면 코루틴 중지됨)
        {
            // 선택창 비활성화 (다시 켜졌을때 보이지 않도록)
            description[sNum].gameObject.SetActive(false);
            arrow[sNum].gameObject.SetActive(false);
            gameObject.SetActive(false); // 상점창 비활성화
            Time.timeScale = 1; // 시간정지 해제
        }
    }

    IEnumerator NoItem() // 아이템이 부족해 새로고침 실패
    {
        if (!isFail) // 코루틴 제어 변수
        {
            isFail = true; // 코루틴 비활성화
            rerollFail.SetActive(true);     // 실패창 활성화
            yield return new WaitForSecondsRealtime(1f); //1초대기          
            rerollFail.SetActive(false); // 실패창 비활성화
            isFail = false; // 코루틴 활성화
        }
    }

    public void Rerolltry() // 새로고침 시도
    {
        bool isPossible = true; // 새로고침 가능여부 판단 변수
        int count = 0; // 남은 아이템 개수

        // 남은 아이템 세기
        for (int i = 0; i < iteminfo.Length; i++)
        {
            if (num.Contains(i)) // 이미 표시중인 아이템이면 생략
                continue;

            if (!iteminfo[i].isSold) // 판매된 아이템이 아닐시
            {
                count++; // 남은 아이템 개수 증가
                break;
            }
        }
        if (count == 0) // 남은 아이템이 없을시
        {
            isPossible = false; // 새로고침 불가
            StartCoroutine(NoItem()); // 새로고침 실패 함수 호출
        }

        if (player.gold < rerollPrice) isPossible = false; // 소지금이 부족하면 false

        if (isPossible) // 새로고침이 가능할시
        {
            player.gold -= rerollPrice; // 새로고침 비용만큼 소지금 감소
            Reroll(); // 새로고침
        }
    }
    void Reroll() // 새로고침
    {

        list.Clear(); // 표시중인 목록 비우기

        // 재판매된 아이템 다시 상점에서 활성화
        foreach (ItemInfo x in iteminfo) 
        {
            if (x.isResold && x.isSold)
            {
                x.isResold = false; // 재판매 false
                x.isSold = false; // 판매됨 false
            }

            if (x.isResell)
            {
                x.isSold = false;
            }
        }

        
        //랜덤 아이템 선택
        for (int i = 0; i < num.Length; i++)
        {
            
            itemImage[i].gameObject.SetActive(true); // 아이템 이미지 활성화

            int rand = Random.Range(0, iteminfo.Length); // 랜덤값 생성

            //랜덤한 아이템 선택
            while (list.Contains(rand) || iteminfo[rand].isSold)
            {
                if (list.Count >= (iteminfo.Length - soldItem))
                {
                    while (!iteminfo[rand].isSold)
                    {
                        rand = Random.Range(0, iteminfo.Length);
                    }
                    break;
                }
                rand = Random.Range(0, iteminfo.Length);
            }
            list.Add(rand); // 선택된 아이템 표시목록에 추가
            num[i] = rand; // 선택된 아이템의 인덱스 저장
        }

        //아이템 목록 새로고침
        for (int i = 0; i < num.Length; i++)
        {
            // 아이템 정보 변경
            nameText[i].text = iteminfo[num[i]].name; // 아이템 이름
            priceText[i].text = $"{iteminfo[num[i]].price}"; // 아이템 가격
            priceText2[i].text = $"{iteminfo[num[i]].price}"; // 설명창내 아이템 가격
            descriptionText[i].text = iteminfo[num[i]].description; // 아이템 설명
            itemImage[i].sprite = iteminfo[num[i]].image; // 아이템 이미지

            if (iteminfo[num[i]].isSold) // 판매된 아이템일시
            {
                // 해당 목록 비우기
                description[i].SetActive(false);
                nameText[i].text = "-";
                itemImage[i].gameObject.SetActive(false);
                priceText[i].text = "-";
            }

            switch (iteminfo[num[i]].rank) // 아이템의 등급 확인
            {
                // 등급에 맞춰 이름 색상 변경
                case ItemInfo.Rank.Rare:    // 레어는 노란색
                    nameText[i].color = Color.yellow;
                    break;
                case ItemInfo.Rank.Unique: // 유니크는 보라색
                    nameText[i].color = new Color32(208, 57, 250, 255);
                    break;              
                default:
                    break;
            }
        }
    }
}
