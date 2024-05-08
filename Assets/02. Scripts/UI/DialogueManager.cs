using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.IO;
using TMPro;
using System.Collections.Generic;


public class DialogueManager : MonoBehaviour, IPointerDownHandler
{

    public TMP_Text dialogueText;        //Story Text
    public TMP_Text dialogueName;        //Story Name

    public TMP_Text ChoiceUpText;        //Up Selection Text
    public TMP_Text ChoiceDownText;      //Down Selection Text

    public TMP_Text ChoiceUpRequireText;       //Up Selection Text
    public TMP_Text ChoiceDownRequireText;     //Down Selection Text

    public GameObject dialogueBox;       //?���? Canvas

    public GameObject Dialogue;          //????���?

    public GameObject ChoiceUp;          //?�� ?��?���? ?��?��
    public GameObject ChoiceDown;         //?��?�� ?��?���? ?��?��

    public GameObject WaitCursor;        //?��?�� Text ???�? ?��?�� 커서
      
    public Image dialogueImage;          //?��?��?��?��
          
    public Sprite[] dialogueImages;      //?��?��?��?�� 목록
      
    public string[] StoryText;           //Story Text 배열
    public string[] StoryName;           //Story Name 배열
      
    public int currentLine;              //?��?�� 출력 중인 문자?�� ?���?
      
    private bool isTyping = false;       //????��?�� ?���? 진행 ?���? ?��?�� �??��
    private bool cancelTyping = false;   //

    void Start()
    {
        dialogueBox.SetActive(false);    //?��?�� ?�� Canvas ?���? 비활?��?��
        ChoiceUp.SetActive(false);      //?��?�� ?�� ?��?���? 비활?��?��
        ChoiceDown.SetActive(false);    
        LoadDialogue();                  //Story Name,Text 불러?���?
        ShowDialogue();                  //?��미�????? ?���? Canvas ?��?��
    }

    void Update()
    {
   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       NextDialogue();
    }

    public void NextDialogue(){
        var IllustTable = new Dictionary<string,int>()
        {
            {"???",0},
            {"����",1}   
        };

        if(isTyping && !cancelTyping)
        {
            cancelTyping = true;
            return;
        }

        currentLine++;

        if (currentLine >= StoryText.Length)
        {
            dialogueBox.SetActive(false);
            return;
        }

        dialogueName.text = StoryName[currentLine];

        if(StoryName[currentLine][0] == '*'){
            Selection();
        }
        else
        {
            // ?��미�?? �?�?
            dialogueImage.sprite = dialogueImages[IllustTable[dialogueName.text]];
            StartCoroutine(TypeSentence(StoryText[currentLine]));
        }

    }

    //Story Text?�� ????��?�� ?���? 추�???��?�� ?��?��
    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        WaitCursor.SetActive(false);
        cancelTyping = false;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
            if (cancelTyping)
            {
                dialogueText.text = sentence;
                break;
            }
        }
        isTyping = false;
        WaitCursor.SetActive(true);
        cancelTyping = false;
    }

    void Selection()
    {
        dialogueText.text = "....";
        dialogueName.text = "";
        Dialogue.SetActive(false);
        ChoiceUp.SetActive(true);
        ChoiceDown.SetActive(true);
        ChoiceUpText.text = StoryName[currentLine].Substring(1);
        ChoiceDownText.text = StoryText[currentLine].Substring(1);
        if(Items.items.Find(x => x == "��") != null){
        
        ChoiceDownRequireText.text = "�ʿ��� ������ : <color=red>��</color>";
        
        }
        else{

        ChoiceDownRequireText.text = "�ʿ��� ������ : ��";

        }
    }

    //초기 ?���? ?��?��
    public void ShowDialogue()
    {
        currentLine = -1;
        dialogueBox.SetActive(true);
        // ?��미�?? 초기?��
        dialogueImage.sprite = dialogueImages[0];
    }

    //TXT ?��?��?��?�� Story Text, Name 불러?��?�� ?��?��
    void LoadDialogue()
    {
        //?��?�� ????�� 경로
        TextAsset asset = Resources.Load ("StoryScript")as TextAsset;

        string Story = asset.text;

        StringReader reader = new StringReader(Story);

        string fileContent = reader.ReadToEnd();

        StoryText = fileContent.Split('\n');

        StoryName = new string[StoryText.Length];

        for (int i = 0; i < StoryText.Length; i++)
        {
            string[] Temp = StoryText[i].Split('#');
            StoryText[i] = Temp[1]; // ????�� 문장 ????��
            StoryName[i] = Temp[0]; // ????�� ?���? ????��
        }

        reader.Close();
    }
}
