using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "",menuName = "Scriptable/Recipe")]
public class Recipe : ScriptableObject
{
    public MaterialInfo[] matList; // 필요 재료 목록
    public int[] EA; // 필요 개수
   
}
