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
    public PRS originPRS;

    // DOTween ������
    Sequence moveSequence;
    Sequence disappearSequence;
    float dotweenTime = 0.3f;
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
    }

    #region ���콺 ��ȣ�ۿ�
    // ���콺�� ī�� ���� �ø� �� ����ȴ�.
    void OnMouseEnter()
    {
        Debug.Log("Enter");
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
        Debug.Log("Exit");
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

    // �巡�װ� ���۵� �� ȣ��ȴ�.
    public void OnMouseDown()
    {
        Debug.Log("Down");
        if (BattleInfo.Inst.isGameOver)
        {
            return;
        }

        // ���� ī���� ��� ȭ��ǥ�� ǥ���Ѵ�.
        CardArrow.Instance.ShowArrow();

        // ���� ī�尡 �ƴ� ���, �ƹ� �ϵ� ���� �ʴ´�.

        // ���� ���� moveSequence�� �ִٸ� �����Ѵ�.
        moveSequence.Kill();

        // �߾ӿ��� ��Ŀ����Ų��.
        FocusCardOnCenter();

        // �巡�� ������ ǥ��
        isDragging = true;
    }

    // ī�带 �߾ӿ��� �����Ѵ�.
    void FocusCardOnCenter()
    {
        MoveTransform(new PRS(CardManager.Inst.focusPos, Utils.QI, originPRS.scale * 1.2f), true, dotweenTime);

        cardOrder.SetMostFrontOrder(true);
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
        Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // ���� ī���� ���, ȭ��ǥ�� ���� ���콺�� ���Ѵ�.
        CardArrow.Instance.MoveArrow(WorldPosition);
    }

    // �巡�װ� ���� �� ȣ��ȴ�.
    public void OnMouseUp()
    {
        Debug.Log("Up");
        if (BattleInfo.Inst.isGameOver)
        {
            return;
        }

        // ȭ��ǥ�� �����.
        CardArrow.Instance.HideArrow();

        // �巡�װ� ������ ǥ��
        isDragging = false;

        // ī�带 ����Ѵ�.
        UseCard();
    }
    #endregion ���콺 ��ȣ�ۿ�

    #region ī�� ���
    // ī�� �ߵ� �� ���õ� ������Ʈ
    public Character selectedCharacter;

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

        // ī�� ������ ���� Enemy �Ǵ� Field ���̾ �����Ѵ�.
        LayerMask layer = CardInfo.Instance.ReturnLayer(cardData.type);

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
        // �� ������Ʈ�� �����ϴ� ī����
        if (layer == LayerMask.GetMask("Enemy"))
        {
            // �� ������Ʈ�� Character ��ũ��Ʈ�� ��������
            selectedCharacter = selectedObject.GetComponent<Character>();
        }
        // �� �ܴ�
        else
        {
            // Player�� �����´�.
            selectedCharacter = Player.Instance;
        }

        // ī�带 �ߵ��Ѵ�. ���� ī���� ��� ���õ� ������ �ߵ��Ѵ�.
        CardInfo.Instance.effects[(int)cardData.type](cardData.amount, cardData.turnCount, selectedCharacter);
        // �ڽ�Ʈ�� ���ҽ�Ų��.
        BattleInfo.Inst.UseCost(cardData.cost);

        // ī�带 ������ ������. ������ �� ���ä�� ���ϰ� Collider�� ��� ���д�.
        cardCollider.enabled = false;
        // ī�� ���� ��ġ�� ���ư��� ����. ���߿� �����ε� �ٲ�� �Ѵ�. -> �ٲ��
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
    #endregion �ִϸ��̼�
}
