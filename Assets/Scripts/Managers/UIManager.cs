using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Team Setup in Game Variables")]
    [Space(10)]
        [SerializeField] private TeamSpawn Init;

    [Header("In Game Display Variables")]
    [Space(10)]
        [SerializeField] private bool Player1Turn=true;
        [SerializeField] private TurnAnnouncement TurnAnnouncementDisplayPrefab;
        [SerializeField] private PlayerTurn PlayerTurnDisplay=null;

    //SETTERS

        public void SetPlayerTurnDisplay(PlayerTurn SRC){PlayerTurnDisplay=SRC;}//Setter For PlayerTurnDisplay

        public void UpdateActionPoint()
        {

            PlayerTurnDisplay.UpdateActionPoint();

        }//Update the action point value display

        public void DisplayActionButton(bool isMoveDisplayable,bool isBaseActionDisplayable,bool isSpecialActionDisplayable)
        {

            PlayerTurnDisplay.DisplayMove(isMoveDisplayable);
            PlayerTurnDisplay.DisplayBaseACtion(isBaseActionDisplayable,TeamManager.instance.GetActiveMonster().GetBaseName());
            PlayerTurnDisplay.DisplaySpecialAction(isSpecialActionDisplayable,TeamManager.instance.GetActiveMonster().GetSpecialName());

        }//Display action buttons if enabled for the current monster

        public void DisplaySelectMonster(string MonsterName,GameManager.GameState ActualGameState)
        {

            switch(ActualGameState)
            {

                case GameManager.GameState.init:
                    GameContentManager.instance.GetTeamInit().SetMonsterNameDisplay(MonsterName);
                    break;

                case GameManager.GameState.game:
                    PlayerTurnDisplay.DisplayMonster(MonsterName);
                    break;

                case GameManager.GameState.action:
                    break;

                default:
                    break;

            }

        }//Display the monster selected on ATH

        public void DisplaySelectTile(Vector2 TilePos,GameManager.GameState ActualGameState)
        {

            switch(ActualGameState)
            {

                case GameManager.GameState.init:
                    GameContentManager.instance.GetTeamInit().SetTileSelectDisplay((int)TilePos.x+1,(int)TilePos.y+1);
                    break;

                case GameManager.GameState.game:
                    GameContentManager.instance.GetPlayerActionDisplay().DisplaySelectTile(TilePos);
                    if((int)TilePos.x==-1 || (int)TilePos.y==-1)
                    {

                        GameContentManager.instance.GetPlayerActionDisplay().DisplayConfirmButton(false);

                    }else
                    {

                        GameContentManager.instance.GetPlayerActionDisplay().DisplayConfirmButton(true);

                    }
                    break;

                case GameManager.GameState.action:
                    break;

                default:
                    break;

            }

        }//Display the tile selected on ATH

        public void ResetDisplaySelection(GameManager.GameState gameState)
        {

            switch (gameState)
            {

                case GameManager.GameState.init:
                    GameContentManager.instance.GetTeamInit().SwitchDisplayButton(false);
                    break;

                default :
                    break;

            }

        }

       public void LaunchSpawn()
       {

            Init=GameContentManager.instance.GetTeamInit();
            Init.SwitchDisplayButton(false);
            

       }//Set Spawn Step UI

        public void LaunchGame()
        {

            PlayerTurnDisplay=GameContentManager.instance.GetPlayerActionDisplay();
            PlayerTurnDisplay.gameObject.SetActive(true);

        }//Display the game ATH

        public void SetPlayer1Turn()
        {

            Player1Turn=true;
            DisplayTurn("Tour du joueur 1");


        }//Display Player 1 turn window

        public void SetPlayer2Turn()
        {


            Player1Turn=false;
            DisplayTurn("Tour du joueur 2");

        }//Display Player 2 turn window
            

        private void DisplayTurn(string ActualPlayerTurn)
        {

            TurnAnnouncement Temp=Instantiate(TurnAnnouncementDisplayPrefab);
            Temp.Initialize(ActualPlayerTurn);

        }//Display Current Player Turn Popup

        public void DisplayPlayer2TeamSelection()
        {

            TeamSelectContentManager.instance.SetHeaderText("Joueur 2\nComposez votre équipe.");
            TeamSelectContentManager.instance.SetConfirmButtonDisplay(false);

        }//Display Player 2 Team Selection Popup


    public static UIManager instance;
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
    }
}
