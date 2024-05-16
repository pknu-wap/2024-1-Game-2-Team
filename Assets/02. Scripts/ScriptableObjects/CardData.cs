// ���ö
using UnityEngine;

// ī���� ����
public enum SkillType
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

public enum SkillTarget
{
    Player,         // �÷��̾�
    Enemy,          // ��
    AllEnemy,       // ��� ��
}

// ī�尡 ���� �ִ� ��ų�� ����
[System.Serializable]   // �̰� �ٿ���� Inspector�� ��Ÿ����.
public class Skill
{
    [Header("ī�� ����")]
    // ī�� ȿ���� ����
    public SkillType type;
    // ��ų�� ������� Ÿ��
    public SkillTarget target;
    // ī�� ȿ���� ����Ǵ� ��ġ
    public int amount;
    // ȿ���� ���ӵǴ� �� ��
    public int turnCount;
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
    // ī�� ��ų
    public Skill[] skills;
    // ī�� �ߵ��� �ʿ��� �ڽ�Ʈ
    public int cost;
}
