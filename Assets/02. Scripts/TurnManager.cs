using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    private void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField] [Tooltip("���� �� ��带 ���մϴ�")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("��ο� ī�� ������ ���մϴ�")] int drawCardCount;

    [Header("Properties")]
    public bool isLoading; // ���� ������ isLoading�� true�� �ϸ� ī��� ��ƼƼ Ŭ������
    public bool myTurn;

    enum ETurnMode { Random, My, Other }
    WaitForSeconds delay03 = new WaitForSeconds(0.3f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAddCard;

    // �� �̺�Ʈ
    public UnityEvent onStartPlayerTurn;    // �÷��̾� ���� ������ ��
    public UnityEvent onStartEnemyTurn;     // �� ���� ������ ��
    public UnityEvent onEndPlayerTurn;      // �÷��̾� ���� ���� ��
    public UnityEvent onEndEnemyTurn;       // �� ���� ���� ��

    void GameSetup()
    {
        switch (eTurnMode)
        {
            case ETurnMode.Random:
                myTurn = Random.Range(0, 2) == 0;
                break;
            case ETurnMode.My:
                myTurn = true;
                break;
            case ETurnMode.Other:
                myTurn = false;
                break;
        }
    }

    public IEnumerator StartGameCo()
    {
        // ���� ����
        GameSetup();
        isLoading = true;

        // ��ο� ī�� ����ŭ ��ο�
        for (int i = 0; i < drawCardCount; i++)
        {
/*            yield return delay05;
            OnAddCard?.Invoke(false);*/
            yield return delay03;
            OnAddCard?.Invoke(true);
        }

        yield return delay03;
        isLoading = false;
    }

    IEnumerator StartPlayerTurnCo()
    {
        // �� ���� UI ���, �� �κе� ���� �����ؾ� �մϴ�.
        GameManager.Inst.Notification("���� ��");

        // �츮 ������ ���� �÷��̾ ��ο��մϴ�.
        // ��ο� ī�� ����ŭ ��ο�
        for (int i = 0; i < drawCardCount; i++)
        {
            yield return delay03;
            OnAddCard?.Invoke(myTurn);
        }

        yield return delay03;
    }

    public void EndTurn()
    {
        if (isLoading == true)
        {
            return;
        }

        // ��ȣ�ۿ��� ���f��.
        isLoading = true;

        StartCoroutine(EndTurnCo());
    }

    public IEnumerator EndTurnCo()
    {
        // �� ���� �� ȣ��
        if (myTurn)
        {
            // ī�� ���� ������
            yield return StartCoroutine(CardManager.Inst.DiscardHandCo());

            onEndPlayerTurn.Invoke();
            onStartEnemyTurn.Invoke();

            // �� ������ ����
            myTurn = false;

            // �� �� ����
            yield return StartCoroutine(EndTurnCo());

            // �÷��̾� �� ����
            yield return StartCoroutine(StartPlayerTurnCo());

            isLoading = false;
        }
        // �� ���� �� ȣ��
        else
        {
            onEndEnemyTurn.Invoke();
            onStartPlayerTurn.Invoke();

            // �÷��̾� ������ ����
            myTurn = true;
        }
    }
}
