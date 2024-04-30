// �赿��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Progress;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] List<Card> hand;
    [SerializeField] Transform handLeft;
    [SerializeField] Transform handRight;
    [SerializeField] Transform cardSpawnPoint;

    public List<Item> deck;
    List<Item> dump;
    Card selectCard;

    public Item DrawCard()
    {
        // queue�� dequeue�� ���� �� �� ���� ��
        Item card = deck[0];
        deck.RemoveAt(0);
        return card;
    }

    void SetUpDeck()
    {
        deck = new List<Item>(100);

        // itemSO�� ī����� deck�� �߰�
        for (int i = 0; i < itemSO.items.Length; i++) 
        {
            // �� �ٷ� ���̸� Item�� �����ϴ� ����� ���� �ʴ´�.
            Item card = itemSO.items[i];
            deck.Add(card);
        }

        // deck ����
        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            Item temp = deck[i];
            deck[i] = deck[rand];
            deck[rand] = temp;
        }
    }

    void Start()
    {
        SetUpDeck();
        dump = new List<Item>(100);
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

    public float h = 1000f;

    void CardAlignment()
    {
        List<PRS> originCardPRSs = new List<PRS>();
        originCardPRSs = RoundAlignment(handLeft, handRight, hand.Count, h, Vector3.one * 10f);

        for (int i = 0; i < hand.Count; i++)
        {
            var targetCard = hand[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
        }
    }

    // �����ϱ⸦ ������...
    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
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

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }

    public void DiscardCard(Card card)
    {

        hand.Remove(card);
        dump.Add(card.item);

        card.transform.DOKill();

        DestroyImmediate(card.gameObject);
        selectCard = null;

        CardAlignment();
    }

    public void ResetDeck()
    {
        deck = new List<Item>(100);

        // dump�� ī����� deck�� �߰�
        for (int i = 0; i < dump.Count; i++)
        {
            Item card = dump[i];
            deck.Add(card);
        }

        // deck ����
        for (int i = 0; i < deck.Count; i++)
        {
            int rand = Random.Range(i, deck.Count);
            Item temp = deck[i];
            deck[i] = deck[rand];
            deck[rand] = temp;
        }

        // dump ����
        dump = new List<Item>(100);
    }

    public IEnumerator DiscardHandCo()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            Sequence sequence = DOTween.Sequence()
            .Append(hand[i].transform.DOLocalMoveY(0.5f, 0.9f).SetEase(Ease.OutQuad))
            .Join(hand[i].GetComponent<SpriteRenderer>().DOFade(0, 0.9f).SetEase(Ease.InExpo));
        }

        // sequence ������ ������ ��ٸ���
        yield return new WaitForSeconds(0.9f);

        // sequence�� ������ ��� ������Ʈ �ı�
        for (int i = 0; i < hand.Count; i++)
        {
            Card card = hand[i];
            dump.Add(card.item);

            DestroyImmediate(card.gameObject);
        }

        hand = new List<Card>(100);
        selectCard = null;
    }

    #region MyCard

    public void CardMouseOver(Card card)
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
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, card.originPRS.pos.y + h, -3f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, card.originPRS.scale * 1.2f), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<CardOrder>().SetMostFrontOrder(isEnlarge);
    }

    #endregion
}
