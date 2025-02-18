using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Victory : MonoBehaviour
{

    [Header("Victory Text Variables")]
        public TextMeshProUGUI VictoryText;
        public string team1="Team 1 Victory ! You are the Monster's King !";
        public string team2="Team 2 Victory ! You are the Monster's King !";

    //Essentials
        public void Start()
        {

            if(GameManager.instance.GetWinner()==1)
            {

                VictoryText.text = team1;
                VictoryText.fontSize = 50;
                VictoryText.alignment = TextAlignmentOptions.Center;

            }

            else
            {

                VictoryText.text = team2;
                VictoryText.fontSize = 50;
                VictoryText.alignment = TextAlignmentOptions.Center;

            }
           

        }//Setup Victory Text On Load

    //Game Navigation

        public void BackToMenu()
        {

            ScenesManager.instance.ToMenu();

        }//Go Back To The Game Menu

        public void BackToGame()
        {

            ScenesManager.instance.ToTeamSelect();

        }// Go Back To a New Game

        public void BackToCredits()
        {

            ScenesManager.instance.ToCredits();

        }//Go Back To The Credits Scene

   
}
