using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTurn : MonoBehaviour
{
    
    [Header("Display Variables")]
        [SerializeField] private Image PortraitSlot;
        [SerializeField] private TextMeshProUGUI Instructions,ActionInstructions;
        [SerializeField] private TextMeshProUGUI ActionPointQuantity;
        [SerializeField] private TextMeshProUGUI ActionData;

    [Header("Buttons Variables")]
        [SerializeField] private Button MoveButton,BaseActionButton,SpecialActionButton;
        [SerializeField] private TextMeshProUGUI BaseName,SpecialName;
        [SerializeField] private Button ConfirmButton;

    [Header("Data Variables")]
        [SerializeField] private int PA=4;

    //SETTERS

        public void SetMove()
        {

            SetSelectedAction(GameManager.MOVE);

        }//Set the move action details

        public void SetBase()
        {

            SetSelectedAction(GameManager.BASE);

        }//Set the base action details

        public void SetSpecial()
        {

            SetSelectedAction(GameManager.SPECIAL);

        }//Set the special action details
        
        private void SetSelectedAction(string ActionType)
        {

            GameManager.instance.SetActionType(ActionType);
            UpdateActionInfoDisplay(ActionType);
            DisplayActionDetails(true);        

        }//Set the selected action details

    //ESSENTIALS

        public void OnAwake()
        {

            UIManager.instance.SetPlayerTurnDisplay(this);

        }

    //DISPLAYER

        public void DisplayMonster(string MonsterName)
        {

            Invokable tampon=MonsterManager.instance.GetMonsterByName(MonsterName);
            if(tampon!=null)
            {
                Sprite temp=tampon.GetPortrait();
                PortraitSlot.gameObject.SetActive(true);
                if(temp!=null)
                    PortraitSlot.sprite=temp;

            }
            else
            {

                PortraitSlot.gameObject.SetActive(false);
                DisplayMove(false);
                DisplayBaseACtion(false,"");
                DisplaySpecialAction(false,"");
                DisplayConfirmButton(false);
                DisplayActionDetails(false);

            }

        }//Display the monster related informations and actions

        public void DisplayMove(bool isDisplay)
        {
            
            MoveButton.gameObject.SetActive(isDisplay);

        }//Display the move button

        public void DisplayBaseACtion(bool isDisplay,string Name)
        {

            BaseActionButton.gameObject.SetActive(isDisplay);
            BaseName.text=Name;

        }//Display the base button
        
        public void DisplaySpecialAction(bool isDisplay,string Name)
        {

            SpecialActionButton.gameObject.SetActive(isDisplay);
            SpecialName.text=Name;

        }//Display the special button

        public void DisplayConfirmButton(bool isDisplay)
        {

            ConfirmButton.gameObject.SetActive(isDisplay);

        }//Display the confirm button

        public void DisplayActionDetails(bool isDisplay)
        {

            ActionInstructions.gameObject.SetActive(isDisplay);
            ActionData.gameObject.SetActive(isDisplay);

        }//Display the action details

        public void DisplaySelectTile(Vector2 Tile)
        {

            if((int)Tile.x==-1 || (int)Tile.y==-1)
            {

                ActionData.text="";
                ActionData.gameObject.SetActive(false);

            }else
            {

                ActionData.text="Case ciblée : "+Tile.x+","+Tile.y;
                ActionData.fontSize=22;

            }

        }//Display the selected tile informations

        public void UpdateActionInfoDisplay(string ActionType)
        {

            switch(ActionType)
            {

                case GameManager.BASE:
                    ActionInstructions.text=TeamManager.instance.GetTeamMonsterByName(TurnManager.instance.GetPlayerTurn(),GameManager.instance.GetSelectedMonster()).GetBaseText();                
                    ActionInstructions.fontSize=22;
                    break;

                case GameManager.SPECIAL:
                    ActionInstructions.text="";                
                    ActionInstructions.fontSize=22;
                    break;

                case GameManager.MOVE:
                    ActionInstructions.text="Veuillez selectionner la tuile de destination.\nPuis validez l'action.";
                    ActionInstructions.fontSize=22;
                    break;

                default:
                    break;

            }

        }//Display the action related details

    //ACTION GESTURE

        public void ConfirmAction()
        {

            GameManager.instance.SetActionQueue();

        }//Set the selected action to the ActionQueue

        public void UpdateActionPoint()
        {

            PA--;
            ActionPointQuantity.text=PA.ToString();
            if(PA<=0)
            {

                TurnManager.instance.ChangeTurn();
                ResetTexts();

            }

        }//Keep up to date the action point then check if the turn may change

    //RESETTER

        private void ResetTexts()
        {

            ActionInstructions.text="";
            ActionData.text="";
            PA=4;
            ActionPointQuantity.text=PA.ToString();
            ActionData.gameObject.SetActive(false);
            ActionInstructions.gameObject.SetActive(false);
            ConfirmButton.gameObject.SetActive(false);
            DisplayMove(false);
            DisplayBaseACtion(false,"");
            DisplaySpecialAction(false,"");
            PortraitSlot.gameObject.SetActive(false);

        }//Reset every display

}
