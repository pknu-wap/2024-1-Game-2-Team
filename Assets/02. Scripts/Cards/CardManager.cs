// �赿��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Card> hand;
    [SerializeField] Transform handLeft;
    [SerializeField] Transform handRight;
    [SerializeField] public Transform cardSpawnPoint;
    [SerializeField] public Transform cardDumpPoint;

    public List<CardData> deck;
    List<CardData> dump;
    Card selectCard;

    [SerializeField] float dotweenTime = 0.5f;
    [SerializeField] float focusOffset;

    public CardData DrawCard()
    {
        if (deck.Count == 0)
            ResetDeck();

        // queue�� dequeue�� ���� �� �� ���� ��
        CardData card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    void SetUpDeck()
    {
        deck = new List<CardData>(100);

        // itemSO�� ī����� deck�� �߰�
        for (int i = 0; i < itemSO.items.Length; i++) 
        {
            CardData card = itemSO.items[i];
            deck.Add(card);
        }

        // deck ����
        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            CardData temp = deck[i];
            deck[i] = deck[rand];
            deck[rand] = temp;
        }
    }

    void Start()
    {
        SetUpDeck();
        dump = new List<CardData>(100);
        // �� �̱��濡�� ȣ������ �ʰ� Action���� ȣ���ұ�,,,,
        TurnManager.OnAddCard += AddCardToHand;
    }

    void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCardToHand;
    }

    void AddCardToHand(bool isMine)
    {
        if (!isMine || hand.Count >= 10 || deck.Count == 0) return;
        var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(DrawCard());
        card.transform.localScale = Vector3.zero;
        hand.Add(card);

        SetOriginOrder();
        CardAlignment();
    }

    void SetOriginOrder()
    {
        int count = hand.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = hand[i];
            targetCard?.GetComponent<CardOrder>().SetOriginOrder(i);
        }
    }

    void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();
        float radius = handRight.position.x - handLeft.position.x;
        originCardPRSs = RoundAlignment(handLeft, handRight, hand.Count, radius, Vector3.one * 10f);

        for (int i = 0; i < hand.Count; i++)
        {
            var targetCard = hand[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
        }
    }

    // �����ϱ⸦ ������...
    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float radius, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        Vector3 circleCenter = Vector3.Lerp(leftTr.position, rightTr.position, 0.5f) + Vector3.down * radius;

        for (int i = 0; i < objCount; i++)
        {
            // Ÿ�� ��ġ�� leftTr�� rightTr ����, i��° ī���� ��ġ
            Vector3 targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            // targetRot�� �켱 �⺻������.
            Quaternion targetRot = Utils.QI;


            // ī�尡 4�� �̻��� ���� ȸ���� �����Ѵ�.
            if (objCount >= 4)
            {
                // ���� ������, (x-a)^2 + (y-b)^2 = r^2�� ����.
                // x = targetPos.x, a = circleCenter.x, y = curve, b = circleCenter.y, r = height                
                float curve = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(targetPos.x - circleCenter.x, 2));
                // �������� ��ȯ
                curve = Mathf.Abs(curve);
                targetPos.y = targetPos.y - radius + curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }

    public void DiscardCard(Card card)
    {
        hand.Remove(card);
        dump.Add(card.cardData);

        card.transform.DOKill();

        DestroyImmediate(card.gameObject);
        selectCard = null;

        CardAlignment();
    }

    public void ResetDeck()
    {
        deck = new List<CardData>(100);

        // dump�� ī����� deck�� �߰�
        for (int i = 0; i < dump.Count; i++)
        {
            CardData card = dump[i];
            deck.Add(card);
        }

        // deck ����
        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            CardData temp = deck[i];
            deck[i] = deck[rand];
            deck[rand] = temp;
        }

        // dump ���� (Clear �Լ��� ����)
        dump = new List<CardData>(100);
    }

    public IEnumerator DiscardHandCo()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            // ī�� ���� ��ġ�� ���ư��� ����. ���߿� �����ε� �ٲ�� �Ѵ�.
            Sequence sequence = DOTween.Sequence()
                .Append(hand[i].transform.DOMove(cardDumpPoint.position, dotweenTime))
                .Join(hand[i].transform.DORotateQuaternion(Utils.QI, dotweenTime))
                .Join(hand[i].transform.DOScale(Vector3.one, dotweenTime))
                .SetEase(Ease.OutQuad);
            /*            Sequence sequence = DOTween.Sequence()
                        .Append(hand[i].transform.DOLocalMoveY(0.5f, 0.9f).SetEase(Ease.OutQuad))
                        .Join(hand[i].GetComponent<SpriteRenderer>().DOFade(0, 0.9f).SetEase(Ease.InExpo));*/
        }

        // sequence ������ ������ ��ٸ���
        yield return new WaitForSeconds(dotweenTime);

        // sequence�� ������ ��� ������Ʈ �ı�
        for (int i = 0; i < hand.Count; i++)
        {
            Card card = hand[i];
            dump.Add(card.cardData);

            DestroyImmediate(card.gameObject);
        }

        hand = new List<Card>(100);
        selectCard = null;
    }

    #region MyCard

    public void CardMouseEnter(Card card)
    {
        selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }

    public void CardMouseDown()
    {
        if(TurnManager.Inst.myTurn)
            DiscardCard(selectCard);
    }

    void EnlargeCard(bool isEnlarge, Card card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, handLeft.position.y + focusOffset, -3f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, card.originPRS.scale * 1.2f), false);
        }
        else
        {
            card.MoveTransform(card.originPRS, false);
        }
        
        card.GetComponent<CardOrder>().SetMostFrontOrder(isEnlarge);
    }

    #endregion
}
