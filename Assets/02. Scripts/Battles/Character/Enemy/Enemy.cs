// ���ö
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // ���� ����� ��ų ����Ʈ
    public GameObject[] skills;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DecreaseHP(6);
        }
    }

    public override void Die()
    {
        base.Die();

        // ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
        // Battle Info�� ���� �� -1
    }
}
