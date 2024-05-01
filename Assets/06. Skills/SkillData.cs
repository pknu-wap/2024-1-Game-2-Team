// ���ö
using UnityEngine;

[System.Serializable]
public class Skill
{
    [Header("����")]
    // ��ų �̸�
    public string skillName;
    // ��ų ����
    public string description;

    // ī�� ȿ���� ����
    public EnemySkillType type;
    // ī�� ȿ���� ����Ǵ� ��ġ
    public int amount;
    // ȿ���� ���ӵǴ� �� ��
    public int turnCount;
}

[CreateAssetMenu(fileName = "EnemySkillData", menuName = "Scriptable Object/Enemy Skill Data")]
public class SkillData : ScriptableObject
{
    public Skill[] skills;
}
