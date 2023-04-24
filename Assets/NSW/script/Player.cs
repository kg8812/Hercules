using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour, IOnDamage
{
    public float speed;
    public GameObject swords;
    public GameObject bow;
    public GameObject club;
    public Rigidbody2D body;
    public bool isGround;
    public bool isFootHold;
    public bool isLeft = false;
    public bool isDash = false;
    bool toDash = false;
    public int jumpMax = 1;
    int jumpCount = 0;
    public int dashMax = 1;
    int dashCount = 0;
    int PlayerLayer;    //플레이어 레이어 저장변수
    int FootholdLayer; // 발판 레이어 저장변수
    public float maxHp; //최대체력
    public float currHp;    //현재체력
    public float atk;   //공격력
    public float atkRatio;  //공격력 계수
    public float dmg { get; private set; }
    public float def;   //방어력
    public float jumpPower = 40;    //점프력
    public float gold;  //돈
    public float dashCd; //대시쿨타임
    public int weapon;  //무기종류
    float dashTime = 0.15f;
    public float dashPower; //대시 파워(거리)
    public float critProb;   //치명타확률
    public float critDmg;    //치명타데미지배율
    public float goldGain;
    public float skillDmg;
    public float atkDebuff;
    public float weapon_Durability;
    public float atkSpeed;

    public Animator ani;

    public GameObject dashEffect;
    public GameObject jumpEffect;
    public GameObject attackEffect;
    public GameObject curWeapon;
    public GameObject arrow;

    bool isBelowJump = false;

    bool isInvincible = false;

    public bool isCharge = false;
    public bool isSkill = false;
    public bool isLand = false;
    float rayLength = 2f;
    bool isAttack = false;
    public Vector2 sideBoxSize;

    bool isFall = false;

    public Canvas gameOver;
    GameObject dmgText;
    GameObject canvas;
    public AnimationClip atkClip;
    bool inAttack = false;
    void Start()
    {
        dmgText = EffectManager.instance.dmgText;
        canvas = GameObject.Find("Canvas");
        ani = GetComponent<Animator>();
        maxHp = PlayerPrefs.GetFloat("PlayerMaxHp");
        currHp = PlayerPrefs.GetFloat("PlayerCurrHp");
        atk = PlayerPrefs.GetFloat("PlayerAtk");
        atkSpeed = PlayerPrefs.GetFloat("PlayerAtkSpeed");
        atkRatio = PlayerPrefs.GetFloat("PlayerAtkRatio");
        atkDebuff = PlayerPrefs.GetFloat("PlayerAtkDebuff");
        def = PlayerPrefs.GetFloat("PlayerDef");
        PlayerLayer = LayerMask.NameToLayer("Player");
        FootholdLayer = LayerMask.NameToLayer("FootHold");
        speed = PlayerPrefs.GetFloat("PlayerSpeed");
        jumpMax = PlayerPrefs.GetInt("JumpMax");
        dashMax = PlayerPrefs.GetInt("DashMax");
        dashPower = PlayerPrefs.GetFloat("PlayerDashPower");
        body = GetComponent<Rigidbody2D>();
        gold = PlayerPrefs.GetFloat("Money");
        weapon = PlayerPrefs.GetInt("PlayerWeapon");
        dashCd = PlayerPrefs.GetFloat("PlayerDashCd");
        critProb = PlayerPrefs.GetFloat("PlayerCritProb");
        critDmg = PlayerPrefs.GetFloat("PlayerCritDmg");
        goldGain = PlayerPrefs.GetFloat("PlayerGoldGain");
        skillDmg = PlayerPrefs.GetFloat("PlayerSkillDmg");
        weapon_Durability = PlayerPrefs.GetFloat("Weapon_Durability");
        WeaponSelect();
        Equipment.instance.UseItems();
        GodSelection.Select(PlayerPrefs.GetInt("God"), this.gameObject);
        
    }

    private void OnEnable()
    {
        isAttack = false;
        inAttack = false;
        toDash = false;
        isDash = false;
    }
    // Update is called once per frame
    void Update()
    {
        float keyW = Input.GetAxisRaw("Horizontal");

        dmg = atk * atkRatio * (1 - atkDebuff);

        if (critProb > 1)
        {
            critDmg = PlayerPrefs.GetFloat("PlayerCritDmg") + critProb - 1;
        }

        if (isLand) return;

        if (body.velocity.y < -5 && !isFall)
        {
            isFall = true;
            ani.SetBool("isJump", false);
            if (!isBelowJump)
                ani.SetBool("isLand", true);
        }

        if (Input.GetKey(KeyCode.DownArrow) && isGround && keyW == 0)
        {
            ani.SetBool("isSit", true);
        }
        else
        {
            ani.SetBool("isSit", false);
        }

        if (isBelowJump)
            ani.SetBool("isBelowJump", true);
        else
            ani.SetBool("isBelowJump", false);

        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(Dash());
        if (currHp > maxHp)
        {
            currHp = maxHp;
        }

        if (body.velocity.y < 1f && !isCharge)
        {
            Debug.DrawRay(body.position, Vector3.down, new Color(1, 0, 0)); //ray 생성
            RaycastHit2D rayHit = Physics2D.Raycast(body.position, Vector3.down, 1, LayerMask.GetMask("map", "FootHold"));
            if (rayHit.collider != null)    //ray에 무언가 닿았을시
            {
                if (rayHit.distance < 0.3f) // 플레이어와의 거리가 0.3 이하면
                {
                    ani.SetBool("isJump", false);
                    isFall = false;
                    isGround = true;
                    jumpCount = 0;
                    if (rayHit.collider.tag == "FootHold" || rayHit.collider.tag == "Elevator")
                        isFootHold = true;
                }
            }
            else     
            {
                if (isGround)
                    isGround = false;

                isFootHold = false;

            }
        }
        else
        {
            if (isGround)
                isGround = false;

            isFootHold = false;

        }
        if (!isDash && !inAttack)
        {
            if (keyW > 0)
            {              
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isLeft = false;
            }
            else if (keyW < 0)
            {               
                transform.rotation = Quaternion.Euler(0, 0, 0);

                isLeft = true;
            }
        }

        if (!Input.GetKey(KeyCode.DownArrow) && Time.timeScale > 0)   //아래쪽 방향키 누르지 않고있을때만 점프가능
            Jump();
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKeyDown(KeyCode.Z))   //아래키누르고 점프누르면 아래점프
        {
            StartCoroutine(BelowJump());
        }

        if (body.velocity.y < -30)
            body.velocity = new Vector2(body.velocity.x, -30);

        if (Input.GetKey(KeyCode.X))
        {
            if (!isDash && !ani.GetBool("isSit"))
                StartCoroutine(Attack());
        }

    }
    private void FixedUpdate()
    {
        if (isLand && !isGround) return;

        float move = speed;
        float keyW = Input.GetAxisRaw("Horizontal");

        if (curWeapon != null)
        {
            if (keyW != 0 && !inAttack) curWeapon.SetActive(false);
            else curWeapon.SetActive(true);
        }
        ani.SetFloat("Run", keyW);
        if (!isDash)
            body.velocity = new Vector2(move * keyW, body.velocity.y);

        WallStop();
    }

    void WallStop() //벽에 끼임 방지 raycast에 닿으면 멈춤
    {
        RaycastHit2D rayHit;
        if (isLeft)
        {

            rayHit = Physics2D.BoxCast(body.position + Vector2.up * 2, sideBoxSize, 0, Vector3.left, rayLength, LayerMask.GetMask("Wall"));
        }
        else
        {

            rayHit = Physics2D.BoxCast(body.position + Vector2.up * 2, sideBoxSize, 0, Vector3.right, rayLength, LayerMask.GetMask("Wall"));
        }

        if (rayHit.collider != null && rayHit.collider.tag == "Wall")
        {
            if (isDash)
                Stop();

        }


    }
    void Stop()
    {
        body.velocity = new Vector2(0, body.velocity.y);
    }

    private void OnDrawGizmos()
    {
        RaycastHit2D rayHit;
        Vector3 dir;
        if (isLeft)
        {
            rayHit = Physics2D.BoxCast(transform.position + Vector3.up * 2, sideBoxSize, 0, Vector3.left, rayLength, LayerMask.GetMask("Wall"));
            dir = Vector3.left;

        }
        else
        {
            rayHit = Physics2D.BoxCast(transform.position + Vector3.up * 2, sideBoxSize, 0, Vector3.right, rayLength, LayerMask.GetMask("Wall"));
            dir = Vector3.right;
        }

        Gizmos.color = Color.red;
        if (rayHit.collider != null)
        {
            Gizmos.DrawRay(transform.position + Vector3.up * 2, dir * rayHit.distance);
            Gizmos.DrawWireCube(transform.position + Vector3.up * 2 + dir * rayHit.distance, sideBoxSize);
        }
        else
        {
            Gizmos.DrawRay(transform.position + Vector3.up * 2, dir * rayLength);
            Gizmos.DrawWireCube(transform.position + Vector3.up * 2 + dir * rayLength, sideBoxSize);
        }
    }
    public void WeaponSelect()
    {
        if (weapon == 1)
        {
            swords.SetActive(true);
            bow.SetActive(false);
            club.SetActive(false);
            curWeapon = swords;
        }
        else if (weapon == 2)
        {
            swords.SetActive(false);
            bow.SetActive(true);
            club.SetActive(false);
            curWeapon = bow;
        }
        else if (weapon == 3)
        {
            swords.SetActive(false);
            bow.SetActive(false);
            club.SetActive(true);
            curWeapon = club;
        }
        else
        {
            swords.SetActive(false);
            bow.SetActive(false);
            club.SetActive(false);
            curWeapon = null;
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Z) && jumpCount < jumpMax && !isBelowJump)
        {
            isFall = false;
            ani.SetBool("isLand", false);
            ani.SetBool("isJump", true);
            if (jumpCount == 0)
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                GameObject j = Instantiate(jumpEffect, transform.position, transform.rotation);
                Destroy(j, 0.3f);
                body.velocity = new Vector2(body.velocity.x, jumpPower * 0.8f);
            }

            jumpCount++;

        }
        if (Input.GetKeyUp(KeyCode.Z) && body.velocity.y > jumpPower * 0.8)  //점프키를 도중에 뗄시 점프력 감소
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * 0.3f);
        }

    }

    IEnumerator Attack()
    {
        if (!isAttack)
        {
            ani.SetFloat("AtkSpeed", atkSpeed);
            inAttack = true;    //공격중 확인변수
            isAttack = true;    //코루틴 제어변수
            Vector3 dir;
            Quaternion rot;
            bool isBow;
            if (weapon == 2)
                isBow = true;
            else
            {
                isBow = false;
            }

            if (isBow)
            {
                ani.SetBool("isBowAttack", true);
            }
            else
            {
                ani.SetBool("isAttack", true);
            }
            if (isLeft)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

                dir = Vector3.left;
                rot = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                dir = Vector3.right;
                rot = Quaternion.identity;
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }
            
            if (isBow)
            {
                Instantiate(arrow, transform.position + dir * 4 + Vector3.up * 3, rot);

            }
            else
            {
                Instantiate(attackEffect, transform.position + dir * 4 + Vector3.up * 3, rot);
            }          
            yield return new WaitForSeconds(atkClip.length);
            if (isLeft)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }
            inAttack = false;
            ani.SetBool("isAttack", false);
            ani.SetBool("isBowAttack", false);

            yield return new WaitForSeconds(0.4f/atkSpeed - atkClip.length);
            isAttack = false;
        }
    }
    IEnumerator BelowJump() //아래점프 (발판에서만 가능)
    {
        if (isFootHold) //발판을 밟고있을시
        {
            Physics2D.IgnoreLayerCollision(PlayerLayer, FootholdLayer, true);  //플레이어와 발판 충돌 무시
            isBelowJump = true;
            yield return new WaitForSeconds(0.5f);

            if (!isCharge)
                Physics2D.IgnoreLayerCollision(PlayerLayer, FootholdLayer, false);  //0.5초후에 다시 충돌 활성

        }
    }


    IEnumerator Dash()
    {
        if (!toDash && !isDash)
        {
            if (PlayerPrefs.GetInt("Hermes") == 1)
            {
                isInvincible = true;
            }
            toDash = true;
            isDash = true;
            ani.SetBool("isDash", true);
            if (isLeft)
            {
                body.AddForce(Vector2.left * dashPower, ForceMode2D.Impulse);
                GameObject d = Instantiate(dashEffect, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                Destroy(d, 0.2f);
            }
            else
            {
                body.AddForce(Vector2.right * dashPower, ForceMode2D.Impulse);
                GameObject d = Instantiate(dashEffect, transform.position, Quaternion.Euler(Vector3.zero));
                Destroy(d, 0.2f);
            }
            dashCount++;

            yield return new WaitForSeconds(dashTime);
            body.velocity = new Vector2(0, body.velocity.y);
            yield return new WaitForSeconds(0.05f);
            isDash = false;
            ani.SetBool("isDash", false);
            if (PlayerPrefs.GetInt("Hermes") == 1)
            {
                isInvincible = false;
            }
            if (dashCount >= dashMax)
            {
                yield return new WaitForSeconds(dashCd);
                dashCount = 0;
            }
            toDash = false;
        }
    }
    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        float y = collision.contacts[0].normal.y;

        if (y > 0.7f)
        {
            isBelowJump = false;
            ani.SetBool("isLand", false);

        }
        if (collision.gameObject.tag == "Wall" && y < 0.7f && y > -0.7f)
            body.velocity = new Vector2(0, body.velocity.y);


    }
    private void OnCollisionStay2D(UnityEngine.Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isBelowJump = false;
            ani.SetBool("isLand", false);
        }

    }
    void IOnDamage.OnHit(float damage)
    {
        if (currHp > 0)
        {
            if (GetComponent<Shield>() != null)
            {
                if (Shield.afterHit >= Shield.cd)
                {
                    GameObject e = Instantiate(EffectManager.instance.shieldEffect, transform.position - Vector3.up, Quaternion.identity);
                    e.transform.parent = gameObject.transform;
                    Destroy(e, 1);
                    damage = 0;
                    Shield.afterHit = 0;
                }
            }
            damage = damage / (1 + def / 100.0f);
            if (PlayerPrefs.GetInt("Ajax") == 1)
                damage = Mathf.Ceil(damage * 0.7f);
            else if (PlayerPrefs.GetInt("Ajax") == 2)
                damage = Mathf.Ceil(damage * 0.5f);
            if (isInvincible) damage = 0;
            if (PlayerPrefs.GetInt("Achilleus") == 1 && damage <= 5)
                damage = 1;
            else if (PlayerPrefs.GetInt("Achilleus") == 2 && damage <= 7)
                damage = 1;

            if (PlayerPrefs.GetInt("WingBoots") != 0 && !isGround)
                damage /= 2;


            currHp -= damage;
        }
        else
        {
            if (PlayerPrefs.GetInt("Hades") == 1)
            {
                currHp = 500;
                PlayerPrefs.SetFloat("PlayerCurrHp", 500);
                PlayerPrefs.SetInt("Hades", 0);
            }
            else
            {
                GameManager.Reset();
                this.gameObject.SetActive(false);
                gameOver.gameObject.SetActive(true);
            }
        }
        GameObject text = Instantiate(dmgText, canvas.transform);
        text.GetComponent<DmgText>().Set(damage, transform.position + Vector3.up * 3);
        text.GetComponent<DmgText>().dmgText.color = Color.red;

    }



}
