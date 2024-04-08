// ���ö
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    #region ���콺 Ŭ��
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ���� ī���� ��� ȭ��ǥ�� ǥ���Ѵ�.

        // ���� ī�尡 �ƴ� ���, �ƹ� �ϵ� ���� �ʴ´�.
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ���� ī���� ���, ȭ��ǥ�� ���� ���콺�� ���Ѵ�.
    }

    // ī�� �ߵ� �� ���õ� ������Ʈ
    public GameObject selectedObject;

    public void OnEndDrag(PointerEventData eventData)
    {
        if (cardData.effect == EffectType.Attack)
        {
            // ���õ� �� ������Ʈ�� �����´�.
            selectedObject = GetClickedObject(LayerMask.GetMask("Enemy"));

            // �� ������Ʈ�� ���õ��� �ʾҴٸ�
            if (selectedObject == null)
            {
                // ī�� �ߵ��� ����Ѵ�.
            }

            // ���õ� ������ ������ �����Ѵ�.
            CardInfo.instance.effects[(int)cardData.effect](cardData.amount, selectedObject);

            Debug.Log(selectedObject);
        }

        else
        {
            // ���õ� �� ������Ʈ�� �����´�.
            selectedObject = GetClickedObject(LayerMask.GetMask("Field"));
            Debug.Log(selectedObject);

            if (selectedObject == null)
            {
                // ī�� �ߵ��� ����Ѵ�.
            }

            // ī�带 �ߵ��Ѵ�.
            CardInfo.instance.effects[(int)cardData.effect](cardData.amount, selectedObject);

            Debug.Log(selectedObject);
        }
    }

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
    #endregion ���콺 Ŭ��

    #region ���� ����
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

    #region ī�� ȿ��
    public virtual void ActivateCard()
    {
        CardInfo.instance.effects[(int)cardData.effect](cardData.amount, gameObject);
    }
    #endregion ī�� ȿ��

    #region ī�� ����
    public virtual void RemoveCard()
    {
        // ī�带 ������ ������.
    }
    #endregion ī�� ����
}
