// ���ö
using UnityEngine;
using System;
using System.Collections;

public class CardInfo : MonoBehaviour
{
    #region �̱���
    public static CardInfo Instance { get; set; }
    private static CardInfo instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }

        // ī�� ȿ���� ��������Ʈ�� ��� ���
        EnrollAllSkills();
        EnrollLayerDict();
    }
    #endregion �̱���

    #region ī�� UI ������
    // �����ϰ� �ʹ�...
    public Sprite[] skillIcons;
    public Sprite[] debuffIcons;
    #endregion ī�� UI ������

    #region ���� �˻�
    // ī�� Ÿ���� ������ ���̾ ����ִ� �迭
    private LayerMask[] layerDict;

    // �迭�� Ÿ�� - ���̾� ������ ����Ѵ�.
    private void EnrollLayerDict()
    {
        layerDict = new LayerMask[Enum.GetValues(typeof(SkillType)).Length];

        layerDict[(int)SkillType.Attack] = LayerMask.GetMask("Enemy");
        layerDict[(int)SkillType.Shield] = LayerMask.GetMask("Field");
        layerDict[(int)SkillType.Heal] = LayerMask.GetMask("Field");
        layerDict[(int)SkillType.Cleanse] = LayerMask.GetMask("Field");
        layerDict[(int)SkillType.RestoreCost] = LayerMask.GetMask("Field");
        layerDict[(int)SkillType.Draw] = LayerMask.GetMask("Field");
        layerDict[(int)SkillType.Buff] = LayerMask.GetMask("Field");
        layerDict[(int)SkillType.Debuff] = LayerMask.GetMask("Enemy");
        layerDict[(int)SkillType.Bleed] = LayerMask.GetMask("Enemy");
    }

    // Ÿ�Կ� �´� ���̾ ��ȯ�Ѵ�.
    public LayerMask ReturnLayer(SkillType type)
    {
        return layerDict[(int)type];
    }

    // Ÿ���� ��ȯ�Ѵ�.
    public Character[] GetTarget(SkillTarget target, Enemy selectedEnemy)
    {
        // �� ��쿡 �´� ���� ��ȯ�Ѵ�. �ٵ� ���� �迭�� ����� ��ȯ�ؼ�, ���߿� �����丵 �� �� �ؾ� �ڴ�.
        if (target == SkillTarget.Player)
        {
            return new Character[] { Player.Instance };
        }

        else if (target == SkillTarget.Enemy)
        {
            return new Character[] { selectedEnemy };
        }

        else if (target == SkillTarget.AllEnemy)
        {
            return BattleInfo.Inst.remainingEnemies.ToArray();
        }

        return null;
    }


    // Ÿ���� ��ȯ�Ѵ�. ��Ÿ�� ��ų�� �� ȣ�� �����ϴ�.
    public Character[] GetTarget(SkillTarget target)
    {
        // Ÿ���� �����Ǹ� null�� ��ȯ�Ѵ�.
        if (target == SkillTarget.Enemy)
        {
            return null;
        }

        // �� ��쿡 �´� ���� ��ȯ�Ѵ�. �ٵ� ���� �迭�� ����� ��ȯ�ؼ�, ���߿� �����丵 �� �� �ؾ� �ڴ�.
        else if (target == SkillTarget.Player)
        {
            return new Character[] { Player.Instance };
        }

        else if (target == SkillTarget.AllEnemy)
        {
            return BattleInfo.Inst.remainingEnemies.ToArray();
        }

        return null;
    }

    // ī�尡 Ÿ���� ī������ �˷��ش�.
    public bool IsTargetingCard(Skill[] data)
    {
        // ī�尡 Ÿ���� ��ų�� ���� �ִٸ� true�� �����Ѵ�.
        for(int i = 0; i < data.Length; ++i)
        {
            if (data[i].target == SkillTarget.Enemy)
            {
                return true;
            }
        }

        // �� �ܿ� false�� �����Ѵ�.
        return false;
    }

    public SkillTextInfo skillTexts;

    public SkillText GetSkillText(Skill skill)
    {
        SkillText skillText = new SkillText();

        // ��ų �̸��� �����Ѵ�.
        skillText.name = skillTexts.text[(int)skill.type].name;

        // ��ų ������ ȿ����, �� ���� �Բ� �����Ѵ�.
        if(skill.turnCount != 0)
        {
            // �� ���� 0�� �ƴ϶�� ���� �߰��Ѵ�.
            skillText.description = skill.turnCount + "�� �� ";
        }

        if (skill.amount != 0)
        {
            // ȿ������ 0�� �ƴ϶�� �߰��Ѵ�.
            skillText.description = skill.amount + "�� ";
        }
        // Ÿ�Կ� �´� ������ �߰��Ѵ�.
        skillText.description += skillTexts.text[(int)skill.type].description;

        return skillText;
    }
    #endregion ���� �˻�

    #region ī�� ȿ��
    // ī�� ȿ�� �Լ����� ��Ƶ� ��������Ʈ
    // amount�� ī���� ȿ����, turnCount�� ���ӵ� ���� ��, target�� ���� ����̴�.
    // ���� ��� Bleed�� ������ 6, 3, Player.Instance���, �÷��̾�� 3�ϰ�, �� �� 6�� �������� �ش�.
    public delegate void CardSkill(int amount, int turnCount, Character target);
    // ��������Ʈ �迭, EffectType�� �´� �Լ��� ��Ī�Ѵ�.
    public CardSkill[] effects;

    // ��� ȿ���� effects �迭�� ����Ѵ�.
    void EnrollAllSkills()
    {
        effects = new CardSkill[Enum.GetValues(typeof(SkillType)).Length];

        // ī�� ȿ���� �迭�� ���
        effects[(int)SkillType.Attack] += Attack;
        effects[(int)SkillType.Shield] += Shield;
        effects[(int)SkillType.Heal] += Heal;
        effects[(int)SkillType.Cleanse] += Cleanse;
        effects[(int)SkillType.RestoreCost] += RestoreCost;
        effects[(int)SkillType.Draw] += Draw;
        effects[(int)SkillType.Buff] += Buff;
        effects[(int)SkillType.Debuff] += Debuff;
        effects[(int)SkillType.Bleed] += Bleed;
    }


    // ��� Ÿ�ٿ��� ��ų�� ����Ѵ�.
    public void ActivateSkill(Skill skill, Character[] target)
    {
        for (int i = 0; i < target.Length; ++i)
        {
            // ��� Ÿ�ٿ��� skill�� ����Ѵ�.
            effects[(int)skill.type](skill.amount, skill.turnCount, target[i]);
        }
    }

    // target�� null�� ���� Card�� OnEndDrag���� �˻������Ƿ�, �˻����� �ʴ´�.
    public void Attack(int amount, int turnCount, Character target)
    {
        target.DecreaseHP(amount);
    }

    public void Shield(int amount, int turnCount, Character target)
    {
        Debug.Log("Shield");
    }

    public void Heal(int amount, int turnCount, Character target)
    {
        Debug.Log("Heal");
    }

    public void Cleanse(int amount, int turnCount, Character target)
    {
        Debug.Log("Cleanse");
    }

    public void RestoreCost(int amount, int turnCount, Character target)
    {
        Debug.Log("RestoreCost");
    }

    public void Draw(int amount, int turnCount, Character target)
    {
        Debug.Log("Draw");
    }

    public void Buff(int amount, int turnCount, Character target)
    {
        Debug.Log("Buff");
    }

    public void Debuff(int amount, int turnCount, Character target)
    {
        Debug.Log("Debuff");
    }

    public void Bleed(int amount, int turnCount, Character target)
    {
        target.EnrollBleed(new BleedEffect(SkillType.Bleed, amount, turnCount));
    }
    #endregion ī�� ȿ��
}
