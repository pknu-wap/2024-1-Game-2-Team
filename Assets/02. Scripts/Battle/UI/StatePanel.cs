using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject statePanel;
    [SerializeField] Image statePanelImage;
    [SerializeField] protected Canvas uiCanvas;

    private void Awake()
    {
        statePanel = transform.parent.GetChild(2).gameObject;
        statePanelImage = statePanel.GetComponent<Image>();
        uiCanvas = transform.parent.GetComponent<Canvas>();

        statePanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //// DOTween�� �Ἥ Fade ȿ���� �ִ� ���. ���� �� ��
        //statePanelImage.color = Color.clear;
        //statePanel.SetActive(true);

        //// ���콺�� �ø��� ����â�� ����.
        //statePanelImage.DOFade(1, duration)
        //    .OnComplete(() => statePanel.SetActive(true));

        // ������ ĵ������ order�� �� ������ �����ϰ�
        uiCanvas.sortingOrder = 10;
        // �г��� Ȱ��ȭ ��Ų��.
        statePanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //// DOTween�� �Ἥ Fade ȿ���� �ִ� ���. ���� �� ��
        //statePanelImage.color = Color.white;
        //statePanel.SetActive(true);

        //// ���콺�� ������ ����â�� �ݴ´�.
        //statePanelImage.DOFade(0, duration)
        //    .OnComplete(() => statePanel.SetActive(false));

        // ������ ĵ������ order�� �������� �����ϰ�
        uiCanvas.sortingOrder = 0;
        // �г��� ��Ȱ��ȭ ��Ų��.
        statePanel.SetActive(false);
    }
}
