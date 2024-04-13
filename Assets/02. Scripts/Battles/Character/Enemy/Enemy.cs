// ���ö
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    [Header("����")]
    // ���� ����� ��ų ����Ʈ
    public SkillData skillData;

    [Header("������Ʈ")]
    public Image behaviorIcon;
    public TMP_Text behaviorAmount;

    public override void Awake()
    {
        base.Awake();

        behaviorIcon = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Image>();
        behaviorAmount = transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DecreaseHP(6);
        }
    }

    [Header("��Ÿ�� ����")]
    // ���� �غ� ���� ��ų
    public Skill currentSkill;

    // ��ų ����� �غ��Ѵ�.
    public void ReadySkill()
    {
        // 1. ������ ��ų�� �� �ϳ��� �����Ѵ�.
        currentSkill = skillData.skills[0];

        // 2.UI�� �����Ѵ�.
        // 2-1. �ڽ��� �� ��ų�� ü�¹� ���� ǥ���Ѵ�.
        // 2-2. ������â�� ��ų�� �������� �����Ѵ�.
    }

    // ��ų�� ����Ѵ�.
    public void CastSkill()
    {
        GameObject target;

        // ���� ��ų�̸� Player��, �� �ܴ� �ڽ��� Ÿ������ �Ѵ�.
        if (currentSkill.type == EffectType.Attack)
        {
            target = Player.Instance.gameObject;
        }
        else
        {
            target = gameObject;
        }

        // �غ��� ��ų�� ����Ѵ�.
        CardInfo.Instance.effects[(int)currentSkill.type](currentSkill.amount, target);
    }

    // �״´�.
    public override void Die()
    {
        base.Die();

        // ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
        // Battle Info�� ���� �� -1
    }
}
