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
    Debuff,         // �����
    Bleed,          // ����
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

    #region ���� �˻�
    private EnemyTarget[] targetDict;

    private void EnrollTargetDict()
    {
        targetDict = new EnemyTarget[Enum.GetValues(typeof(EnemySkillType)).Length];

        targetDict[(int)EnemySkillType.Attack] = EnemyTarget.Player;
        targetDict[(int)EnemySkillType.Shield] = EnemyTarget.Self;
        targetDict[(int)EnemySkillType.Heal] = EnemyTarget.Self;
        targetDict[(int)EnemySkillType.Cleanse] = EnemyTarget.Self;
        targetDict[(int)EnemySkillType.Buff] = EnemyTarget.Self;
        targetDict[(int)EnemySkillType.Debuff] = EnemyTarget.Player;
        targetDict[(int)EnemySkillType.Bleed] = EnemyTarget.Player;
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
    public delegate void EnemySkillEffects(int amount, int turnCount, Character target);
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
        effects[(int)EnemySkillType.Bleed] += Bleed;
    }

    // target�� null�� ���� Card�� OnEndDrag���� �˻������Ƿ�, �˻����� �ʴ´�.
    // {target}���� {amount}��ŭ �������� ������.
    public void Attack(int amount, int turnCount, Character target)
    {
        target.DecreaseHP(amount);
    }

    // {Shield}��ŭ ��ȣ���� ��´�. ��ȣ���� ü�� ��� �Ҹ�ȴ�.
    public void Shield(int amount, int turnCount, Character target)
    {
        Debug.Log("Shield");
    }

    // {amount}��ŭ ü���� ȸ���Ѵ�.
    public void Heal(int amount, int turnCount, Character target)
    {
        Debug.Log("Heal");
    }

    // ��� ������� ��ȭ�Ѵ�.
    public void Cleanse(int amount, int turnCount, Character target)
    {
        Debug.Log("Cleanse");
    }

    // �ڽ�Ʈ�� {amount} ȸ���Ѵ�.
    public void RestoreCost(int amount, int turnCount, Character target)
    {
        Debug.Log("RestoreCost");
    }

    // ī�带 {amount} �� �̴´�.
    public void Draw(int amount, int turnCount, Character target)
    {
        Debug.Log("Draw");
    }

    // ����(���� ����)
    public void Buff(int amount, int turnCount, Character target)
    {
        Debug.Log("Buff");
    }

    // �����(���� ����)
    public void Debuff(int amount, int turnCount, Character target)
    {
        Debug.Log("Debuff");
    }

    // {turnCount} �� �� {amount}�� �������� ������.
    public void Bleed(int amount, int turnCount, Character target)
    {
        target.EnrollBleed(new BleedEffect(amount, turnCount));
    }
    #endregion ī�� ȿ��
}
