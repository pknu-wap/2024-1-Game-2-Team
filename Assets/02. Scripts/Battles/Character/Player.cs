// ���ö
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DecreaseHP(10);
        }
    }
    public override void Die()
    {
        // �÷��̾��� ����� �˸�
        BattleManager.Instance.GameOver();
    }
}
