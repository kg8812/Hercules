using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : MonoBehaviour
{
    bool isPoison = false;
    GameObject posionEffect;
    AnimationClip clip;
    float time = 5;
    private void Start()
    {
        posionEffect = EffectManager.instance.cureseEffect;
        clip = EffectManager.instance.curseAni;
    }
    private void Update()
    {
        if(!isPoison)
        {
            StartCoroutine(poison());
        }             
    }
    void DestroyCurse()
    {
        GetComponent<Enemy>().isDebuff = false;
        Destroy(gameObject.GetComponent<Curse>());
    }
    IEnumerator poison()
    {
        isPoison = true;
        GetComponent<Enemy>().hp -= Mathf.Ceil(GetComponent<Enemy>().maxHp / 100);
        GameObject effect = Instantiate(posionEffect, base.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(clip.length);
        isPoison = false;
        if(time > 0)
        {
            time--;
        }
        else
        {
            DestroyCurse();
        }
    }
    
}
