using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();
    // TMP_Text slot;
    public List<TMP_Text> slots = new List<TMP_Text>();
    void Start()
    {
        items.Add("�����");
        items.Add("����");
        items.Add("�ٷ�");
        items.Add("�ӵ�");
        items.Add("��");
        items.Add("���");
        // slot = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        slots = transform.GetChild(1).GetChild(0).GetChild(0).GetComponentsInChildren<TMP_Text>().ToList();
    }

    void Update()
    {
        
    }
    public void AddItem(string item)
    {
        items.Add(item);
    }
    public void ShowItems()
    {
        // TMP_Texts.text = items[0];
        int i = 0;
        for (; i < items.Count; i++)
        {
            slots[i].text = items[i];
            slots[i].gameObject.SetActive(true);
        }
        for (; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
}
