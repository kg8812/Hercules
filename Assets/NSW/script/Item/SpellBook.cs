using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour
{
    public int index;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        body.AddForce(Vector2.up * 1600);
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(index == 0)
            {
                int num = PlayerPrefs.GetInt("AtkSpellBook");
                num++;
                PlayerPrefs.SetInt("AtkSpellBook", num);
            }
            else if(index == 1)
            {
                int num = PlayerPrefs.GetInt("CdmgSpellBook");
                num++;
                PlayerPrefs.SetInt("CdmgSpellBook", num);
            }
            else
            {
                int num = PlayerPrefs.GetInt("CrateSpellBook");
                num++;
                PlayerPrefs.SetInt("CrateSpellBook", num);
            }
            Destroy(this.gameObject);
            

        }
    }
}
