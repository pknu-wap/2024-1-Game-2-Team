using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ų�� �̸��� ������ ��Ƶδ� Ŭ���� (���� ���� �ൿ�� ��� �� ���)
[System.Serializable]
public class SkillText
{
    public string name;
    public string description;
}

[CreateAssetMenu(fileName = "SkillInfo", menuName = "Scriptable Object/Skill Info")]
public class SkillTextInfo : ScriptableObject
{
    public SkillText[] text;
}
