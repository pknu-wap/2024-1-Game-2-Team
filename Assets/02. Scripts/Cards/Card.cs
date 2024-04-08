// ���ö
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    #region ����
    [Header("ī�� ����")]
    public CardData cardData;

    [Header("ī�� UI")]
    private Image spriteRenderer;
    private TextMeshProUGUI descriptionText;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI costText;
    #endregion ����

    #region ����������Ŭ
    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<Image>();
        descriptionText = transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        nameText = transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
        costText = transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();

        UpdateCardInfo(cardData);
    }
    #endregion ����������Ŭ

    #region ���콺 ����(Mouse Hover)
    // ���콺�� ī�� ���� �ö�� �� ����ȴ�.
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // ī���� ũ�⸦ Ű���. (1.2���)
        UpscaleCard();
    }

    // ���콺�� ī�� ������ ��� �� ����ȴ�.
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // ī���� ũ�⸦ ���δ�. (1���)
        DownscaleCard();
    }

    // ī���� ũ�⸦ Ű���. (1.2���)
    void UpscaleCard()
    {
        transform.localScale = new Vector2(1.2f, 1.2f);
    }

    // ī���� ũ�⸦ ���δ�. (1���)
    void DownscaleCard()
    {
        transform.localScale = new Vector2(1f, 1f);
    }
    #endregion ���콺 ����(Mouse Hover)

    #region ���콺 Ŭ�� �� �巡��
    // �巡�װ� ���۵� �� ȣ��ȴ�.
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ���� ī���� ��� ȭ��ǥ�� ǥ���Ѵ�.

        // ���� ī�尡 �ƴ� ���, �ƹ� �ϵ� ���� �ʴ´�.
    }

    // �巡�� ���� �� ��� ȣ��ȴ�.
    public void OnDrag(PointerEventData eventData)
    {
        // ���� ī���� ���, ȭ��ǥ�� ���� ���콺�� ���Ѵ�.
    }

    // ī�� �ߵ� �� ���õ� ������Ʈ
    public GameObject selectedObject;

    // �巡�װ� ���� �� ȣ��ȴ�.
    public void OnEndDrag(PointerEventData eventData)
    {
        // �⺻ ���̾��ũ�� Field�̸�
        LayerMask layer = LayerMask.GetMask("Field");

        // ���� ī���� ��� ���̾��ũ�� Enemy�� �����Ѵ�.
        if (cardData.type == EffectType.Attack)
        {
            layer = LayerMask.GetMask("Enemy");
        }

        // layer�� ��ġ�ϴ�, ���õ� ������Ʈ�� �����´�.
        selectedObject = GetClickedObject(layer);

        // ������Ʈ�� ���õ��� �ʾҴٸ�
        if (selectedObject == null)
        {
            // ī�� �ߵ��� ����Ѵ�.

            return;
        }

        // ī�带 �ߵ��Ѵ�. ���� ī���� ��� ���õ� ������ �ߵ��Ѵ�.
        CardInfo.instance.effects[(int)cardData.type](cardData.amount, selectedObject);
    }

    // Ŭ����(�巡�� �� ���콺�� �� ����) ������Ʈ�� �����´�.
    GameObject GetClickedObject(LayerMask layer)
    {
        // ���콺 ��ġ�� �޾ƿ´�.
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ���콺 ��ġ�� ����ĳ��Ʈ�� ���, layer�� ��ġ�ϴ� ������Ʈ �� ���� ���� �浹�� ������Ʈ�� ��ȯ�Ѵ�.
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f, layer);

        // layer�� ��ġ�ϴ� ������Ʈ�� ã�Ҵٸ�
        if(hit.collider != null)
        {
            // �ش� ������Ʈ�� ��ȯ�ϰ�
            return hit.transform.gameObject;
        }

        // �ƴ϶�� null�� ��ȯ�Ѵ�.
        return null;
    }
    #endregion ���콺 Ŭ�� �� �巡��

    #region ���� ����
    // ī���� ������ �����Ѵ�.
    public void UpdateCardInfo(CardData data)
    {
        if (data != null)
        {
            spriteRenderer.sprite = data.sprite;
            descriptionText.text = data.description;
            nameText.text = data.name;
            costText.text = data.cost.ToString();
        }
    }
    #endregion ���� ����

    #region ī�� ����
    public virtual void RemoveCard()
    {
        // ī�带 ������ ������.
    }
    #endregion ī�� ����
}
