using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    private void Awake() => Instance = this;

    [Header("Develop")]
    [SerializeField] [Tooltip("시작 턴 모드를 정합니다")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("드로우 카드 개수를 정합니다")] int drawCardCount;

    [Header("Properties")]
    public bool isLoading; // 게임 끝나면 isLoading을 true로 하면 카드와 엔티티 클릭방지
    public bool myTurn;

    enum ETurnMode { Random, My, Other }
    WaitForSeconds delay03 = new WaitForSeconds(0.3f);
    WaitForSeconds delay05 = new WaitForSeconds(0.5f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAddCard;

    // 턴 이벤트
    public UnityEvent onStartPlayerTurn;    // 플레이어 턴이 시작할 때
    public UnityEvent onStartEnemyTurn;     // 적 턴이 시작할 때
    public UnityEvent onEndPlayerTurn;      // 플레이어 턴이 끝날 때
    public UnityEvent onEndEnemyTurn;       // 적 턴이 끝날 때

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
        // 게임 세팅
        GameSetup();
        
        // 플레이어 턴으로 시작한다. 이벤트 호출
        onStartPlayerTurn.Invoke();

        isLoading = true;

        // 드로우 카드 수만큼 드로우
        for (int i = 0; i < drawCardCount; i++)
        {
/*            yield return delay05;
            OnAddCard?.Invoke(false);*/
            yield return delay05;
            OnAddCard?.Invoke(true);
        }

        yield return delay05;
        isLoading = false;
    }

    IEnumerator StartPlayerTurnCo()
    {
        // 턴 시작 UI 출력, 이 부분도 추후 수정해야 합니다.
        GameManager.Instance.Notification("나의 턴");

        // 플레이어 턴 시작 시 이벤트 호출
        onStartPlayerTurn.Invoke();

        // 우리 게임은 오직 플레이어만 드로우합니다.
        // 드로우 카드 수만큼 드로우
        for (int i = 0; i < drawCardCount; i++)
        {
            yield return delay05;
            OnAddCard?.Invoke(myTurn);
        }

        yield return delay05;
    }

    // 카드를 count 장 드로우 합니다.
    public IEnumerator DrawCard(int count)
    {
        // 드로우 카드 수만큼 드로우
        for (int i = 0; i < count; i++)
        {
            yield return delay03;
            OnAddCard?.Invoke(true);
        }
    }

    public void EndTurn()
    {
        if (BattleInfo.Instance.isGameOver)
        {
            return;
        }

        if (isLoading == true)
        {
            return;
        }

        // 상호작용을 막늗나.
        isLoading = true;

        StartCoroutine(EndTurnCo());
    }

    public IEnumerator EndTurnCo()
    {
        // 내 턴일 때 호출
        if (myTurn)
        {
            // 카드 전부 버리기
            yield return StartCoroutine(CardManager.Instance.DiscardHandCo());

            onEndPlayerTurn.Invoke();
            onStartEnemyTurn.Invoke();

            // 적 턴 직전, 플레이어가 죽으면 코루틴을 끝낸다.
            if (BattleInfo.Instance.isGameOver)
            {
                yield break;
            }

            // 적 턴으로 변경
            myTurn = false;

            // 적 턴 종료
            yield return StartCoroutine(EndTurnCo());

            // 적 턴 종료 후에도, 플레이어가 죽으면 코루틴을 끝낸다.
            if (BattleInfo.Instance.isGameOver)
            {
                yield break;
            }

            // 플레이어 턴 시작
            yield return StartCoroutine(StartPlayerTurnCo());

            isLoading = false;
        }
        // 적 턴일 때 호출
        else
        {
            onEndEnemyTurn.Invoke();
            onStartPlayerTurn.Invoke();

            // 플레이어 턴으로 변경
            myTurn = true;
        }
    }
}
