using UnityEngine;
using UnityEngine.UI;

public class GameContentManager : MonoBehaviour
{

    [Header("UI Components")]
        [SerializeField] private Transform GameBarPos;
        [SerializeField] private Header header;
        [SerializeField] private PlayerTurn PlayerActionDisplay;
        [SerializeField] private InvokableDisplay[] Player1Slots,Player2Slots;
        [SerializeField] private TeamSpawn TeamInit;
    

    //GETTERS
        public Transform GetGameBarTransform(){return GameBarPos;}//Getter for GameBarPos
        public PlayerTurn GetPlayerActionDisplay(){return PlayerActionDisplay;}//Getter for PlayerActionDisplay
        public TeamSpawn GetTeamInit(){return TeamInit;}//Getter for TeamInit
        public InvokableDisplay GetTeamSlot(int Team,int Slot){if(Team==1)return Player1Slots[Slot]; else return Player2Slots[Slot];}//Getter For InvokableDisplay


    //EVENTS

        public void OnEnable()
        {

            for(int i=0;i<TeamManager.instance.GetTeamSize();++i)
            {

                Player1Slots[i].Initialize(TeamManager.instance.GetTeamMonster(1,i),i);
                Player1Slots[i].GameVersion();
                Player2Slots[i].Initialize(TeamManager.instance.GetTeamMonster(2,i),i);
                Player2Slots[i].GameVersion();
                Player2Slots[i].Flip();

            }

            TeamInit.gameObject.SetActive(true);
            header.ShowEndTurn(false);

            
            SetTeamSelectableSlots(TurnManager.instance.GetPlayerTurn());
            UIManager.instance.LaunchSpawn();

        }//Initialize UI

    //DISPLAYERS

        public void HideInit()
        {

            TeamInit.gameObject.SetActive(false);

        }//Hide Init Phase UI

        public void ShowActionDisplay()
        {

            PlayerActionDisplay.gameObject.SetActive(true);

        }//Display Action UI

        public void HideActionDisplay()
        {

            PlayerActionDisplay.gameObject.SetActive(false);

        }//Hide Aciton UI

    //STEP SETTUP

        public void SetTeamSelectableSlots(int PlayerTurn)
        {

            bool Team;

            if(PlayerTurn==1)
                Team=true;
            else if(PlayerTurn==2)
                Team=false;
            else
            {

                Debug.Log("Player Turn Error");
                return;

            }

            for(int i=0;i<TeamManager.instance.GetTeamSize();++i)
            {

                Player1Slots[i].SetSelectable(Team);
                Player1Slots[i].Reset();
                Player2Slots[i].SetSelectable(!Team);
                Player2Slots[i].Reset();
            }

        }//Set Selectable The Active Team's Slots

        


        public void IncrementTurnCount()
        {

            header.IncrementTurnCount();

        }//Get +1 On Turn Count

        public void IsGameLaunched()
        {

            for(int i=0;i<TeamManager.instance.GetTeamSize();++i)
            {

                Player1Slots[i].GameLaunched();
                Player2Slots[i].GameLaunched();

            }

            header.ShowEndTurn(true);
            ShowActionDisplay();
            HideInit();

        }//Set The Playing Step

    //MONSTER

        public void SpawnMonster()
        {

            GameManager.instance.SpawnMonster();

        }//Spawn a Monster
    
    
    public static GameContentManager instance;
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
