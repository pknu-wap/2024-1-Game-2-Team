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
    private void Start()
    {
        inventoryPanel.SetActive(activeInventory); // �ʱ� �������� false�� �Ѵ�.
        slots = transform.GetChild(1).GetChild(0).GetChild(0).GetComponentsInChildren<TMP_Text>().ToList();
    }
    public void ToggleInventory()
    {
        activeInventory = !activeInventory; // Pointer Enter, Exit�� false -> true, true -> false�� �ٲ۴�
        inventoryPanel.SetActive(activeInventory); // activeInventory�� false���� true������ ���� Panel�� ������ ���� �����Ѵ�.
    }
    public static List<string> items = new List<string>();
    public List<TMP_Text> slots = new List<TMP_Text>();
}