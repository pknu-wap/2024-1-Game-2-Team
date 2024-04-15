using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum EnemySkillType
{
    Attack,         // ����
    Shield,         // �ǵ� ����
    Heal,           // ü�� ȸ��
    Cleanse,        // ����� ����
    Buff,           // ����
    Debuff          // �����
};

public enum EnemyTarget
{
    Player, // �÷��̾�
    Self    // �ڱ� �ڽ�
};

public class EnemySkillInfo : MonoBehaviour
{
    #region �̱���
    public static EnemySkillInfo Instance { get; set; }
    private static EnemySkillInfo instance;

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

        // Ÿ�Ժ� Ÿ���� �迭�� ���
        EnrollTargetDict();
        // ī�� ȿ���� ��������Ʈ �迭�� ��� ���
        EnrollAllEffects();
    }
    #endregion �̱���

    private EnemyTarget[] targetDict;

    #region ���� �˻�
    private void EnrollTargetDict()
    {
        targetDict = new EnemyTarget[Enum.GetValues(typeof(EnemySkillType)).Length];

        targetDict[(int)EnemySkillType.Attack] = EnemyTarget.Player;
        targetDict[(int)EnemySkillType.Shield] = EnemyTarget.Self;
        targetDict[(int)EnemySkillType.Heal] = EnemyTarget.Self;
        targetDict[(int)EnemySkillType.Cleanse] = EnemyTarget.Self;
        targetDict[(int)EnemySkillType.Buff] = EnemyTarget.Self;
        targetDict[(int)EnemySkillType.Debuff] = EnemyTarget.Player;
    }

    public Character ReturnTarget(EnemySkillType type, Character caller)
    {
        if (targetDict[(int)type] == EnemyTarget.Player)
        {
            return Player.Instance;
        }
        else
        {
            return caller;
        }
    }
    #endregion ���� �˻�

    #region ��ų ȿ��
    // ī�� ȿ�� �Լ����� ��Ƶ� ��������Ʈ
    public delegate void EnemySkillEffects(int amount, Character target);
    // ��������Ʈ �迭, EnemySkillType�� �´� �Լ��� ��Ī�Ѵ�.
    public EnemySkillEffects[] effects;

    // ��� ����
    // EnemySkillInfo.Instance.effects[(int)ȿ�� ����](ȿ����, ���);
    // EnemySkillInfo.Instance.effects[(int)EnemySkillType.Buff](5, target);

    // ��� ȿ���� effects �迭�� ����Ѵ�.
    void EnrollAllEffects()
    {
        effects = new EnemySkillEffects[Enum.GetValues(typeof(EnemySkillType)).Length];

        // ī�� ȿ���� �迭�� ���
        effects[(int)EnemySkillType.Attack] += Attack;
        effects[(int)EnemySkillType.Shield] += Shield;
        effects[(int)EnemySkillType.Heal] += Heal;
        effects[(int)EnemySkillType.Cleanse] += Cleanse;
        effects[(int)EnemySkillType.Buff] += Buff;
        effects[(int)EnemySkillType.Debuff] += Debuff;
    }

    // target�� null�� ���� Card�� OnEndDrag���� �˻������Ƿ�, �˻����� �ʴ´�.
    public void Attack(int amount, Character target)
    {
        target.DecreaseHP(amount);
        Debug.Log(target.GetHP());
    }

    public void Shield(int amount, Character target)
    {
        Debug.Log("Shield");
    }

    public void Heal(int amount, Character target)
    {
        Debug.Log("Heal");
    }

    public void Cleanse(int amount, Character target)
    {
        Debug.Log("Cleanse");
    }

    public void RestoreCost(int amount, Character target)
    {
        Debug.Log("RestoreCost");
    }

    public void Draw(int amount, Character target)
    {
        Debug.Log("Draw");
    }

    public void Buff(int amount, Character target)
    {
        Debug.Log("Buff");
    }

    public void Debuff(int amount, Character target)
    {
        Debug.Log("Debuff");
    }
    #endregion ī�� ȿ��
}
