// ���ö
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
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

    public virtual void RemoveCard()
    {
        // ī�带 ������ ������.
    }
}
