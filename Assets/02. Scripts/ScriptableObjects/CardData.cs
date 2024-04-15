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
    Debuff          // ����
}

// ����Ƽ ������, Project ���� Create �޴��� �Ʒ� �׸��� �߰��Ѵ�.
[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Objects/Card Data", order = 0)]
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
    // ī�� �ߵ��� �ʿ��� �ڽ�Ʈ
    public int cost;
    // ī�� ȿ���� ����Ǵ� ��ġ
    public int amount;
    // ī�� ȿ���� ����
    public EffectType type;
}
