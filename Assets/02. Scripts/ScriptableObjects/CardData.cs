// ���ö
using UnityEngine;

public enum EffectType
{
    Attack,         // ����
    Shield,         // �ǵ� ����
    Heal,           // ü�� ȸ��
    Cleanse,        // ����� ����
    RestoreCost,    // �ڽ�Ʈ ȸ��
    Draw,           // ī�� ��ο�
    Buff            // ����
}

[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Objects/Card Data", order = 0)]
public class CardData : ScriptableObject
{
    [Header("ī�� ���")]
    public new string name;
    [TextArea(3, 5)]
    public string description;
    public Sprite sprite;

    [Header("ī�� ����")]
    public int cost;
    public int amount;
    public EffectType effect;
}
