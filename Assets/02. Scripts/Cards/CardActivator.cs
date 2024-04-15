using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardActivator : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("ī�� ����")]
    public CardData cardData;

    Vector3 originScale;
    Vector3 largeScale;

    private void Awake()
    {
        originScale = transform.localScale;
        largeScale = 1.2f * transform.localScale;
    }

    #region ���콺 ����(Mouse Hover)
    void OnMouseEnter()
    {
        // ī���� ũ�⸦ Ű���. (1.2���)
        UpscaleCard();
    }

    void OnMouseExit()
    {
        // ī���� ũ�⸦ ���δ�. (1���)
        DownscaleCard();
    }

    // ī���� ũ�⸦ Ű���. (1.2���)
    void UpscaleCard()
    {
        transform.localScale = largeScale;
    }

    // ī���� ũ�⸦ ���δ�. (1���)
    void DownscaleCard()
    {
        transform.localScale = originScale;
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

    // �巡�װ� ���� �� ȣ��ȴ�.
    public void OnEndDrag(PointerEventData eventData)
    {
        // ī�带 ����Ѵ�.
        UseCard();
    }
    #endregion ���콺 Ŭ�� �� �巡��

    #region ī�� ���

    // ī�� �ߵ� �� ���õ� ������Ʈ
    public Character selectedCharacter;

    // ī�带 ����Ѵ�.
    private void UseCard()
    {
        // ī�� ������ ���� Enemy �Ǵ� Field ���̾ �����Ѵ�.
        LayerMask layer = CardInfo.Instance.ReturnLayer(cardData.type);

        // layer�� ��ġ�ϴ�, ���õ� ������Ʈ�� �����´�.
        GameObject selectedObject = GetClickedObject(layer);

        // ������Ʈ�� ���õ��� �ʾҴٸ�
        if (selectedObject == null)
        {
            // ī�� �ߵ��� ����Ѵ�.

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
        CardInfo.Instance.effects[(int)cardData.type](cardData.amount, selectedCharacter);
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

    #region ī�� ����
    public virtual void RemoveCard()
    {
        // ī�带 ������ ������.
    }
    #endregion ī�� ����
}
