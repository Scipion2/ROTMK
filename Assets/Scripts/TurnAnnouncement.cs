using TMPro;
using UnityEngine;

public class TurnAnnouncement : MonoBehaviour
{

    [Header("Components")]
        [SerializeField] TextMeshProUGUI Body;
    

    //Essentials

        void Update()
        {

         if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.Return))
            {

                Close();

            }
        }//Check If Player Press Exit Keys

    //Settupers

        public void Initialize(string PlayerTurn)
        {

            Body.text=PlayerTurn;

        }//Display the player turn announcement

        public void Close()
        {

            Object.DestroyImmediate(this.gameObject);

        }//Destroy the window



    

    
}

