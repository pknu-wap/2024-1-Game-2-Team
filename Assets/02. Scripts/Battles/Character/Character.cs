using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("������")]
    // HP(ü��)
    protected int currentHp = 100;
    protected int maxHp = 100;

    [Header("������Ʈ")]
    [SerializeField]
    protected Image hpBar;
    [SerializeField]
    protected TMP_Text hpText;

    public void Awake()
    {
        hpBar = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        hpText = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>();

        UpdateCurrentHP();
    }

    // �� ������Ʈ�� hp�� ��ȯ�Ѵ�.
    public int GetHP()
    {
        return currentHp;
    }

    public void UpdateCurrentHP()
    {
        hpBar.fillAmount = (float)currentHp / maxHp;
        hpText.text = currentHp + "/" + maxHp;
    }

    // �� ������Ʈ�� hp�� ���ҽ�Ų��.
    public void DecreaseHP(int damage)
    {
        // hp�� damage��ŭ ���ҽ�Ų��.
        currentHp -= damage;

        UpdateCurrentHP();

        // hp�� 0 ���ϰ� �� ���
        if (currentHp <= 0)
        {
            // ���� �̺�Ʈ ����
            Die();
        }
    }

    public void Die()
    {
        // ������ ���õ� ȿ�� ó��
        // ���� �ִϸ��̼�
        // ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
        // Battle Info�� ���� �� -1
    }
}
