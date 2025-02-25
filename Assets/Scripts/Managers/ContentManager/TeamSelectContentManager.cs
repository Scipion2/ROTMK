using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamSelectContentManager : MonoBehaviour
{
    
    [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI Header;
        [SerializeField] private TextMeshProUGUI Instructions;
        [SerializeField] private Button ConfirmButton,RandomButton;
        [SerializeField] private Transform Body;

    [Header("Monster Datas")]
        [SerializeField] InvokableDisplay[] MonstersSlot;
        [SerializeField] private InvokableDisplay InvokableDisplayPrefab;

    //SETTERS

        public void SetHeaderText(string HeaderText){Header.text=HeaderText;}//Set Header Text To Display
        public void SetInstructions(string InstructionsText){Instructions.text=InstructionsText;}//Set Instruction Text To Display
    
    //ESSENTIALS

        public void Start()
        {

            SetHeaderText("Joueur 1\nComposez votre équipe.");
            SetInstructions("Votre équipe doit contenir 3 monstres.\nChoisissez judicieusement.");
            SetConfirmButtonDisplay(false);

            MonstersSlot=new InvokableDisplay[MonsterManager.instance.MonsterList.Length];

            for(int i=0;i<MonsterManager.instance.MonsterList.Length;++i)
            {

                MonstersSlot[i]=Instantiate(InvokableDisplayPrefab,Body);
                MonstersSlot[i].transform.SetParent(Body,false);
                MonstersSlot[i].Initialize(MonsterManager.instance.MonsterList[i],i);

            }

        }//Initialize UI

    //DISPLAYERS

        public void SetConfirmButtonDisplay(bool isDisplayed)
        {

            ConfirmButton.gameObject.SetActive(isDisplayed);
            RandomButton.gameObject.SetActive(!isDisplayed);

        }//Display Confirmaton Button

        public void ResetButton()
        {

            for(int i=0;i<MonstersSlot.Length;++i)
            {

                MonstersSlot[i].Reset();

            }

        }//Reset Display


    //RANDOMIZER

        public void SelectRandomMembers()
        {

            for(;MonsterManager.instance.GetMonsterCount()<TeamManager.instance.GetTeamSize();)
            {

                int SlotNumber;
                for(SlotNumber=Random.Range(0,MonstersSlot.Length);MonstersSlot[SlotNumber].isItSelected();SlotNumber=Random.Range(0,MonstersSlot.Length)){}
                MonstersSlot[SlotNumber].OnClickInvokable();

            }

        }


    public static TeamSelectContentManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }//Enable to call instance of this class from everywhere

}
