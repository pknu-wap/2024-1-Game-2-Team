using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Items : MonoBehaviour
{
    public static Items Inst {get; private set;}
    private void Awake() => Inst = this;

    public static List<string> items = new List<string>();
    public List<TMP_Text> slots = new List<TMP_Text>();
    void Start()
    {
        slots = transform.GetChild(1).GetChild(0).GetChild(0).GetComponentsInChildren<TMP_Text>().ToList();
        items.Add("�����");
        items.Add("����");
        items.Add("�ٷ�");
        items.Add("�ӵ�");
        items.Add("��");
        items.Add("����");
        items.Add("�丶�� ������");
        items.Add("��ġ ������");
        items.Add("ö��");
        items.Add("å");
        items.Add("�ش�");
        items.Add("������");
        items.Add("����");
        items.Add("����");
        items.Add("�ֻ��� �Ļ�");
        items.Add("�õ� ����");
        items.Add("���� �Ƿ�");
        items.Add("��ü��");
        items.Add("��");
        items.Add("�Ѿ�");
        items.Add("�����");
        foreach (TMP_Text slot in slots)
        {
            slot.text = "";
        }
    }
    public void AddItem()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!string.IsNullOrEmpty(slots[i].text))
                continue;
            string randomItem = AddAndRemoveItem();
            if (randomItem == null)
                break;
            slots[i].text = randomItem;
            break;
        }
    }
    private string AddAndRemoveItem()
    {
        if (items.Count == 0)
            return null;
        int randomIndex = Random.Range(0, items.Count);
        string randomItem = items[randomIndex];
        items.RemoveAt(randomIndex);
        return randomItem;
    }
}
