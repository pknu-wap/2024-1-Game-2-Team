// ���ö
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    #region �̱���
    public static CardInfo instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        // ī�� ȿ���� ��������Ʈ�� ��� ���
        EnrollAllEffect();

        // ��� ����
        // CardInfo.Instance.effects[(int)ȿ�� ����](ȿ����, ���);
        // CardInfo.Instance.effects[(int)EffectType.Buff](5, gameObject);
    }
    #endregion �̱���

    #region ī�� ȿ��
    // ī�� ȿ�� �Լ����� ��Ƶ� ��������Ʈ
    public delegate void CardEffects(int amount, GameObject target);
    // ��������Ʈ �迭, EffectType�� �´� �Լ��� ��Ī�Ѵ�.
    public CardEffects[] effects = new CardEffects[7];

    void EnrollAllEffect()
    {
        // ī�� ȿ���� �迭�� ���
        effects[(int)EffectType.Attack] += Attack;
        effects[(int)EffectType.Shield] += Shield;
        effects[(int)EffectType.Heal] += Heal;
        effects[(int)EffectType.Cleanse] += Cleanse;
        effects[(int)EffectType.RestoreCost] += RestoreCost;
        effects[(int)EffectType.Draw] += Draw;
        effects[(int)EffectType.Buff] += Buff;
    }

    public void Attack(int amount, GameObject target)
    {
        Debug.Log("Attack");
    }

    public void Shield(int amount, GameObject target)
    {
        Debug.Log("Shield");
    }

    public void Heal(int amount, GameObject target)
    {
        Debug.Log("Heal");
    }

    public void Cleanse(int amount, GameObject target)
    {
        Debug.Log("Cleanse");
    }

    public void RestoreCost(int amount, GameObject target)
    {
        Debug.Log("RestoreCost");
    }

    public void Draw(int amount, GameObject target)
    {
        Debug.Log("Draw");
    }

    public void Buff(int amount, GameObject target)
    {
        Debug.Log("Buff");
    }
    #endregion ī�� ȿ��
}
