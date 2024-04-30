// ���ö
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BleedEffect
{
    public BleedEffect(int damagePerTurn, int remainingTurns)
    {
        this.damagePerTurn = damagePerTurn;
        this.remainingTurns = remainingTurns;
    }

    public int damagePerTurn;
    public int remainingTurns;
}

public class DebuffIconComponent
{
    public DebuffIconComponent(Image image, TMP_Text tmp_Text)
    {
        this.image = image;
        this.tmp_Text = tmp_Text;
    }

    public Image image;
    public TMP_Text tmp_Text;
}

public class Character : MonoBehaviour
{
    [Header("������")]
    // HP(ü��)
    [SerializeField] protected int currentHp = 100;
    [SerializeField] protected int maxHp = 100;

    // ����׿�, ���� ����
    [Header("������Ʈ")]
    // HP ��
    [SerializeField] protected Image hpBar;
    [SerializeField] protected TMP_Text hpText;
    //[SerializeField] protected BuffIconSpawner buffer;
    // ���� ������ ������ ���� ���� -> ������Ʈ Ǯ������ ��ü

    [Header("�����̻�")]
    [SerializeField] public List<BleedEffect> debuffs;
    [SerializeField] public List<DebuffIconComponent> debuffIcons;

    [Header("�̺�Ʈ")]
    [SerializeField] public UnityEvent onTurnStarted;

    public virtual void Awake()
    {
        // HP ��
        hpBar = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        hpText = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<TMP_Text>();

        // ����� ȿ����(���� ������)�� ��Ƶ� ����Ʈ
        debuffs = new List<BleedEffect>();
        debuffIcons = new List<DebuffIconComponent>();

        // ����� �����ܵ��� �θ� �����̳�
        debuffIconContainer = transform.GetChild(1).GetChild(0).GetChild(1);
        // debuffIconContainer�� ��� �ڽ� ������Ʈ�� ��Ȱ��ȭ. (�ڽ��� �ڽ��� X)
        foreach (Transform icon in debuffIconContainer)
        {
            // debuffIcons�� icon�� �̹���, �ؽ�Ʈ ������Ʈ�� �Ҵ�
            debuffIcons.Add(new DebuffIconComponent(icon.GetComponent<Image>(), icon.GetChild(0).GetComponent<TMP_Text>()));
            // �׸��� ��Ȱ��ȭ.
            icon.gameObject.SetActive(false);
        }

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

    public virtual void Die()
    {
        // ������ ���õ� ȿ�� ó��
        // ���� �ִϸ��̼�
    }

    // ���� ȿ���� �� ���� �̺�Ʈ�� ����Ѵ�.
    public void EnrollBleed(BleedEffect bleedEffect)
    {
        // ���� ����Ʈ�� �߰�
        debuffs.Add(bleedEffect);

        // ���� ����� UI �߰�
        int i = debuffs.Count - 1;
        Transform icon = debuffIconContainer.GetChild(i);
        /*
         * �ε��� ���� �ʿ�
         */
        debuffIcons[i].image.sprite = CardInfo.Instance.debuffIcons[0];
        debuffIcons[i].tmp_Text.text = bleedEffect.remainingTurns.ToString();

        icon.gameObject.SetActive(true);
    }

    [Header("���� ������")]
    public Transform debuffIconContainer;

    public void UpdateDebuffIcon()
    {
        // ��� ���� ������� ����(i��° ������� ����)
        int i = 0;
        for (; i < debuffs.Count; ++i)
        {
            // i��° �̹����� �ؽ�Ʈ�� �����ϰ�
            debuffIcons[i].image.sprite = CardInfo.Instance.debuffIcons[0];
            debuffIcons[i].tmp_Text.text = debuffs[i].remainingTurns.ToString();
            
            // ������Ʈ Ȱ��ȭ
            // �� ������ �����丵�� �ʿ��غ��δ�.
            debuffIconContainer.GetChild(i).gameObject.SetActive(true);
        }

        // ������� ���� �����ܵ���
        for(;i < debuffIconContainer.childCount; ++i)
        {
            // ��Ȱ��ȭ�Ѵ�.
            debuffIconContainer.GetChild(i).gameObject.SetActive(false);
        }
    }

    // ���� ȿ���� �߻���Ų��.
    public void GetBleedAll()
    {
        // �ް� �� ��ü ������
        int totalDamage = 0;

        // ��� ���� ���� ���� ȿ���� ����
        for(int i = 0; i < debuffs.Count; ++i)
        {
            totalDamage += debuffs[i].damagePerTurn;

            // ���� �� 1 ����
            --debuffs[i].remainingTurns;
            // ���� ���� 0 ���϶��
            if (debuffs[i].remainingTurns <= 0)
            {
                // �ش� ���� ȿ���� �����Ѵ�.
                debuffs.RemoveAt(i);
                // ���� ��������� 1ĭ�� ������ ����������, �ε����� 1 ������ ����
                --i;
            }
        }

        // ���� ����Ʈ ���

        // ü���� ���ҽ�Ų��.
        DecreaseHP(totalDamage);
        Debug.Log(totalDamage);

        // �������� ������Ʈ �Ѵ�.
        UpdateDebuffIcon();
    }
}
