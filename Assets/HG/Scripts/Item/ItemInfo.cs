using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemInfo : MonoBehaviour
{
    public int price;   //아이템 가격
    public string description;  //아이템 능력 설명
    public new string name; //아이템 이름
    public Sprite image;    //아이템 이미지
    public bool isSold = false; //팔렸는지 확인
    protected Player player;    // 플레이어
    public bool isLimit;    // 구매제한아이템(전설아이템)
    public bool isResell;   // 다시 구매 가능한 아이템
    public string enhanceDescription;   //강화 내용
    public int enhancePrice;    //강화 비용
    public bool isEnhanced = false; //강화상태 확인
    public float reSellPrice; //되팔기 가격
    public bool isResold = false;   //되팔았는지 확인 (한번 확인후 상점으로 되돌림)
    public bool bluePrint = false;  // 
    public bool isMaterial = false; //재료 아이템인지 확인

    public bool isCrafted = false;  //제작아이템일시 제작했는지 확인
    public Recipe recipe;   //제작법
    public bool reUseNeeded;
    public bool isOwn = false;

    public void PriceSet()  // 판매비용 설정 함수
    {
        reSellPrice = Mathf.Round(price * 0.3f);
    }

    public enum Rank    //아이템 등급 Enum
    {
        Rare =0,
        Unique,
        Legend
    };
    public Rank rank;

}
