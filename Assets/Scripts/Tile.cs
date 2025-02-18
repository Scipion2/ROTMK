using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    
   [Header("Grid Management Variables")]
       [SerializeField] private bool isEmpty=true,isBrowsable=true,isClickable=false;
       [SerializeField] private Vector2 GridPos;
       [SerializeField] private Content ObjectOnTile;

   [Header("Sprite Gesture Variables")]
       [SerializeField] private SpriteRenderer TileSprite;
       [SerializeField] private Sprite SelectedSprite,BaseSprite;
       [SerializeField] private bool isSelected=false;


   //GETTERS

       public bool GetEmpty(){return isEmpty;}//Getter For isEmpty
       public Content.ContentType GetContentType(){return ObjectOnTile.GetContentType();}//Getter For ContentType
       public string GetInvokableOnTile(){return ObjectOnTile.GetInvokable();}//Getter for ObjectOnTile.MonsterName
       public int GetInvokableTeam(){return ObjectOnTile.GetInvokableTeam();}//Getter for MonsterTeam
       public bool GetClickable(){return isClickable;}//Getter For isClickable
       public Vector2 GetPos(){return GridPos;}//Getter for GridPos

    //SETTERS

       public void Initialize(Vector2 Pos)
       {

         GridPos=Pos;
         TileSprite.color=new Color(1f,1f,1f,0.4f);

       }//Initialize the tile variables

       public void SetInvokable(Content.ContentType Type,string MonsterName,int Team)
       {

            ObjectOnTile=new Content(Type);
            ObjectOnTile.SetupInvokable(MonsterName,Team);
            isEmpty=false;

       }//Fill the content of the tile with an Invokable item

       public void SetClickable(bool IsClickable)
       { 

             isClickable=IsClickable;

       }//Set the tile clickable or not

   //MOUSE ACTION

       void OnMouseEnter()
       {

          if(isClickable)
          {

            if(!isSelected)
            TileSprite.color=new Color(0f,1f,0f,0.8f);

          }     

       }//Change the display when mouse hover over the tile

       void OnMouseExit()
       {

          if(isClickable)
          {

            TileSprite.color=new Color(1f,1f,1f,0.4f);

          }

       }//Change the display when mouse leave the tile

       void OnMouseDown()
       {
          if(isClickable)
          {

            if(isSelected)
            {

              this.UnselectTile();
              GameManager.instance.UnselectTile();

            }else if(isEmpty)
            {

              isSelected=true;
              TileSprite.sprite=SelectedSprite;
              GameManager.instance.SetSelectedTile(this);


            }else if(ObjectOnTile.GetContentType()==Content.ContentType.Monster)
            {

                if(ObjectOnTile.GetInvokableTeam()!=TurnManager.instance.GetPlayerTurn() && GameManager.instance.GetSelectedMonster()!="")
                {

                    isSelected=true;
                    TileSprite.sprite=SelectedSprite;
                    GameManager.instance.SetSelectedTile(this);

                }else
                {

                    GameManager.instance.SelectMonster(TeamManager.instance.GetTeamMonsterByName(ObjectOnTile.GetInvokableTeam(),ObjectOnTile.GetInvokable()));

                }

            }

          }
            

       }//React to the mouse click on the tile


    //CLEARER

       public void ClearInvokable()
       {

            ObjectOnTile.Void();
            isEmpty=true;

       }//Cleat the  content of the tile

       public void UnselectTile()
       {

          TileSprite.sprite=BaseSprite;
          isSelected=false;

       }//Set the tile not selected


}


public class Content
{

    //[Header("Content Data")]
        [SerializeField] public enum ContentType {None,Monster,Obstacle,Water}
        [SerializeField] private ContentType Type=Content.ContentType.None;

    [Header("Monster Data")]
        [SerializeField] private string MonsterName;
        [SerializeField] private int MonsterTeam;


    //ESSENTIALS
        public Content(Content.ContentType TypeSrc)
        {

            Type=TypeSrc;

        }//Constructor


    //GETTERS

        public string GetInvokable(){return MonsterName;}//Getter for MonsterName
        public int GetInvokableTeam(){return MonsterTeam;}//Getter for Monsterteam
        public Content.ContentType GetContentType(){return Type;}//Getter For Type


    //SETTERS

        public void SetupInvokable(string SRC,int Team)
        {

            MonsterName=SRC;
            MonsterTeam=Team;

        }

    //CLEARER

        public void Void()
        {

            Type=Content.ContentType.None;
            MonsterName="";
            MonsterTeam=0;

        }

}//Content onto the tile