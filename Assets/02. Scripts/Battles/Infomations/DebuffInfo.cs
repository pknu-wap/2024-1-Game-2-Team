using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffInfo
{
    public static Dictionary<EnemySkillType, string> debuffNameDict = new Dictionary<EnemySkillType, string>
    {
        { EnemySkillType.Bleed, "����" }
    };
    public static Dictionary<EnemySkillType, string> debuffDescriptionDict = new Dictionary<EnemySkillType, string>
    {
        { EnemySkillType.Bleed, "�� �� 2�� ���ظ� �Խ��ϴ�." }
    };
}
