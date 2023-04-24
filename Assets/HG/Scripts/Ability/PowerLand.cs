using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PowerLand : MonoBehaviour
{
    Player player;
    bool isCharge = false;
    GameObject effect1;
    GameObject effect2;
    GameObject firstEffect;
    GameObject secondEffect;
    public static float scale = 1;
   
    int PlayerLayer;
    int FootholdLayer;
    private void Start()
    {
        player = GetComponent<Player>();
        effect1 = EffectManager.instance.landEffect1;
        effect2 = EffectManager.instance.landEffect2;
        PlayerLayer = LayerMask.NameToLayer("Player");
        FootholdLayer = LayerMask.NameToLayer("FootHold");
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.DownArrow)&&Input.GetKeyDown(KeyCode.Z) && !player.isGround&&(player.body.velocity.y>1f||player.body.velocity.y<-1f))
            StartCoroutine(Land());
    }
    private void FixedUpdate()
    {
        if (isCharge) player.body.velocity = Vector2.up;

    }
    IEnumerator Land()
    {
        if (!isCharge)
        {            
            if (!player.isLand)
            {
                player.isCharge = true;
                player.isLand = true;
                isCharge = true;
                yield return new WaitForSeconds(0.3f);
                isCharge = false;
                firstEffect = Instantiate(effect1,transform.position, Quaternion.identity);
                firstEffect.transform.parent = transform;
                player.body.AddForce(Vector3.down * 500, ForceMode2D.Impulse);
                yield return new WaitForSeconds(0.5f);
                player.isCharge = false;
                Physics2D.IgnoreLayerCollision(PlayerLayer, FootholdLayer, false);  //0.5초후에 다시 충돌 활성

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.isLand)
        {
            Destroy(firstEffect);
            player.isLand = false;
            secondEffect = Instantiate(effect2, collision.contacts[0].point, Quaternion.identity);
            secondEffect.transform.localScale *= scale;
            player.body.velocity = Vector2.zero;
            Destroy(secondEffect,0.25f);

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (player.isLand && collision.gameObject.tag == "Ground")
        {
            player.isLand = false;
        }
    }


}
