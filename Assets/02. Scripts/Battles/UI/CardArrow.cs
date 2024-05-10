using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArrow : MonoBehaviour
{
    #region �̱���
    public static CardArrow Instance { get; private set; }
    void Awake() => Instance = this;
    #endregion �̱���

    #region ȭ��ǥ ����
    public GameObject[] points;

    [Header("ȭ��ǥ ����")]
    // ȭ��ǥ �� ���� ����
    public int numberOfPoints;
    // ���� ����
    public float space;
    // ���� ������
    public GameObject point;
    // ���κ� ������
    public GameObject arrow;

    // �θ� ȭ��ǥ ������Ʈ, ���� ��ġ�� transform�� �������� �Ѵ�.

    public void Start()
    {
        points = new GameObject[numberOfPoints + 1];

        // ������ numberOfPoints �� �����ϰ�
        for(int i = 0; i < numberOfPoints; ++i)
        {
            points[i] = Instantiate(point, transform);
        }

        // ���κе� �����Ѵ�.
        points[numberOfPoints] = Instantiate(arrow, transform);

        // ���� ������ ���� ���´�.
        HideArrow();
    }

    public void ShowArrow()
    {
        gameObject.SetActive(true);
    }

    public void MoveArrow(Vector2 targetPosition)
    {
        for(int i = 0; i < numberOfPoints + 1; ++i)
        {
            points[i].transform.position = Vector2.Lerp(transform.position, targetPosition, (float)i / numberOfPoints);
        }
    }

    public void HideArrow()
    {
        gameObject.SetActive(false);
    }
    #endregion ȭ��ǥ ����
}
