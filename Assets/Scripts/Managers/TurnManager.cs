using UnityEngine;

public class TurnManager : MonoBehaviour
{

    [SerializeField] private int PlayerTurn=1;
    [SerializeField] private bool isGameLaunched=false;

    public int GetPlayerTurn(){return PlayerTurn;}//Getter for PlayerTurn


    public void ChangeTurn()
    {

        GameManager.instance.ResetSelection();

        if(PlayerTurn==1)
        {

            PlayerTurn=2;
            if(isGameLaunched)
            {

                GameContentManager.instance.SetTeamSelectableSlots(2);
                UIManager.instance.SetPlayer2Turn();
                TeamManager.instance.SetTeamSelectable(2);

            }

        }
        else if(PlayerTurn==2)
        {

            PlayerTurn=1;
            GameManager.instance.ChangeState();
            if(isGameLaunched)
            {
            
                GameContentManager.instance.SetTeamSelectableSlots(1);
                UIManager.instance.SetPlayer1Turn();
                TeamManager.instance.SetTeamSelectable(1);


            }else
                isGameLaunched=true;

        }
        else
            Debug.Log("Turn Player Warning !");

    }//Set The Current Turn

     public static TurnManager instance;
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
