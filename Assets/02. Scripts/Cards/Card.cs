using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static UnityEditor.Progress;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer illust;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text costTMP;
    [SerializeField] TMP_Text descriptionTMP;
    [SerializeField] Sprite cardBackground;

    public Item item;
    public PRS originPRS;

    // DOTween 시퀀스
    Sequence moveSequence;
    Sequence disappearSequence;

    public void Setup(Item item)
    {
        this.item = item;

        illust.sprite = this.item.sprite;
        nameTMP.text = this.item.name;
        costTMP.text = this.item.cost.ToString();
        descriptionTMP.text = this.item.description;
    }

    void OnMouseEnter()
    {
        CardManager.Inst.CardMouseEnter(this);
    }

    void OnMouseDown()
    {
        CardManager.Inst.CardMouseDown();
    }

    void OnMouseExit()
    {
        CardManager.Inst.CardMouseExit(this);
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            // moveSequence에 위치, 회전, 스케일을 조정하는 DOTween을 연결했다.
            moveSequence = DOTween.Sequence()
                .Append(transform.DOMove(prs.pos, dotweenTime))
                .Join(transform.DORotateQuaternion(prs.rot, dotweenTime))
                .Join(transform.DOScale(prs.scale, dotweenTime));
        }

        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    public void SlowDisappear()
    {
        disappearSequence = DOTween.Sequence()
            .Append(transform.DOLocalMoveY(10, 0.5f).SetEase(Ease.OutQuart))
            .AppendInterval(1f);
        //.Join(card.DoFade())
    }
}
