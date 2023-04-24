using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI1 : MonoBehaviour
{
    public ItemInfo[] items; // 전설 아이템 목록

    int select; //선택 아이템 인덱스
    public Image itemImage; // 제작 아이템 이미지
    public Text itemName; // 제작 아이템 이름 텍스트

    public Image[] materialImage; // 재료 아이템 이미지
    public Text[] materialName; // 재료 아이템 이름 텍스트
    public Text[] materialHave; // 재료 아이템 소지수 텍스트 
    public Text[] materialNeed; // 재료 아이템 필요수 텍스트

    public GameObject material; // 재료창 오브젝트
    public GameObject alreadyCrafted; // 이미 제작된 아이템 창
    public GameObject craftSuccess; // 제작 성공창
    public GameObject craftFail; // 제작 실패창
    public GameObject noRecipe; // 설계도 없음 창
    bool isCraftable = true; // 제작 가능 판단변수
    bool isCrafting = false; // 제작 코루틴 제어변수

    private void OnEnable()
    {
        Time.timeScale = 0; //시간정지
        select = 0; // 인덱스 0으로 초기화
        SetPage(); // 페이지 새로고침
    }

    private void OnDisable()
    {
        Time.timeScale = 1; // 시간정지 해제
    }
    void Update()
    {
        // 방향키로 페이지 조절
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            select++;
            if (select >= items.Length)
            {
                select = 0;
            }

            SetPage();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            select--;
            if (select < 0)
            {
                select = items.Length - 1;
            }
            SetPage();
        }

        //Z키로 제작
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCraft();
        }
    }

    void SetPage() // 페이지 새로고침
    {
        isCraftable = true; // 일단 제작가능으로 설정
        ItemInfo sel = items[select]; // 선택중인 아이템 
        itemName.text = sel.name; // 제작 아이템 이름 업데이트
        itemImage.sprite = sel.image; // 제작 아이템 이미지 업데이트

        if (!sel.bluePrint) // 설계도가 없을시
        {
            isCraftable = false; // 제작불가
            noRecipe.SetActive(true); // 설계도 없음창 활성화
            material.SetActive(false); // 재료 아이템창 비활성화
            alreadyCrafted.SetActive(false); // 이미 제작됨창 비활성화
        }
        else if (sel.isCrafted) // 이미 제작된 아이템일시
        {
            isCraftable = false; // 제작불가

            alreadyCrafted.SetActive(true); // 이미 제작됨창 활성화
            material.SetActive(false); // 재료 아이템창 비활성화
            noRecipe.SetActive(false); // 설계도 없음창 비활성화

        }
        else // 제작가능
        {
            material.SetActive(true); // 재료 아이템창 활성화
            noRecipe.SetActive(false); // 설계도 없음창 비활성화
            alreadyCrafted.SetActive(false); // 이미 제작됨창 비활성화
        }

        // 재료 아이템 정보 새로고침
        for (int i = 0; i < sel.recipe.matList.Length; i++)
        {
            materialImage[i].sprite = sel.recipe.matList[i].image; //재료 이미지
            materialName[i].text = sel.recipe.matList[i].name; // 재료 이름
            materialNeed[i].text = sel.recipe.EA[i].ToString(); // 재료 필요수
            materialHave[i].text = $"{Material.instance.GetItemCount(sel.recipe.matList[i])}/{sel.recipe.EA[i]}"; // 재료 소지수

            //재료 소지수가 필요수보다 적을시
            if (sel.recipe.EA[i] > Material.instance.GetItemCount(sel.recipe.matList[i]))
            {
                isCraftable = false; // 제작불가
                materialHave[i].color = Color.red; // 텍스트 붉은색으로 변경
            }
            else // 재료가 충분할시
            {
                materialHave[i].color = Color.white; // 텍스트 흰색으로 변경
            }
        }
    }

    public void StartCraft() //제작 시작
    {
        StartCoroutine(Craft());
    }

    IEnumerator Craft() // 제작 코루틴 함수
    {
        if (!isCrafting) // 코루틴 제어변수
        {
            isCrafting = true; // 코루틴 비활성화
            ItemInfo sel = items[select]; // 선택중인 아이템 저장         

            GameObject window; // 활성화할 창

            if (isCraftable) // 제작이 가능할시
            {
                sel.GetComponent<IOnItemUse>().Use(); // 제작 아이템 사용
                Equipment.instance.Add(sel); // 제작 아이템 인벤토리에 추가
                sel.isCrafted = true; // 제작아이템 제작됨 True               

                // 재료 아이템 사용
                for (int i = 0; i < sel.recipe.matList.Length; i++)
                {
                    Material.instance.Use(sel.recipe.EA[i], sel.recipe.matList[i]);
                }

                window = craftSuccess; // 제작 성공창
            }
            else
            {               
                window = craftFail; // 제작 실패창
            }
            SetPage(); // 페이지 새로고침
            window.SetActive(true); // 창 활성화
            yield return new WaitForSecondsRealtime(1f); // 1초 대기
            window.SetActive(false); // 창 비활성화
            isCrafting = false; // 코루틴 활성화
        }
    }

    public void Close() // 제작창 닫기
    {
        if (!isCrafting) // 제작중엔 닫기 불가
        {
            gameObject.SetActive(false); // 오브젝트 비활성화
        }
    }
}
