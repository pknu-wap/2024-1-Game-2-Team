// ���ö
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{
    public static BattleInfo Inst { get; private set; }
    void Awake() => Inst = this;

    private void Start()
    {
        ResetCost();
        TurnManager.Inst.onStartPlayerTurn.AddListener(ResetCost);
    }

    [Header("���� ����")]
    // ���� �� ����
    public int remainingEnemies;
    // ���� ���� ����
    public bool isGameOver = false;
    // �ִ� �ڽ�Ʈ
    public int maxCost = 5;
    // ���� �ڽ�Ʈ
    public int currentCost = 5;

    [Header("�÷��̾� �ɷ�ġ")]
    // �߰� ���ݷ�. ������ ��� �Ŀ� ����
    public int bonusAttackStat;
    // �߰� ������. ������ ��� ��, �߰��� ���� ���� ������
    public int bonusDamage;
    // �߰� ����. ������ ��� �Ŀ� ����
    public int bonusArmor;

    [Header("������Ʈ")]
    public TMP_Text costText;

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

    // cost�� ī�� ����� �������� �˷��ش�.
    public bool CanUseCost(int cost)
    {
        return currentCost - cost >= 0;
    }


    // �ڽ�Ʈ�� ���δ�. ���� �� true, ���� �� false�� ��ȯ�Ѵ�.
    public bool UseCost(int cost)
    {
        if(currentCost - cost < 0)
        {
            return false;
        }

        currentCost -= cost;
        UpdateCostText();

        return true;
    }

    // �ڽ�Ʈ�� maxCost�� ������Ų��.
    public void ResetCost()
    {
        currentCost = maxCost;
        UpdateCostText();
    }

    void UpdateCostText()
    {
        costText.text = currentCost + "/" + maxCost;
    }
}
