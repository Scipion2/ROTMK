using UnityEngine;
using UnityEngine.UI;

public class MenuContentManager : MonoBehaviour
{
    
    [Header("UI COMPONENTS")]
        [SerializeField] private GameObject MenuButtons;
        [SerializeField] private Image EyeButton;
        [SerializeField] private Sprite OpenEye,ClosedEye;
    [Header("DATA COMPONENTS")]
        [SerializeField] private bool isDisplayed=true;

    //ESSENTALS

        public void Start()
        {

            Show();

        }//Init the Menu display

    //DISPLAYERS

        public void SwitchDisplay()
        {

            if(isDisplayed)
                Hide();
            else
                Show();

            isDisplayed=!isDisplayed;

        }//Switch beetween show and hide the menu buttons

        private void Hide()
        {

            MenuButtons.gameObject.SetActive(false);
            EyeButton.sprite=ClosedEye;

        }//Hide the menu buttons

        private void Show()
        {

            MenuButtons.gameObject.SetActive(true);
            EyeButton.sprite=OpenEye;

        }//Show the menu buttons

        

    public static MenuContentManager instance;
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
