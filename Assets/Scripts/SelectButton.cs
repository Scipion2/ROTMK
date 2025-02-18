using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{

    [Header("Select Variables")]
        [SerializeField] Sprite Selected,NotSelected;
        [SerializeField] Image ButtonSprite;
        [SerializeField] Button button;
        private bool isSelected=false;

    //INTERRACT

        public void Enable()
        {

            button.interactable=true;

        }//Allow the mouse to click on the button

        public void Disable()
        {

            button.interactable=false;

        }//Don't allow the mouse to click on the button

    //DISPLAYER

        public void SpriteSwap()
        {

            isSelected=!isSelected;
            if(isSelected)
                ButtonSprite.sprite=Selected;
            else
                ButtonSprite.sprite=NotSelected;

        }//Set the button sprite if it's selected or not

    //RESETTER

        public void Reset()
        {

            isSelected=false;
            ButtonSprite.sprite=NotSelected;

        }//Reset the button state

    
}
