// ���ö
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{
    [Header("���� ����")]
    // ���� �� ����
    public static int remainingEnemies;

    [Header("�÷��̾� �ɷ�ġ")]
    // �߰� ���ݷ�. ������ ��� �Ŀ� ����
    public static int bonusAttackStat;
    // �߰� ������. ������ ��� ��, �߰��� ���� ���� ������
    public static int bonusDamage;
    // �߰� ����. ������ ��� �Ŀ� ����
    public static int bonusArmor;

    // ��� ���� �ʱ�ȭ�Ѵ�.
    public void ResetBattleInfo()
    {
        // ���� �� ���ڸ� �� ���ڿ� ���� �ʱ�ȭ�Ѵ�.
        bonusAttackStat = 0;
        bonusDamage = 0;
        bonusArmor = 0;
    }
}
