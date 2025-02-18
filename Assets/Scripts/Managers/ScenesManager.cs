using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{

    [Header("Scenes Data")]
        [SerializeField] private ScenesName CurrentScene=ScenesName.Menu;
        [SerializeField] public enum ScenesName{Menu,Credits,Tuto,TeamComp,Game,Victory,Settings}
        

    //GETTERS

        public ScenesName GetCurrentScene(){return CurrentScene;}//Getter for CurrentScene

    //CHECKERS

        public bool IsCurrentScene(ScenesName SceneToCheck)
        {

            return SceneToCheck==CurrentScene;

        }//Return true if SceneToCheck is the active scene

    //SCENE SELECTORS

        public void ToTeamSelect()
        {

            if(CurrentScene!=ScenesName.TeamComp)
            {

                SceneManager.LoadScene("TeamComp");
                CurrentScene=ScenesName.TeamComp;

            }

        }//Change the active scene for the TeamComp one

        public void ToGame()
        {

            if(CurrentScene!=ScenesName.Game)
            {

                SceneManager.LoadScene("Game");
                CurrentScene=ScenesName.Game;

            }

        }//Change the active scene for the Game one

        public void ToSettings()
        {


            if(CurrentScene!=ScenesName.Settings)
            {

                SceneManager.LoadScene("Settings");
                CurrentScene=ScenesName.Settings;

            }

        }//Change the active scene for the Settings one

        public void ToVictory()
        {

            if(CurrentScene!=ScenesName.Victory)
            {

                SceneManager.LoadScene("Victory");
                CurrentScene=ScenesName.Victory;

            }

        }//Change the active scene for the Victory one

        public void ToMenu()
        {

            if(CurrentScene!=ScenesName.Menu)
            {

                SceneManager.LoadScene("Menu");
                CurrentScene=ScenesName.Menu;

            }

        }//Change the active scene for the Menu one

        public void ToCredits()
        {

            if(CurrentScene!=ScenesName.Credits)
            {

                SceneManager.LoadScene("Credits");
                CurrentScene=ScenesName.Credits;

            }

        }//Change the active scene for the Credits one

        public void ToTutorial()
        {

            if(CurrentScene!=ScenesName.Tuto)
            {

                SceneManager.LoadScene("Tuto");
                CurrentScene=ScenesName.Tuto;

            }

        }//Change the active scene for the Tuto one
        


    public static ScenesManager instance;
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
