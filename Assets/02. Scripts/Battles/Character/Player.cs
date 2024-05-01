// ���ö
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character
{
    #region �̱���
    public static Player Instance { get; set; }
    private static Player instance;

    public override void Awake()
    {
        base.Awake();

        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }
    #endregion �̱���

    protected override void Start()
    {
        base.Start();

        TurnManager.Inst.onEndPlayerTurn.AddListener(EndPlayerTurn);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DecreaseHP(10);
        }
    }

    public void EndPlayerTurn()
    {
        GetBleedAll();
    }

    public override void Die()
    {
        // �÷��̾��� ����� �˸�
        BattleInfo.Inst.isGameOver = true;

        currentHp = 0;
        UpdateCurrentHP();

        GameManager.Inst.Notification("���� ����");
    }
}
