using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InvokableDisplay : MonoBehaviour
{

    [Header("Monster Data")]
        [SerializeField] private TextMeshProUGUI MonsterName;
        [SerializeField] private string Monster;
        [SerializeField] private int Slot;
        [SerializeField] private Image PortraitSpace;

    [Header("Game Data")]
        [SerializeField] private SelectButton Button;
        [SerializeField] private bool isSelected=false;
        [SerializeField] private bool isOnGame=false;
        [SerializeField] private bool isSpawned=false;
        [SerializeField] private bool isGameLaunched=false;
        [SerializeField] private bool isDead=false;

    [Header("Sprites")]
        [SerializeField] private Sprite BaseSprite;
        [SerializeField] private Sprite HoverSprite;
        [SerializeField] private Sprite SelectedSprite;
        [SerializeField] private Sprite DeathSprite;
        [SerializeField] private Image ButtonSprite;
        [SerializeField] private Slider SliderHealthBar;

    //GETTERS

        public bool isItSpawned(){return isSpawned;}//Getter for isSpawned
        public bool isItSelected(){return isSelected;}//Getter For isSelected
        public string GetInvokable(){return Monster;}//Getter for Monster

    //SETTERS

        public void Initialize(Invokable SRC,int TeamSlot)
        {
          
            MonsterName.text=Monster=SRC.GetName();
            PortraitSpace.sprite=SRC.GetPortrait();
            SliderHealthBar.maxValue=SRC.GetMaxHealth();
            UpdateHealth(SliderHealthBar.maxValue);
            Slot=TeamSlot;
            SliderHealthBar.gameObject.SetActive(false);
            DeathSprite=SRC.GetDeathSprite();

        }//Set Every Variables

    //STEP CHANGERS

        public void Spawned()
        {

            isSpawned=true;
            Button.Disable();

        }//Set The Spawned Step

        public void GameLaunched()
        {

            isGameLaunched=true;
            Button.Enable();

        }//Set The Playing Step

        public void GameVersion()
        {

            isOnGame=true;
            SliderHealthBar.gameObject.SetActive(true);

        }//Set The In Game Step

    //DISPLAYS

        public void Flip()
        {

            PortraitSpace.transform.localRotation=Quaternion.Euler(0,180,0);

        }//Flip Monster Display


        

        public void SetSelectable(bool Selectable)
        {

            if(!isDead)
            {

                if(Selectable)
                    Button.Enable();
                else
                    Button.Disable();

            }

        }//Display Selectable Button


        public void UpdateHealth(float NewValue)
        {

            SliderHealthBar.value=NewValue;

        }//Display The Current Health Value

        public void Reset()
        {

            isSelected=false;
            ButtonSprite.sprite=BaseSprite;
            this.Button.Reset();

        }//Reset The Display


    //EVENTS   

        public void OnClickInvokable()
        {

            switch(GameManager.instance.GetGameState())
            {

                case GameManager.GameState.select :
                    if(isSelected)
                    {

                        MonsterManager.instance.UnSelectMonster(Monster);
                        Reset();

                    }else
                    {

                        MonsterManager.instance.SelectMonster(Monster);
                        isSelected=true;

                    }

                    break;

                default :

                if(isSelected)
                {

                    Reset();
                    GameManager.instance.UnSelectMonster();

                }else
                {

                    isSelected=true;
                    GameManager.instance.SelectMonster(TeamManager.instance.GetTeamMonster(TurnManager.instance.GetPlayerTurn(),Slot));
                    ButtonSprite.sprite=SelectedSprite;

                }

                break;

            }

        }//React To User Click

    //FIGHT

        public void Death()
        {

            SliderHealthBar.value=0;
            Slot=-1;
            PortraitSpace.sprite=DeathSprite;
            Button.Disable();
            ButtonSprite.sprite=HoverSprite;
            isDead=true;

        }//Resolve The Death


}
