// ���ö
using UnityEngine;

// ī���� ����
public enum EffectType
{
    Attack,         // ����
    Shield,         // �ǵ� ����
    Heal,           // ü�� ȸ��
    Cleanse,        // ����� ����
    RestoreCost,    // �ڽ�Ʈ ȸ��
    Draw,           // ī�� ��ο�
    Buff,           // ����
    Debuff,         // �����
    Bleed,          // ����
}

// ����Ƽ ������, Project ���� Create �޴��� �Ʒ� �׸��� �߰��Ѵ�.
[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data", order = 0)]
public class CardData : ScriptableObject
{
    [Header("ī�� ���")]
    // ī�� �̸�
    public new string name;
    // ī�� ����
    [TextArea(3, 5)]
    public string description;
    // ī�� �Ϸ���Ʈ
    public Sprite sprite;

    [Header("ī�� ����")]
    // ī�� ȿ���� ����
    public EffectType type;
    // ī�� �ߵ��� �ʿ��� �ڽ�Ʈ
    public int cost;
    // ī�� ȿ���� ����Ǵ� ��ġ
    public int amount;
    // ȿ���� ���ӵǴ� �� ��
    public int turnCount;
}
