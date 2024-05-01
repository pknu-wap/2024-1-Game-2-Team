// ���ö
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{
    public static BattleInfo Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("���� ����")]
    // ���� �� ����
    public int remainingEnemies;
    // ���� ���� ����
    public bool isGameOver = false;

    [Header("�÷��̾� �ɷ�ġ")]
    // �߰� ���ݷ�. ������ ��� �Ŀ� ����
    public int bonusAttackStat;
    // �߰� ������. ������ ��� ��, �߰��� ���� ���� ������
    public int bonusDamage;
    // �߰� ����. ������ ��� �Ŀ� ����
    public int bonusArmor;

    // ��� ���� �ʱ�ȭ�Ѵ�.
    public void ResetBattleInfo()
    {
        // ���� �� ���ڸ� �� ���ڿ� ���� �ʱ�ȭ�Ѵ�.
        bonusAttackStat = 0;
        bonusDamage = 0;
        bonusArmor = 0;
    }

    // �� ������Ʈ ���ڸ� 1 ���δ�.
    public void IncreaseEnemyCount()
    {
        ++remainingEnemies;
    }

    // �� ������Ʈ ���ڸ� 1 ���δ�.
    public void DecreaseEnemyCount()
    {
        --remainingEnemies;

        if(remainingEnemies <= 0)
        {
            isGameOver = true;

            GameManager.Inst.Notification("�¸�");
        }
    }
}
