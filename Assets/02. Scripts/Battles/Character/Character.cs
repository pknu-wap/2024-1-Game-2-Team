using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("������")]
    // HP(ü��)
    protected int hp;

    [Header("������Ʈ")]
    protected int a;

    // �� ������Ʈ�� hp�� ��ȯ�Ѵ�.
    public int GetHP()
    {
        return hp;
    }

    // �� ������Ʈ�� hp�� ���ҽ�Ų��.
    public void DecreaseHP(int damage)
    {
        // hp�� damage��ŭ ���ҽ�Ų��.
        hp -= damage;

        // hp�� 0 ���ϰ� �� ���
        if (hp <= 0)
        {
            // ���� �̺�Ʈ ����
            Die();
        }
    }

    public void Die()
    {
        // ������ ���õ� ȿ�� ó��
        // ���� �ִϸ��̼�
        // ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
        // Battle Info�� ���� �� -1
    }
}
