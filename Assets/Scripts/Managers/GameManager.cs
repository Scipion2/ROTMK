using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    [Header("Base Variables")]
        [SerializeField] private string MapToLoad;
        [SerializeField] private int Winner;

    [Header("Monster Gesture Variables")]
        [SerializeField] private string SelectedMonster;
        [SerializeField] private Tile SelectedTile=null;
        [SerializeField] private int MonsterToSpawn;
    
    [Header("Game State Manage Variables")]
        [SerializeField] private bool isSettedUp=false;
        [SerializeField] public enum GameState{select,init,game,action}
        [SerializeField] private GameState ActualGameState=GameState.select;

    [Header("Action Gesture Variables")]
        [SerializeField] public const string MOVE="Move",BASE="Base",SPECIAL="Special";
        [SerializeField] private string ActionType;

    //GETTERS
        public GameState GetGameState(){return ActualGameState;}//Getter for ActualGameState
        public string GetActionType(){return ActionType;}//Getter for ActionType
        public int GetWinner(){return Winner;}//Getter for Winner
        public string GetSelectedMonster(){return SelectedMonster;}//Getter for SelectedMonster

    //ESSENTIALS

        public void Start()
        {

            MonsterToSpawn=TeamManager.instance.GetTeamSize();
            SelectedMonster=null;
            
        }//Setup Variables and Display and Map


    //TILE GESTURE

        public void SetSelectedTile(Tile SelectedOne)
        {

            SelectedTile=SelectedOne;
            GridManager.instance.ResetTile(SelectedTile);
            UIManager.instance.DisplaySelectTile(SelectedTile.GetPos(),ActualGameState);

            if(SelectedMonster!="")
                GameContentManager.instance.GetTeamInit().SwitchDisplayButton(true);
            else
                GameContentManager.instance.GetTeamInit().SwitchDisplayButton(false);

        }//Save The Selected Tile and Call For UIManager to Display it

        public void UnselectTile()
        {

            SelectedTile=null;
            UIManager.instance.DisplaySelectTile(new Vector2(-1,-1),ActualGameState);
            GridManager.instance.ResetTile(null);

        }//Clear the selected tile

    //MONSTER GESTURE

        public void SelectMonster(Invokable Monster)
        {

            SelectedMonster=Monster.GetName();
            UIManager.instance.DisplaySelectMonster(Monster.GetName(),ActualGameState);

            switch(ActualGameState)
            {

                case GameState.select :
                    MonsterManager.instance.SelectMonster(Monster.GetName());
                    break;

                case GameState.init :
                    if(TurnManager.instance.GetPlayerTurn()==1)
                    {

                        GridManager.instance.SetClickableTiles(0,GridManager.instance.GetWidth()/2,0,GridManager.instance.GetHeight(),false);
                        

                    }else if(TurnManager.instance.GetPlayerTurn()==2)
                    {

                        GridManager.instance.SetClickableTiles(GridManager.instance.GetWidth()/2,GridManager.instance.GetWidth(),0,GridManager.instance.GetHeight(),false);

                    }

                    GameContentManager.instance.GetTeamInit().SwitchDisplayButton(SelectedTile!=null);

                    for(int i=0;i<TeamManager.instance.GetTeamSize();++i)
                    {

                        if(GameContentManager.instance.GetTeamSlot(TurnManager.instance.GetPlayerTurn(),i).GetInvokable()!=SelectedMonster)
                            GameContentManager.instance.GetTeamSlot(TurnManager.instance.GetPlayerTurn(),i).Reset();

                    }

                    break;

                case GameState.game :
                    TeamManager.instance.SelectMonster(Monster.GetSlot());
                    UIManager.instance.DisplayActionButton(Monster.GetMove(),Monster.GetBase(),Monster.GetSpecial());
                    
                    for(int i=0;i<TeamManager.instance.GetTeamSize();++i)
                    {

                        if(GameContentManager.instance.GetTeamSlot(TurnManager.instance.GetPlayerTurn(),i).GetInvokable()!=SelectedMonster)
                            GameContentManager.instance.GetTeamSlot(TurnManager.instance.GetPlayerTurn(),i).Reset();

                    }
                    
                    break;

                default :
                    break;

            }
                

        }//Set the Monster Variable

        public void UnSelectMonster()
        {

            switch(ActualGameState)
            {

                case GameState.select:
                    MonsterManager.instance.UnSelectMonster(SelectedMonster);
                    break;

                case GameState.init:
                    GridManager.instance.SetClickableTiles(-1,-1,-1,-1,false);
                    break;

                case GameState.game:
                    break ;

                case GameState.action:
                    break ;

                default :
                    break ;

            }

            SelectedMonster="";
            UIManager.instance.DisplaySelectMonster("Monstre à Sélectionner",ActualGameState);
            
        }//Unselect The Current Monster

        public void SpawnMonster()
        {

            TeamManager.instance.SpawnMonster(SelectedMonster,SelectedTile.GetPos());
            GridManager.instance.FillTile(SelectedTile.GetPos(),Content.ContentType.Monster,SelectedMonster);
            GridManager.instance.SetClickableTiles(-1,-1,-1,-1,false);
            ResetMonsterSelection();


        }//Make The Monster On The Map

        public void SetMonsterDead(int Team, int Slot)
        {

            TeamManager.instance.KillMonster(Team,Slot);
            GameContentManager.instance.GetTeamSlot(Team,Slot).Death();

        }//Update Monster Data After Death

    //ACTION GESTURE

        public void SetActionType(string src)
        {

            Vector2 MonsterPos=new Vector2(-1,-1);
            int Slot=-1;
            int Range;
            for(int i=0;i<TeamManager.instance.GetTeamSize();++i)
                if(TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),i).GetName()==SelectedMonster)
                {

                    MonsterPos=TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),i).GetPos();
                    Slot=i;

                }
            switch(src)
            {

                case GameManager.MOVE :
                    Range=TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),Slot).GetRange();
                    GridManager.instance.SetClickableTiles((int)MonsterPos.x-Range,(int)MonsterPos.x+Range+1,(int)MonsterPos.y-Range,(int)MonsterPos.y+Range+1,false);
                    break;

                case GameManager.BASE :
                    Range=TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),Slot).GetBaseRange();
                    GridManager.instance.SetClickableTiles((int)MonsterPos.x-Range,(int)MonsterPos.x+Range+1,(int)MonsterPos.y-Range,(int)MonsterPos.y+Range+1,true);
                    break;

                case GameManager.SPECIAL :

                    Range=TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),Slot).GetSpecialRange();
                    GridManager.instance.SetClickableTiles((int)MonsterPos.x-Range,(int)MonsterPos.x+Range+1,(int)MonsterPos.y-Range,(int)MonsterPos.y+Range+1,true);
                    break;


                default :
                    GridManager.instance.SetClickableTiles(-1,-1,-1,-1,false);
                    break;

            }

            ActionType=src;

        }//Select The Action


        public void SetActionQueue()
        {

            int Slot=-1;

            for(int i=0;i<TeamManager.instance.GetTeamSize();++i)
            {

                if(TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),i).GetName()==SelectedMonster)
                    Slot=i;

            }


            if(ActionType==MOVE)
                TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),Slot).SetMove(SelectedTile.GetPos());
                
            ActionManager.instance.AddAction(TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),Slot),ActionType,SelectedTile);

            ResetAction();

        }//Queue Up The Selected Action


    //GAME PHASE GESTURE

        public void GameLaunch()
        {

            SceneManager.LoadScene(MapToLoad,LoadSceneMode.Additive);

        }//Set The Game Scene

        public void EndGame(int WinningTeam)
        {

            Winner=WinningTeam;
            ScenesManager.instance.ToVictory();

        }//End The Game

        public void ChangeState()
        {

            if(ActualGameState==GameState.select)
            {

                ActualGameState=GameState.init;
                GameLaunch();

            }else if(ActualGameState==GameState.init)
            {

                ActualGameState=GameState.game;
                UIManager.instance.LaunchGame();
                GameContentManager.instance.IsGameLaunched();

            }
            if(ActualGameState==GameState.game)
                ActualGameState=GameState.action;
            if(ActualGameState==GameState.action)
            {

                ActualGameState=GameState.game;
                GameContentManager.instance.IncrementTurnCount();

            }

        }//Change The State Of The Game
    

    //RESETTERS

        public void ResetSelection()
        {

            if(GameState.select!=ActualGameState)
            {

                UnselectTile();
                UnSelectMonster();

            }

        }//Reset Every Selection

        private void ResetMonsterSelection()
        {

            MonsterToSpawn--;
            GridManager.instance.FindTile(SelectedTile.GetPos()).UnselectTile();
            SelectedMonster="";
            SelectedTile=null;
            UIManager.instance.ResetDisplaySelection(ActualGameState);
            UIManager.instance.DisplaySelectMonster("Monstre à Sélectionner",ActualGameState);
            UIManager.instance.DisplaySelectTile(new Vector2(-1,-1),ActualGameState);

            if(MonsterToSpawn==0)
            {

                TurnManager.instance.ChangeTurn();
                MonsterToSpawn=TeamManager.instance.GetTeamSize();

            }


        }//Reset Monster Selection

        public void ResetTile()
        {}//Reset Tile Selection

        public void ResetAction()
        {}//Reset Action Selection



    //WIN GESTURE

        public void BlueWin()
        {

            ScenesManager.instance.ToVictory();

        }//Set Win For Player 1

        public void RedWin()
        {

            ScenesManager.instance.ToVictory();

        }//Set Win For Player 2


    public static GameManager instance;
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
    }//To call Function/Variables from anywhere

}
