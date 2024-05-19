using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    #region ����
    [Header("ī�� ����")]
    public CardData cardData;

    [Header("������Ʈ")]
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer illust;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text costTMP;
    [SerializeField] TMP_Text descriptionTMP;
    [SerializeField] CardOrder cardOrder;
    [SerializeField] Collider2D cardCollider;

    [Header("����")]
    [SerializeField] bool isDragging = false;
    [SerializeField] bool isTargetingCard = false;
    public PRS originPRS;

    [Header("��Ÿ�� ����")]
    // ī�� �ߵ� �� ���õ� ������Ʈ
    public Character[] selectedCharacter;

    // DOTween ������
    Sequence moveSequence;
    Sequence disappearSequence;
    [SerializeField] float dotweenTime = 0.4f;
    [SerializeField] float focusTime = 0.4f;
    #endregion ����

    public void Setup(CardData item)
    {
        cardData = item;

        illust.sprite = cardData.sprite;
        nameTMP.text = cardData.name;
        costTMP.text = cardData.cost.ToString();
        descriptionTMP.text = cardData.description;

        cardOrder = GetComponent<CardOrder>();
        cardCollider = GetComponent<Collider2D>();

        isTargetingCard = CardInfo.Instance.IsTargetingCard(cardData.skills);
    }

    #region ���콺 ��ȣ�ۿ�
    // ���콺�� ī�� ���� �ø� �� ����ȴ�.
    void OnMouseEnter()
    {
        if (BattleInfo.Inst.isGameOver)
        {
            return;
        }

        CardManager.Inst.CardMouseEnter(this);

        // ���� ���� moveSequence�� �ִٸ� �����Ѵ�.
        moveSequence.Kill();
    }

    // ���콺�� ī�带 ��� �� ����ȴ�.
    void OnMouseExit()
    {
        if (BattleInfo.Inst.isGameOver)
        {
            return;
        }

        if(isDragging == true)
        {
            return;
        }

        CardManager.Inst.CardMouseExit(this);
    }

    Vector3 arrowOffset = new Vector3(0f, 100f, 3f);

    // �巡�װ� ���۵� �� ȣ��ȴ�.
    public void OnMouseDown()
    {
        if (BattleInfo.Inst.isGameOver)
        {
            return;
        }

        // ���� ���� moveSequence�� �ִٸ� �����Ѵ�.
        moveSequence.Kill();

        // �ٸ� ī���� ���콺 �̺�Ʈ�� ���´�.
        CardArrow.Instance.ShowBlocker();

        // ���� ī���� ���
        if (isTargetingCard)
        {
            // ���� ���콺�� ��ġ�� ����Ѵ�.
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // ǥ�� ���� ��ġ�� �Ű�, ������ ������ �̻��� �� ����
            CardArrow.Instance.MoveStartPosition(transform.position + arrowOffset);
            CardArrow.Instance.MoveArrow(worldPosition);

            // ȭ��ǥ�� ǥ���Ѵ�.
            CardArrow.Instance.ShowArrow();

            // �߾ӿ��� ��Ŀ����Ų��.
            FocusCardOnCenter();
        }
        // ���� ī�尡 �ƴ� ���, �ƹ� �ϵ� ���� �ʴ´�. (ī�尡 ���콺�� ����)

        // �巡�� ������ ǥ��
        isDragging = true;
    }

    // �巡�� ���� �� ��� ȣ��ȴ�.
    public void OnMouseDrag()
    {
        if (BattleInfo.Inst.isGameOver)
        {
            return;
        }

        // ���� ���콺�� ��ġ�� ����Ѵ�.
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (isTargetingCard)
        {
            CardArrow.Instance.MoveStartPosition(transform.position + arrowOffset);
            // Ÿ���� ī���� ���, ȭ��ǥ�� ���� ���콺�� ���Ѵ�.
            CardArrow.Instance.MoveArrow(worldPosition);
        }
        else
        {
            // ��Ÿ���� ī��� ī�尡 ���콺�� �ε巴�� ����ٴѴ�.
            transform.position = Vector2.Lerp(transform.position, worldPosition, 0.06f);
        }
    }

    // �巡�װ� ���� �� ȣ��ȴ�.
    public void OnMouseUp()
    {
        if (BattleInfo.Inst.isGameOver)
        {
            return;
        }

        if (isTargetingCard)
        {
            // ȭ��ǥ�� �����.
            CardArrow.Instance.HideArrow();
        }

        // �ٸ� ī�尡 ���콺 �̺�Ʈ�� �ް� �Ѵ�.
        CardArrow.Instance.HideBlocker();

        // �巡�װ� ������ ǥ��
        isDragging = false;

        // ī�带 ����Ѵ�.
        UseCard();
    }
    #endregion ���콺 ��ȣ�ۿ�

    #region ī�� ���
    // ī�带 ����Ѵ�. (���콺�� �������� ������ ȣ��)
    private void UseCard()
    {
        // �ڽ�Ʈ�� ���ڶ� ���
        if (BattleInfo.Inst.CanUseCost(cardData.cost) == false)
        {
            // ī�� �ߵ��� ����Ѵ�.
            CancelUsingCard();

            return;
        }

        // Ÿ���� ��ų�� ��
        if (isTargetingCard)
        {
            LayerMask layer = LayerMask.GetMask("Enemy");

            // layer�� ��ġ�ϴ�, ���õ� ������Ʈ�� �����´�.
            GameObject selectedObject = GetClickedObject(layer);

            // ������Ʈ�� ���õ��� �ʾҴٸ�
            if (selectedObject == null)
            {
                // ī�� �ߵ��� ����Ѵ�.
                CancelUsingCard();

                return;
            }

            // ���� ���� Ÿ���� ���ؾ� �Ѵ�.
            // �� ������Ʈ�� Enemy ��ũ��Ʈ�� �����´�
            Enemy selectedEnemy = selectedObject.GetComponent<Enemy>();

            // ī���� ��� ȿ���� �ߵ��Ѵ�.
            for (int i = 0; i < cardData.skills.Length; ++i)
            {
                // Ÿ���� ���Ѵ�.
                selectedCharacter = CardInfo.Instance.GetTarget(cardData.skills[i].target, selectedEnemy);

                // �ش� Ÿ�ٿ��� ��ų�� �����Ѵ�.
                CardInfo.Instance.ActivateSkill(cardData.skills[i], selectedCharacter);
            }

            // �ڽ�Ʈ�� ���ҽ�Ų��.
            BattleInfo.Inst.UseCost(cardData.cost);

            // ī�带 ������ ������. ������ �� ���ä�� ���ϰ� Collider�� ��� ���д�.
            cardCollider.enabled = false;
            // �ϴ� �Ʒ��� �ڵ带 �״�� �����Դ�. �Լ�ȭ�ϸ� ���� ��
            moveSequence = DOTween.Sequence()
                .Append(transform.DOMove(CardManager.Inst.cardDumpPoint.position, dotweenTime))
                .Join(transform.DORotateQuaternion(Utils.QI, dotweenTime))
                .Join(transform.DOScale(Vector3.one, dotweenTime))
                .OnComplete(() => {
                    CardManager.Inst.DiscardCard(this);
                    cardCollider.enabled = true;
                }); // �ִϸ��̼� ������ �п��� ����
        }

        // ��Ÿ�� ��ų�� ��
        else
        {
            // Field ���̾ �����Ѵ�.
            LayerMask layer = LayerMask.GetMask("Field");

            // layer�� ��ġ�ϴ�, ���õ� ������Ʈ�� �����´�.
            GameObject selectedObject = GetClickedObject(layer);

            // ������Ʈ�� ���õ��� �ʾҴٸ�
            if (selectedObject == null)
            {
                // ī�� �ߵ��� ����Ѵ�.
                CancelUsingCard();

                return;
            }

            // ī���� ��� ȿ���� �ߵ��Ѵ�.
            for (int i = 0; i < cardData.skills.Length; ++i)
            {
                // Ÿ���� ���Ѵ�. Ÿ���� ī�尡 �ƴϴ�, selectedEnemy�� ����.
                selectedCharacter = CardInfo.Instance.GetTarget(cardData.skills[i].target);

                CardInfo.Instance.ActivateSkill(cardData.skills[i], selectedCharacter);
            }

            // �ڽ�Ʈ�� ���ҽ�Ų��.
            BattleInfo.Inst.UseCost(cardData.cost);

            // ī�带 ������ ������. ������ �� ���ä�� ���ϰ� Collider�� ��� ���д�.
            cardCollider.enabled = false;
            // �ϴ� �Ʒ��� �ڵ带 �״�� �����Դ�. �Լ�ȭ�ϸ� ���� ��
            moveSequence = DOTween.Sequence()
                // �߾����� �̵��ϰ�
                .Append(transform.DOMove(Vector3.zero, dotweenTime))
                .Join(transform.DORotateQuaternion(Utils.QI, dotweenTime))
                .Join(transform.DOScale(originPRS.scale * 1.2f, dotweenTime))
                // 1�ʰ� ����
                .AppendInterval(focusTime)
                // ������ �̵��Ѵ�.
                .Append(transform.DOMove(CardManager.Inst.cardDumpPoint.position, dotweenTime))
                .Join(transform.DORotateQuaternion(Utils.QI, dotweenTime))
                .Join(transform.DOScale(Vector3.one, dotweenTime))
                .OnComplete(() => {
                    CardManager.Inst.DiscardCard(this);
                    cardCollider.enabled = true;
                }); // �ִϸ��̼� ������ �п��� ����
        }

        
    }

    // ī�� �ߵ��� ����Ѵ�.
    void CancelUsingCard()
    {
        MoveTransform(originPRS, true, 0.5f);
        cardOrder.SetMostFrontOrder(false);
    }
    // Ŭ����(�巡�� �� ���콺�� �� ����) ������Ʈ�� �����´�.
    GameObject GetClickedObject(LayerMask layer)
    {
        // ���콺 ��ġ�� �޾ƿ´�.
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ���콺 ��ġ�� ����ĳ��Ʈ�� ���, layer�� ��ġ�ϴ� ������Ʈ �� ���� ���� �浹�� ������Ʈ�� ��ȯ�Ѵ�.
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f, layer);

        // layer�� ��ġ�ϴ� ������Ʈ�� ã�Ҵٸ�
        if (hit.collider != null)
        {
            // �ش� ������Ʈ�� ��ȯ�ϰ�
            return hit.transform.gameObject;
        }

        // �ƴ϶�� null�� ��ȯ�Ѵ�.
        return null;
    }
    #endregion ī�� ���

    #region �ִϸ��̼�
    public void MoveTransform(PRS destPRS, bool useDotween, float dotweenTime = 0)
    {
        // �巡�� �߿� ������� �ʰ� �� ���ڿ������� �������� �����Ѵ�.
        if (isDragging == true)
        {
            return;
        }

        if (useDotween)
        {
            // moveSequence�� ��ġ, ȸ��, �������� �����ϴ� DOTween�� �����ߴ�.
            moveSequence = DOTween.Sequence()
                .Append(transform.DOMove(destPRS.pos, dotweenTime))
                .Join(transform.DORotateQuaternion(destPRS.rot, dotweenTime))
                .Join(transform.DOScale(destPRS.scale, dotweenTime));
        }

        else
        {
            transform.position = destPRS.pos;
            transform.rotation = destPRS.rot;
            transform.localScale = destPRS.scale;
        }
    }

    // ī�带 �߾ӿ��� �����Ѵ�.
    void FocusCardOnCenter()
    {
        MoveTransform(new PRS(CardManager.Inst.focusPos, Utils.QI, originPRS.scale * 1.2f), true, dotweenTime);

        cardOrder.SetMostFrontOrder(true);
    }
    #endregion �ִϸ��̼�
}
