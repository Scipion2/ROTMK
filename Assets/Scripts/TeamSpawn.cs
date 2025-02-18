using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamSpawn : MonoBehaviour
{
    
    [Header("Component Variables")]
        [SerializeField] private TextMeshProUGUI MonsterSelectDisplay;
        [SerializeField] private TextMeshProUGUI TileSelectDisplay;
        [SerializeField] private TextMeshProUGUI Instructions;
        [SerializeField] private Button ConfirmButton;

    //SETTERS

        public void SetInstructionDisplay(string Content)
        {

            Instructions.text=Content;

        }//Set the instructions text


        public void SetMonsterNameDisplay(string Name)
        {

            MonsterSelectDisplay.text=Name;

        }//Set the Selected monster name display

        public void SetTileSelectDisplay(int x,int y)
        {

            if(x==0 || y==0)
                TileSelectDisplay.text="Emplacement à sélectionner";
            else
                TileSelectDisplay.text="Case : "+x+","+y;

        }//Set the selected tile position display

    //DISPLAYER

        public void SwitchDisplayButton(bool isVisible)
        {

            ConfirmButton.gameObject.SetActive(isVisible);

        }

    

}
