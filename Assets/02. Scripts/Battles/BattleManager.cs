using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ����, ���� ����, ���� �ߴ� ���� �ٷ�� Ŭ����
public class BattleManager : MonoBehaviour
{
    #region �̱���
    public static BattleManager Instance { get; set; }
    private static BattleManager instance;

    public void Awake()
    {
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

    #region �� ����
    public bool isPlayerTurn = true;

    public void ToggleTurn()
    {
        // ���� �����Ѵ�.
    }

    public void StartTurn(bool isPlayerTurn)
    {
        // �÷��̾��� ���� �����Ѵ�.
        if (isPlayerTurn)
        {

        }

        // ���� ���� �����Ѵ�.
        else
        {

        }
    }

    public void EndTurn(bool isPlayerTurn)
    {

    }
    #endregion �� ����

    public void GameOver()
    {
        Debug.Log("���� ����");
        // ������ ��� ����� �����Ѵ�. -> �Է��� ����, ���� �ݶ��̴� ����?
        // ���� ���� �޽����� ����Ѵ�.
        // ��Ȱ�� ������ ���, ��Ȱ �˾��� ����. <- ������ ������ ȸ�� �ʿ�

        // ������ ���½�Ų��. Ȥ�� ó������ �ٽ� �����Ѵ�. -> ��� ���������� ������.
    }
}
