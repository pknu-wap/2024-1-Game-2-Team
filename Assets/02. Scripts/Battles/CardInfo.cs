// ���ö
using UnityEngine;

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
        EnrollAllEffect();
    }
    #endregion �̱���

    #region ī�� UI ������
    public Sprite[] Icons;
    #endregion ī�� UI ������

    #region ī�� ȿ��
    // ī�� ȿ�� �Լ����� ��Ƶ� ��������Ʈ
    public delegate void CardEffects(int amount, GameObject target);
    // ��������Ʈ �迭, EffectType�� �´� �Լ��� ��Ī�Ѵ�.
    public CardEffects[] effects = new CardEffects[7];

    // ��� ����
    // CardInfo.Instance.effects[(int)ȿ�� ����](ȿ����, ���);
    // CardInfo.Instance.effects[(int)EffectType.Buff](5, gameObject);

    // ��� ȿ���� effects �迭�� ����Ѵ�.
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

    // target�� null�� ���� Card�� OnEndDrag���� �˻������Ƿ�, �˻����� �ʴ´�.
    public void Attack(int amount, GameObject target)
    {
        Enemy enemy = target.GetComponent<Enemy>();

        enemy.DecreaseHP(amount);
        Debug.Log(enemy.GetHP());
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
