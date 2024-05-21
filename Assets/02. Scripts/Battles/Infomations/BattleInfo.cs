// ���ö
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{
    public static BattleInfo Inst { get; private set; }
    void Awake() {
        Inst = this;

        remainingEnemies = new List<Enemy>();
    }

    private void Start()
    {
        ResetCost();
        TurnManager.Inst.onStartPlayerTurn.AddListener(ResetCost);
    }

    [Header("���� ����")]
    // ���� �� ������Ʈ �迭
    public List<Enemy> remainingEnemies;
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

    // �� ������Ʈ ����Ѵ�. ���丮 ���� ����Ǹ� ������ ���� �ִ�.
    public void EnrollEnemy(Enemy enemy)
    {
        remainingEnemies.Add(enemy);
    }

    // �� ������Ʈ�� �����Ѵ�.
    public void DisenrollEnemy(Enemy enemy)
    {
        // ����Ʈ�� �̹� ����ִٸ� �����Ѵ�. (���� ó��)
        if(remainingEnemies.Any() == false)
        {
            return;
        }

        // ����Ʈ���� enemy�� �����Ѵ�.
        remainingEnemies.Remove(enemy);

        // ���� �� ����Ʈ�� ����ִٸ�
        if(remainingEnemies.Any() == false)
        {
            // ������ �����Ѵ�.
            isGameOver = true;

            GameManager.Inst.Notification("�¸�");

            // ���� ���丮�� �����Ѵ�.
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
