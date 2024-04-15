using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject statePanel;
    [SerializeField] Image statePanelImage;

    private void Awake()
    {
        statePanel = transform.parent.GetChild(1).gameObject;
        statePanelImage = statePanel.GetComponent<Image>();

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

        statePanel.SetActive(false);
    }
}
