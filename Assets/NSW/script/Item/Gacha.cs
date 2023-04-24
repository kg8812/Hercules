using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gacha : MonoBehaviour
{
    public List<ItemInfo> Legendary; //전설아이템목록
    public List<ItemInfo> Rare; //레어아이템목록
    public List<ItemInfo> Normal; //노말아이템목록
    public Canvas gaChaCanvas;
    public Transform Result;
    public Transform ResultImage;
    public Text ResultText;
    public Text Token;
    public Text Cost;
    public Sprite[] Error;
    int token;
    bool isLotto = false;
    Player player;
    Image ResultSprite;
    // Start is called before the first frame update
    void Start()
    {
        Result.gameObject.SetActive(false);
        ResultSprite = ResultImage.GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        token = GameManager.Instance.token; // 토큰숫자 가져오기 
        if (token < 1000) // 토큰숫자가 부족하면 빨간색으로 표시
        {
            Cost.color = Color.red;
        }
        else
        {
            Cost.color = Color.white;
        }
        Token.text = token.ToString();
    }
    private void OnEnable()
    {
        token = GameManager.Instance.token;
        PlayerPrefs.SetInt("Token", token);
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Lotto()
    {

        if (token < 1000)
        {
            Result.gameObject.SetActive(true);
            ResultText.text = "토큰이 부족합니다.";
            return;
        }
        if (!isLotto) StartCoroutine(Lottoem());

    }
    public void EndGaCha()
    {
        gaChaCanvas.gameObject.SetActive(false);
    }
    IEnumerator Lottoem()
    {
        isLotto = true;
        GameManager.Instance.token -= 1000;
        token = GameManager.Instance.token;
        PlayerPrefs.SetInt("Token", token);
        Token.text = token.ToString();
        yield return new WaitForSecondsRealtime(1.0f);
        float r = Random.Range(1, 100);
        if (r >= 99) // r이 99이상이면 전설급 아이템 획득 
        {
            int ran = Random.Range(0, Legendary.Count);
            if (Legendary[ran].isSold) // 중복처리
            {
                Result.gameObject.SetActive(true);
                ResultText.text = "전설적인 꽝";
                ResultText.color = Color.red;
                ResultSprite.sprite = Error[0];
            }
            else
            {
                Equipment.instance.Add(Legendary[ran]);
                Legendary[ran].isSold = true;
                Legendary[ran].GetComponent<IOnItemUse>().Use();
                Result.gameObject.SetActive(true);
                ResultSprite.sprite = Legendary[ran].image;
                ResultText.color = Color.red;
                ResultText.text = Legendary[ran].name;
            }

        }
        else if (r >= 75)  // r이 75이상이면 레어아이템 획득 
        {
            int ran = Random.Range(0, Rare.Count);
            if (Rare[ran].isSold)
            {
                Result.gameObject.SetActive(true);
                ResultText.text = "희귀한 꽝";
                ResultText.color = new Color32(208, 57, 250, 255);
                ResultSprite.sprite = Error[1];
            }
            else
            {
                Equipment.instance.Add(Rare[ran]);
                Rare[ran].isSold = true;
                Rare[ran].GetComponent<IOnItemUse>().Use();
                Result.gameObject.SetActive(true);
                ResultSprite.sprite = Rare[ran].image;
                ResultText.color = new Color32(208, 57, 250, 255);
                ResultText.text = Rare[ran].name;
            }
        }
        else // 그외는 노말아이템 획득 
        {
            int ran = Random.Range(0, Normal.Count);



            Normal[ran].GetComponent<IOnItemUse>().Use();
            Result.gameObject.SetActive(true);
            ResultSprite.sprite = Normal[ran].image;
            ResultText.color = Color.yellow;
            ResultText.text = Normal[ran].name;



        }
       
        isLotto = false;
    }
}
