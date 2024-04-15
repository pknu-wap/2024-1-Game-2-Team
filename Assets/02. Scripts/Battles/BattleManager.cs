using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent onStartPlayerTurn;    // �÷��̾� ���� ������ ��
    public UnityEvent onStartEnemyTurn;     // �� ���� ������ ��
    public UnityEvent onEndPlayerTurn;      // �÷��̾� ���� ���� ��
    public UnityEvent onEndEnemyTurn;       // �� ���� ���� ��

    public void ToggleTurn()
    {
        // ���� �����Ѵ�.
        if (isPlayerTurn)
        {
            onEndPlayerTurn.Invoke();
            onStartEnemyTurn.Invoke();
            isPlayerTurn = false;
            Debug.Log("�� ��");

            // �� ���� ������ �ڵ����� �÷��̾� ������ �ٲ۴�.
            ToggleTurn();
        }
        else
        {
            onEndEnemyTurn.Invoke();
            onStartPlayerTurn.Invoke();
            isPlayerTurn = true;
            Debug.Log("�÷��̾� ��");
        }
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
