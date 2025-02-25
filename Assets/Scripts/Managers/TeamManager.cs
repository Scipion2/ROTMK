using UnityEngine;

public class TeamManager : MonoBehaviour
{

    [SerializeField] private Invokable[] TeamPlayer1;
    [SerializeField] private Invokable[] TeamPlayer2;
    [SerializeField] private const int TeamSize=3;
    [SerializeField] private int ActiveMonster;
    [SerializeField] private int Team1AliveMonster=TeamSize;
    [SerializeField] private int Team2AliveMonster=TeamSize;


    //GETTERS
        public int GetTeamSize(){return TeamSize;}//Getter for TeamSize
        public Invokable GetActiveMonster(){return TurnManager.instance.GetPlayerTurn()==1 ? TeamPlayer1[ActiveMonster] : TeamPlayer2[ActiveMonster];}//Getter for the current selected monster
        public Invokable GetTeamMonster(int Team,int Slot){if(Team==1)return TeamPlayer1[Slot];else return TeamPlayer2[Slot];}//Getter for team member
        public Invokable GetTeamMonsterByName(int Team,string MonsterName)
        {

            for(int i=0;i<TeamSize;++i)
            {

                if(Team==1)
                {

                    if(TeamPlayer1[i].GetName()==MonsterName)
                    {

                        return TeamPlayer1[i];

                    }

                }else
                {

                    if(TeamPlayer2[i].GetName()==MonsterName)
                    {

                        return TeamPlayer2[i];

                    }

                }


            }


            return null;

        }

    //TEAM SELECTION FUNC
        public void Start()
        {

            TeamPlayer1=new Invokable[TeamSize];
            TeamPlayer2=new Invokable[TeamSize];

        }//initialize Variables

        public void CheckSize(int SelectedMonsterCount)
        {

            if(SelectedMonsterCount==TeamSize)
                TeamSelectContentManager.instance.SetConfirmButtonDisplay(true);
            else
                TeamSelectContentManager.instance.SetConfirmButtonDisplay(false);

        }//Allow to confirm selection if the selection size respect the team size


        public void AddTeamMembers()
        {

            if(TurnManager.instance.GetPlayerTurn()==1)
            {

                TeamPlayer1=MonsterManager.instance.SetTeam();
                for(int i=0;i<TeamSize;TeamPlayer1[i++].SetTeam(1)){}
                TurnManager.instance.ChangeTurn();
                UIManager.instance.DisplayPlayer2TeamSelection();

            }else if(TurnManager.instance.GetPlayerTurn()==2)
            {

                TeamPlayer2=MonsterManager.instance.SetTeam();
                for(int i=0;i<TeamSize;TeamPlayer2[i++].SetTeam(2)){}
                ScenesManager.instance.ToGame();
                TurnManager.instance.ChangeTurn();
                

            }else
                Debug.Log("Turn Player Warning !");

        }//Confirm and set the team selected


    //TEAM SPAWN FUNC
        public void SpawnMonster(string MonsterToSpawn,Vector2 SpawnPlace)
        {

            for(int i=0;i<TeamSize;++i)
            {

                if(TurnManager.instance.GetPlayerTurn()==1)
                {

                    if(TeamPlayer1[i].GetName()==MonsterToSpawn)
                    {

                        TeamPlayer1[i]=MonsterManager.instance.SpawnMonster(MonsterToSpawn,SpawnPlace);
                        TeamPlayer1[i].SetTeam(1);
                        TeamPlayer1[i].SetSlot(i);
                        GameContentManager.instance.GetTeamSlot(1,i).Spawned();

                    }
                }else
                {

                    if(TeamPlayer2[i].GetName()==MonsterToSpawn)
                    {

                        TeamPlayer2[i]=MonsterManager.instance.SpawnMonster(MonsterToSpawn,SpawnPlace);
                        TeamPlayer2[i].SetTeam(2);
                        TeamPlayer2[i].SetSlot(i);
                        TeamPlayer2[i].Flip();
                        GameContentManager.instance.GetTeamSlot(2,i).Spawned();

                    }

                }

            }

        }//Spawn and keep the selected monster

        public void KillMonster(int Team, int Slot)
        {

            if(Team==1)
            {

                if(--Team1AliveMonster<=0)
                    GameManager.instance.EndGame(2);

            }else
            {

                if(--Team2AliveMonster<=0)
                    GameManager.instance.EndGame(1);

            }

        }


    //GAME FUNC
        public void SelectMonster(int slot)
        {

            ActiveMonster=slot;

        }

        public void SetTeamSelectable(int Team)
        {

            if(Team==1)
            {

                for(int i=0;i<TeamSize;++i)
                {

                    if(!TeamPlayer1[i].IsDead())
                        GridManager.instance.SetClickableTile(TeamPlayer1[i].GetPos(),true);
                    GridManager.instance.SetClickableTile(TeamPlayer2[i].GetPos(),false);

                }

            }else if(Team==2)
            {

                for(int i=0;i<TeamSize;++i)
                {

                    if(!TeamPlayer2[i].IsDead())
                        GridManager.instance.SetClickableTile(TeamPlayer2[i].GetPos(),true);
                    GridManager.instance.SetClickableTile(TeamPlayer1[i].GetPos(),false);

                }


            }else
                Debug.Log("Inexistent Team");

        }

    public static TeamManager instance;
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
