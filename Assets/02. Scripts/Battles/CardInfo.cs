// ���ö
using UnityEngine;
using System;

public class CardInfo : MonoBehaviour
{
    #region �̱���
    public static CardInfo Instance { get; set; }
    private static CardInfo instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        // ī�� ȿ���� ��������Ʈ�� ��� ���
        EnrollAllEffects();
        EnrollTargetDict();
    }
    #endregion �̱���

    #region ī�� UI ������
    // �����ϰ� �ʹ�...
    public Sprite[] skillIcons;
    public Sprite[] debuffIcons;
    #endregion ī�� UI ������

    #region ���� �˻�
    // ī�� Ÿ���� ������ ���̾ ����ִ� �迭
    private LayerMask[] layerDict;

    // �迭�� Ÿ�� - ���̾� ������ ����Ѵ�.
    private void EnrollTargetDict()
    {
        layerDict = new LayerMask[Enum.GetValues(typeof(EffectType)).Length];

        layerDict[(int)EffectType.Attack] = LayerMask.GetMask("Enemy");
        layerDict[(int)EffectType.Shield] = LayerMask.GetMask("Field");
        layerDict[(int)EffectType.Heal] = LayerMask.GetMask("Field");
        layerDict[(int)EffectType.Cleanse] = LayerMask.GetMask("Field");
        layerDict[(int)EffectType.RestoreCost] = LayerMask.GetMask("Field");
        layerDict[(int)EffectType.Draw] = LayerMask.GetMask("Field");
        layerDict[(int)EffectType.Buff] = LayerMask.GetMask("Field");
        layerDict[(int)EffectType.Debuff] = LayerMask.GetMask("Enemy");
        layerDict[(int)EffectType.Bleed] = LayerMask.GetMask("Enemy");
    }

    // Ÿ�Կ� �´� ���̾ ��ȯ�Ѵ�.
    public LayerMask ReturnLayer(EffectType type)
    {
        return layerDict[(int)type];
    }
    #endregion ���� �˻�

    #region ī�� ȿ��
    // ī�� ȿ�� �Լ����� ��Ƶ� ��������Ʈ
    // amount�� ī���� ȿ����, turnCount�� ���ӵ� ���� ��, target�� ���� ����̴�.
    // ���� ��� Bleed�� ������ 6, 3, Player.Instance���, �÷��̾�� 3�ϰ�, �� �� 6�� �������� �ش�.
    public delegate void CardEffects(int amount, int turnCount, Character target);
    // ��������Ʈ �迭, EffectType�� �´� �Լ��� ��Ī�Ѵ�.
    public CardEffects[] effects;

    // ��� ����
    // CardInfo.Instance.effects[(int)ȿ�� ����](ȿ����, ���);
    // CardInfo.Instance.effects[(int)EffectType.Buff](5, target);

    // ��� ȿ���� effects �迭�� ����Ѵ�.
    void EnrollAllEffects()
    {
        effects = new CardEffects[Enum.GetValues(typeof(EffectType)).Length];

        // ī�� ȿ���� �迭�� ���
        effects[(int)EffectType.Attack] += Attack;
        effects[(int)EffectType.Shield] += Shield;
        effects[(int)EffectType.Heal] += Heal;
        effects[(int)EffectType.Cleanse] += Cleanse;
        effects[(int)EffectType.RestoreCost] += RestoreCost;
        effects[(int)EffectType.Draw] += Draw;
        effects[(int)EffectType.Buff] += Buff;
        effects[(int)EffectType.Debuff] += Debuff;
        effects[(int)EffectType.Bleed] += Bleed;
    }

    // target�� null�� ���� Card�� OnEndDrag���� �˻������Ƿ�, �˻����� �ʴ´�.
    public void Attack(int amount, int turnCount, Character target)
    {
        target.DecreaseHP(amount);
    }

    public void Shield(int amount, int turnCount, Character target)
    {
        Debug.Log("Shield");
    }

    public void Heal(int amount, int turnCount, Character target)
    {
        Debug.Log("Heal");
    }

    public void Cleanse(int amount, int turnCount, Character target)
    {
        Debug.Log("Cleanse");
    }

    public void RestoreCost(int amount, int turnCount, Character target)
    {
        Debug.Log("RestoreCost");
    }

    public void Draw(int amount, int turnCount, Character target)
    {
        Debug.Log("Draw");
    }

    public void Buff(int amount, int turnCount, Character target)
    {
        Debug.Log("Buff");
    }

    public void Debuff(int amount, int turnCount, Character target)
    {
        Debug.Log("Debuff");
    }

    public void Bleed(int amount, int turnCount, Character target)
    {
        target.EnrollBleed(new BleedEffect(amount, turnCount));
    }
    #endregion ī�� ȿ��
}
