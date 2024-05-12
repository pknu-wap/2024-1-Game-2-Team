using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    // ������ � �߰� ��ǥ
    public Transform middleTr;

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
            // ������ � ��, �ڽ��� ��ġ�� ã�� �̵��Ѵ�.
            points[i].transform.position = GetBezierLerp(transform.position, middleTr.position, targetPosition, (float)i / numberOfPoints);

            if(i == 0)
            {
                continue;
            }

            // ������ �ڱ� �ڽ��� ��ġ - ���� ����Ʈ�� ��ġ�� �����Ѵ�.
            Vector2 delta = points[i].transform.position - points[i - 1].transform.position;
            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

            points[i].transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }

    public void HideArrow()
    {
        gameObject.SetActive(false);
    }

    Vector2 GetBezierLerp(Vector2 start, Vector2 middle, Vector2 end, float t)
    {
        float oneMinusT = 1f - t;
        return oneMinusT * oneMinusT * start
            + 2f * oneMinusT * t * middle
            + t * t * end;
    }
    #endregion ȭ��ǥ ����
}
