using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    IEnumerator StartTurnCo()
    {
        isLoading = true;

        // �� ���� UI ���
        if (myTurn)
            GameManager.Inst.Notification("���� ��");

        // ���� ������ ������ �����ؼ� ���� ����
        if(CardManager.Inst.deck.Count == 0)
            CardManager.Inst.ResetDeck();

        // ��ο� ī�� ����ŭ ��ο�
        for (int i = 0; i < drawCardCount; i++)
        {
            yield return delay03;
            OnAddCard?.Invoke(myTurn);
        }
        yield return delay07;

        isLoading = false;
    }

    public void EndTurn()
    {
        if(myTurn)
            StartCoroutine(CardManager.Inst.DiscardHandCo());
        myTurn = !myTurn;
        StartCoroutine(StartTurnCo());
    }
}
