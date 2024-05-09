using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChoiceType
{
    Up,
    Down
}

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;

    public GameObject UpSignLeft;            //�� ������ ���� ǥ��(����)
    public GameObject UpSignRight;           //�� ������ ���� ǥ��(������)
    public GameObject DownSignLeft;          //�Ʒ� ������ ���� ǥ��(����)
    public GameObject DownSignRight;         //�Ʒ� ������ ���� ǥ��(������)

    public GameObject ChoiceBoxUp;
    public GameObject ChoiceBoxDown;

    public GameObject ChoiceUp;
    public GameObject ChoiceDown;

    public GameObject Dialogue;

    private void Awake()
    {
        if(instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    public Choice currentChoice;

    void Start(){
        UpSignLeft.SetActive(false);
        UpSignRight.SetActive(false);
        DownSignLeft.SetActive(false);
        DownSignRight.SetActive(false);
    }

    void Update(){
        if(currentChoice != null){

            if(Choice.Selected != 0){
                ChoiceUp.SetActive(false);
                ChoiceDown.SetActive(false);
                Dialogue.SetActive(true);
            }
            
            if (currentChoice.name == "ChoiceBoxUp")
            {
                UpSignLeft.SetActive(true);
                UpSignRight.SetActive(true);
                DownSignLeft.SetActive(false);
                DownSignRight.SetActive(false);
            }
            else
            {
                UpSignLeft.SetActive(false);
                UpSignRight.SetActive(false);
                DownSignLeft.SetActive(true);
                DownSignRight.SetActive(true);
            }
        }
        
    }    
}
