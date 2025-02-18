using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Header : MonoBehaviour
{
    
    [Header("Components")]
        [SerializeField] private Button EndTurnButton;
        [SerializeField] private TextMeshProUGUI TurnCount;
        [SerializeField] private int CurrentTurn=0;

    public void IncrementTurnCount()
    {
        
        CurrentTurn++;
        TurnCount.text="Tour Numéro "+CurrentTurn+" : Joueur "+TurnManager.instance.GetPlayerTurn();

    }//Display the next turn in game

    public void EndTurn()
    {

        TurnManager.instance.ChangeTurn();

    }//Go for the next turn of game

    public void ShowEndTurn(bool isShow)
    {

        EndTurnButton.gameObject.SetActive(isShow);

    }//Display the end turn button

}
