using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState // 적 스테이트 설정
{
    idle,
    move,
    attack,
    hit,
    rotate,
    dead
};
public class Enemy : MonoBehaviour
{
    public GameObject bar;
    public float hp;
    public float maxHp;
    public new string name;
    public bool isBoss;
    public float atk;
    public float def;
    public float speed;
    public float height;
    public float gold;
    public bool isDebuff = false;

    GameObject chest;
    public ItemObtain dropItem;
   
    public bool isStop = false;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IOnDamage dmg = collision.GetComponent<IOnDamage>(); 

        if (dmg != null)
        {
            if (PlayerPrefs.GetInt("Aegis") == 1) // 플레이어가 이지스의 방패를 가지고있으면 반사데미지를 받음 
            {
                this.hp -= 5;
            }
            else if (PlayerPrefs.GetInt("Aegis") == 2)
            {
                this.hp -= 10;
            }
        }
    }
    private void OnDestroy()
    {
        if (hp > 0) return; // 스테이지 이동시 파괴되었을시에 보상 획득 방지
        if (isBoss) // 보스면 재료아이템을 드랍
        {
            float rand = Random.Range(0, 1f);

            if (rand < 0.5f)
            {             
                Instantiate(dropItem, transform.position+Vector3.up*3, Quaternion.identity);
            }
        }
        else // 보스가 아닐경우 아이템 상자를 드랍
        {
            chest = EffectManager.instance.chest;
            float r = Random.Range(0.1f, 1.0f);
            if (r > 0.3f)
                Instantiate(chest, this.transform.position, this.transform.rotation);
        }
        Player player = GameObject.Find("Player").transform.Find("Heracles").gameObject.GetComponent<Player>();
      
       
        player.gold += this.gold * player.goldGain; // 플레이어가 골드 획득
        PlayerPrefs.SetFloat("Money", player.gold);
        if (PlayerPrefs.GetInt("God") == 2 && PlayerPrefs.GetInt("PlayerWeapon") == 3) // 하데스 곤봉일경우 플레이어가 체력 회복
        {
            player.currHp += Mathf.Round(player.maxHp / 100);
        }       
    }

    public IEnumerator Stop(float time)
    {
        isStop = true;
        float sp = speed;
        speed = 0;
        yield return new WaitForSeconds(time);
        speed = sp;
        isStop = false;
    }
}
