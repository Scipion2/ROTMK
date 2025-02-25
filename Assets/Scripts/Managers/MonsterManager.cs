using UnityEngine;

public class MonsterManager : MonoBehaviour
{

    [Header("Monsters Datas")]
        [SerializeField] public Invokable[] MonsterList;

    [Header("System Data")]
        [SerializeField] public bool[] SelectedMonsters;
        [SerializeField] private int SelectedMonstersCount=0;

    //SETTERS


    //GETTERS

        public int GetMonsterCount(){return SelectedMonstersCount;}//Getter For SelectedMonstersCount

        public Invokable GetMonsterByName(string name)
        {

            for(int i=0;i<MonsterList.Length;++i)
            {

                if(MonsterList[i].GetName()==name)
                    return MonsterList[i];

            }
            return null;

        }//Get The Named Monster


    //ESSENTIALS

        public void Start()
        {

            SelectedMonsters=new bool[MonsterList.Length];
            SelectedMonstersCount=0;

        }//Initialize The Manager

    //SELECTION GESTURE

        public void SelectMonster(string MonsterToSelect)
        {

            for(int i=0;i<MonsterList.Length;++i)
            {

                if(MonsterList[i].GetName()==MonsterToSelect)
                {

                    SelectedMonsters[i]=true;
                    
                }

            }


            SelectedMonstersCount++;
            TeamManager.instance.CheckSize(SelectedMonstersCount);

        }//Set The Selected Monster

        public void UnSelectMonster(string MonsterName)
        {

            Invokable MonsterToUnselect=GetMonsterByName(MonsterName);
            for(int i=0;i<MonsterList.Length;++i)
            {

                if(MonsterList[i].GetName()==MonsterToUnselect.GetName())
                {

                    SelectedMonsters[i]=false;

                }

            }


            SelectedMonstersCount--;
            TeamManager.instance.CheckSize(SelectedMonstersCount);

        }//Unselect The Current Monster

        private void ResetSelection()
        {

            SelectedMonstersCount=0;
            TeamSelectContentManager.instance.ResetButton();

        }//Reset Every Selection

    //MONSTER GESTURE

        public Invokable SpawnMonster(string MonsterToSpawn,Vector2 SpawnPlace)
        {

            Invokable Monster=null;
            for(int i=0;i<MonsterList.Length;++i)
            {

                if(MonsterList[i].GetName()==MonsterToSpawn)
                {

                    Monster=Instantiate(MonsterList[i],SpawnPlace,Quaternion.identity);

                }

            }

            return Monster;

        }//Spawn The Given Monster


        public Invokable[] SetTeam()
        {

            int TeamSlot=0;
            Invokable[] Team=new Invokable[TeamManager.instance.GetTeamSize()];

            for(int i=0;i<SelectedMonsters.Length;++i)
            {

                if(SelectedMonsters[i])
                {

                    Team[TeamSlot++]=MonsterList[i];
                    SelectedMonsters[i]=false;

                }

            }

            ResetSelection();
            return Team;

       

        }//Set The Team For Current Player




   public static MonsterManager instance;
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
