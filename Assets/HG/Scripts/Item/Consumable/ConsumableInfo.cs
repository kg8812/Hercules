using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableInfo:MonoBehaviour
{
    public new string name; // 아이템 이름
    public string description;  // 아이템 설명
    public Sprite image;    // 아이템 이미지
    public int count;   // 아이템 개수
    public bool isCd = false;   // 아이템 쿨타임 확인용 변수
    public bool isBuff; // 버프 아이템인지 확인
}
