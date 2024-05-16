// ���ö
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    [Header("����")]
    // ���� ����� ��ų ����Ʈ
    public SkillData skillData;

    [Header("������Ʈ")]
    // �ൿ ���� ������
    [SerializeField] protected Image behaviorIcon;
    [SerializeField] protected TMP_Text behaviorAmount;

    // ��������â
    [SerializeField] protected TMP_Text behaviorName;
    [SerializeField] protected TMP_Text behaviorDescription;

    public override void Awake()
    {
        base.Awake();

        // �ൿ ���� ������
        behaviorIcon = transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>();
        behaviorAmount = behaviorIcon.transform.GetChild(0).GetComponent<TMP_Text>();

        // ��������â
        behaviorName = statusPanel.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        behaviorDescription = statusPanel.GetChild(0).GetChild(1).GetComponent<TMP_Text>();
    }

    protected override void Start()
    {
        base.Start();

        // �׽�Ʈ�� Start�� ����
        ReadySkill();

        // BattleInfo�� �ڽ� �߰�
        BattleInfo.Inst.IncreaseEnemyCount();

        // BattleManager�� �̺�Ʈ ���
        TurnManager.Inst.onEndEnemyTurn.AddListener(EndEnemyTurn);
        TurnManager.Inst.onStartPlayerTurn.AddListener(ReadySkill);
    }

    public void StartEnemyTurn()
    {
        ReadySkill();
    }

    public void EndEnemyTurn()
    {
        // �÷��̾�� ��ų�� ����Ѵ�. �̶�, �ִϸ��̼��� ��� ������ ���� ��ɵ��� �����Ѵ�.
        CastSkill();

        // �����(���� ��)�� ���� ����ȴ�.
        GetBleedAll();
    }

    [Header("��Ÿ�� ����")]
    // ���� �غ� ���� ��ų
    public Skill currentSkill;

    // ��ų ����� �غ��Ѵ�.
    public void ReadySkill()
    {
        int i = Random.Range(0, skillData.skills.Length);

        // 1. ������ ��ų�� �� �ϳ��� �����Ѵ�.
        currentSkill = skillData.skills[i];

        // 2.UI�� �����Ѵ�.
        // 2-1. �ڽ��� �� ��ų�� ü�¹� ���� ǥ���Ѵ�.
        behaviorIcon.sprite = CardInfo.Instance.skillIcons[(int)currentSkill.type];
        behaviorAmount.text = currentSkill.amount.ToString();

        // 2-2. ������â�� ��ų�� �������� �����Ѵ�.
        SkillText skillText = CardInfo.Instance.GetSkillText(currentSkill);
        behaviorName.text = skillText.name;
        behaviorDescription.text = skillText.description;
    }


    // Ÿ���� ��ȯ�Ѵ�.
    public Character GetTarget(SkillTarget target, Character caller)
    {
        if (target == SkillTarget.Player)
        {
            return Player.Instance;
        }

        else if (target == SkillTarget.Enemy)
        {
            return this;
        }

        return null;
    }

    // ��ų�� ����Ѵ�.
    public void CastSkill()
    {
        // Ÿ���� �޾ƿ´�.
        Character target = GetTarget(currentSkill.target, this);

        // �غ��� ��ų�� ����Ѵ�.
        CardInfo.Instance.ActivateSkill(currentSkill, target);
    }

    // �״´�.
    public override void Die()
    {
        // ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
        // BattleManager���� �ڱ� �ڽ� ����
        TurnManager.Inst.onEndEnemyTurn.RemoveListener(EndEnemyTurn);
        // Battle Info�� ���� �� -1
        BattleInfo.Inst.DecreaseEnemyCount();
    }
}
