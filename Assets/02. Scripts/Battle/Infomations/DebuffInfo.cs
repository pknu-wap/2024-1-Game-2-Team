using System.Collections;
using System.Collections.Generic;

public class DebuffInfo
{
    public static Dictionary<SkillType, string> debuffNameDict = new Dictionary<SkillType, string>
    {
        { SkillType.Bleed, "����" }
    };
    public static Dictionary<SkillType, string> debuffDescriptionDict = new Dictionary<SkillType, string>
    {
        { SkillType.Bleed, "�� �� 2�� ���ظ� �Խ��ϴ�." }
    };
}
