using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    [Header("Prefabs")]
        [SerializeField] private InGameSettings SettingsPrefab;
        [SerializeField] private InGameSettings InstancePrefab;
    [Header("Checkers")]
        [SerializeField] private bool isMenuOpened=false;

    //ESSENTIALS

        public void Start()
        {

            Screen.fullScreen=true;

        }//Initialize The State

        public void Update()
        {

            if(Input.GetKeyUp(KeyCode.Escape))
            {

                if(ScenesManager.instance.IsCurrentScene(ScenesManager.ScenesName.Game) || ScenesManager.instance.IsCurrentScene(ScenesManager.ScenesName.TeamComp))
                {

                    if(!isMenuOpened)
                    {

                        InstancePrefab=Instantiate(SettingsPrefab);
                        isMenuOpened=true;

                    }else
                    {

                        InstancePrefab.Close();
                        isMenuOpened=false;

                    }

                }

            }

        }//Check User Inputs

    //DISPLAYS

        public void SwitchFullScreen()
        {

            Screen.fullScreen=!Screen.fullScreen;

        }//Set The FullScreen Value

        public void IsClosed()
        {

            isMenuOpened=false;

        }//Set The Display Checker
        
    
    public static SettingsManager instance;
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
