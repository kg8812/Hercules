using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : ItemInfo,IOnItemUse
{
    public void Use()
    {
        player.speed *= 1.3f;
    }
    
}
