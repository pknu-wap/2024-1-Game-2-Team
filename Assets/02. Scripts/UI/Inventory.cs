using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    bool activeInventory = false;
    public List<TMP_Text> slots = new List<TMP_Text>();
    private void Start()
    {
        // ���� ���� �� �κ��丮 �� ���̰�
        inventoryPanel.SetActive(activeInventory);
        slots = transform.GetChild(1).GetChild(0).GetChild(0).GetComponentsInChildren<TMP_Text>().ToList();
    }
    public void ToggleInventory()
    {
        // Pointer Enter, Exit�� false -> true, true -> false�� �ٲ۴�
        activeInventory = !activeInventory;
        // activeInventory�� false���� true������ ���� Panel�� ������ ���� �����Ѵ�.
        inventoryPanel.SetActive(activeInventory); 
    }
}