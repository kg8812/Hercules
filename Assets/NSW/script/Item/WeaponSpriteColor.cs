using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteColor : MonoBehaviour
{
    public SpriteRenderer image;
    int evolution;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        evolution = PlayerPrefs.GetInt("WeaponEvolution") ;
        if (evolution == 0)

            image.color = Color.white;
        else if (evolution == 1)
            image.color = Color.yellow;

        else if (evolution == 2)

            image.color = new Color32(208, 57, 250, 255);

        else

            image.color = Color.red;
    }
}
