using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardInfoUpdater : MonoBehaviour
{
    #region ����
    [Header("ī�� ����")]
    public CardData cardData;

    [Header("ī�� UI")]
    private SpriteRenderer spriteRenderer;
    private TMP_Text descriptionText;
    private TMP_Text nameText;
    private TMP_Text costText;
    #endregion ����

    #region ����������Ŭ
    private void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        nameText = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        descriptionText = transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        costText = transform.GetChild(3).GetChild(0).GetComponent<TMP_Text>();

        UpdateCardInfo(cardData);
    }
    #endregion ����������Ŭ

    #region ���� ����
    // ī���� ������ �����Ѵ�.
    public void UpdateCardInfo(CardData data)
    {
        if (data != null)
        {
            spriteRenderer.sprite = data.sprite;
            nameText.text = data.name;
            descriptionText.text = data.description;
            costText.text = data.cost.ToString();
        }
    }
    #endregion ���� ����
}
