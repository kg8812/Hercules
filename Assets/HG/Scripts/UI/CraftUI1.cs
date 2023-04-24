using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftUI1 : MonoBehaviour
{
    public ItemInfo[] items; // ���� ������ ���

    int select; //���� ������ �ε���
    public Image itemImage; // ���� ������ �̹���
    public Text itemName; // ���� ������ �̸� �ؽ�Ʈ

    public Image[] materialImage; // ��� ������ �̹���
    public Text[] materialName; // ��� ������ �̸� �ؽ�Ʈ
    public Text[] materialHave; // ��� ������ ������ �ؽ�Ʈ 
    public Text[] materialNeed; // ��� ������ �ʿ�� �ؽ�Ʈ

    public GameObject material; // ���â ������Ʈ
    public GameObject alreadyCrafted; // �̹� ���۵� ������ â
    public GameObject craftSuccess; // ���� ����â
    public GameObject craftFail; // ���� ����â
    public GameObject noRecipe; // ���赵 ���� â
    bool isCraftable = true; // ���� ���� �Ǵܺ���
    bool isCrafting = false; // ���� �ڷ�ƾ �����

    private void OnEnable()
    {
        Time.timeScale = 0; //�ð�����
        select = 0; // �ε��� 0���� �ʱ�ȭ
        SetPage(); // ������ ���ΰ�ħ
    }

    private void OnDisable()
    {
        Time.timeScale = 1; // �ð����� ����
    }
    void Update()
    {
        // ����Ű�� ������ ����
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            select++;
            if (select >= items.Length)
            {
                select = 0;
            }

            SetPage();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            select--;
            if (select < 0)
            {
                select = items.Length - 1;
            }
            SetPage();
        }

        //ZŰ�� ����
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCraft();
        }
    }

    void SetPage() // ������ ���ΰ�ħ
    {
        isCraftable = true; // �ϴ� ���۰������� ����
        ItemInfo sel = items[select]; // �������� ������ 
        itemName.text = sel.name; // ���� ������ �̸� ������Ʈ
        itemImage.sprite = sel.image; // ���� ������ �̹��� ������Ʈ

        if (!sel.bluePrint) // ���赵�� ������
        {
            isCraftable = false; // ���ۺҰ�
            noRecipe.SetActive(true); // ���赵 ����â Ȱ��ȭ
            material.SetActive(false); // ��� ������â ��Ȱ��ȭ
            alreadyCrafted.SetActive(false); // �̹� ���۵�â ��Ȱ��ȭ
        }
        else if (sel.isCrafted) // �̹� ���۵� �������Ͻ�
        {
            isCraftable = false; // ���ۺҰ�

            alreadyCrafted.SetActive(true); // �̹� ���۵�â Ȱ��ȭ
            material.SetActive(false); // ��� ������â ��Ȱ��ȭ
            noRecipe.SetActive(false); // ���赵 ����â ��Ȱ��ȭ

        }
        else // ���۰���
        {
            material.SetActive(true); // ��� ������â Ȱ��ȭ
            noRecipe.SetActive(false); // ���赵 ����â ��Ȱ��ȭ
            alreadyCrafted.SetActive(false); // �̹� ���۵�â ��Ȱ��ȭ
        }

        // ��� ������ ���� ���ΰ�ħ
        for (int i = 0; i < sel.recipe.matList.Length; i++)
        {
            materialImage[i].sprite = sel.recipe.matList[i].image; //��� �̹���
            materialName[i].text = sel.recipe.matList[i].name; // ��� �̸�
            materialNeed[i].text = sel.recipe.EA[i].ToString(); // ��� �ʿ��
            materialHave[i].text = $"{Material.instance.GetItemCount(sel.recipe.matList[i])}/{sel.recipe.EA[i]}"; // ��� ������

            //��� �������� �ʿ������ ������
            if (sel.recipe.EA[i] > Material.instance.GetItemCount(sel.recipe.matList[i]))
            {
                isCraftable = false; // ���ۺҰ�
                materialHave[i].color = Color.red; // �ؽ�Ʈ ���������� ����
            }
            else // ��ᰡ ����ҽ�
            {
                materialHave[i].color = Color.white; // �ؽ�Ʈ ������� ����
            }
        }
    }

    public void StartCraft() //���� ����
    {
        StartCoroutine(Craft());
    }

    IEnumerator Craft() // ���� �ڷ�ƾ �Լ�
    {
        if (!isCrafting) // �ڷ�ƾ �����
        {
            isCrafting = true; // �ڷ�ƾ ��Ȱ��ȭ
            ItemInfo sel = items[select]; // �������� ������ ����         

            GameObject window; // Ȱ��ȭ�� â

            if (isCraftable) // ������ �����ҽ�
            {
                sel.GetComponent<IOnItemUse>().Use(); // ���� ������ ���
                Equipment.instance.Add(sel); // ���� ������ �κ��丮�� �߰�
                sel.isCrafted = true; // ���۾����� ���۵� True               

                // ��� ������ ���
                for (int i = 0; i < sel.recipe.matList.Length; i++)
                {
                    Material.instance.Use(sel.recipe.EA[i], sel.recipe.matList[i]);
                }

                window = craftSuccess; // ���� ����â
            }
            else
            {               
                window = craftFail; // ���� ����â
            }
            SetPage(); // ������ ���ΰ�ħ
            window.SetActive(true); // â Ȱ��ȭ
            yield return new WaitForSecondsRealtime(1f); // 1�� ���
            window.SetActive(false); // â ��Ȱ��ȭ
            isCrafting = false; // �ڷ�ƾ Ȱ��ȭ
        }
    }

    public void Close() // ����â �ݱ�
    {
        if (!isCrafting) // �����߿� �ݱ� �Ұ�
        {
            gameObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
        }
    }
}
