using Unity.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    
    [Header("Spatial Datas")]
        [SerializeField] private int Width, Height;
        [SerializeField] private Transform cam;
        [SerializeField] private Transform GridParent;

    [Header("Tile Datas")]
        [SerializeField] private Tile TilePrefab;
        [SerializeField] private Tile[,] TileMatrix;

    //GETTERS
        public int GetWidth() {return Width;} //Getter On Width
        public int GetHeight() {return Height;}//Getter On Height
        public bool IsTileEmpty(Vector2 TilePos){return FindTile(TilePos).GetEmpty();}//Getter For The isEmpty Data From Tile At TilePos
        public Tile FindTile(Vector2 TilePos)
        {

            if((int)TilePos.x>=Width || (int)TilePos.y>=Height)
                return null;

            return TileMatrix[(int)TilePos.x,(int)TilePos.y];

        }//Getter For The Tile At TilePos

    //ESSENTIALS

        public void Start()
        {

            TileMatrix=new Tile[Width,Height];
            GenerateGrid();

        }//Initialize Grid

    //GRID GESTURE

         private void GenerateGrid()
        {

            for(int x=0;x<Width;++x)
            {

                for(int y=0;y<Height;++y)
                {

                    Tile spawnedTile=Instantiate(TilePrefab, new Vector3(x,y),Quaternion.identity);
                    spawnedTile.name="x:"+x+" y:"+y;
                    spawnedTile.Initialize(new Vector2(x,y));
                    spawnedTile.transform.SetParent(GridParent);
                    TileMatrix[x,y]=spawnedTile;

                }

            }

            cam.transform.position=new Vector3((float)Width/2-0.5f,(float)Height/2-0.8f,-10);

        }//Spawned The Grid

    //MONSTER ON TILE GESTURE

        public void ClearTile(Vector2 TilePos)
        {

            FindTile(TilePos).ClearInvokable();

        }//Clear The Invokable On Tile At TilePos

        public void FillTile(Vector2 TilePos,Content.ContentType Type,string MonsterName)
        {

            FindTile(TilePos).SetInvokable(Type,MonsterName,TurnManager.instance.GetPlayerTurn());

        }//Set The Invokable On Tile At TilePos


    //SELECT TILE GESTURE

        public void ResetTile(Tile TileToKeep)
        {

            for(int i=0;i<Width;++i)
            {

                for(int j=0;j<Height;++j)
                {

                    if(TileToKeep==null || TileToKeep.GetPos()!=TileMatrix[i,j].GetPos())
                        TileMatrix[i,j].UnselectTile();

                }

            }

        }//Reset Every Tile Selection Except For TileToKeep


        public void SetClickableTile(Vector2 pos,bool isClickable)
        {

            TileMatrix[(int)pos.x,(int)pos.y].SetClickable(isClickable);

        }//Set A Tile Clickable

        public void SetClickableTiles(int minX,int maxX,int minY,int maxY,bool isMonsterClickable)
        {

            for(int i=0;i<Width;++i)
            {

                for(int j=0;j<Height;++j)
                {

                    if(minX<=i && i<maxX && minY<=j && j<maxY)
                    {

                        if(!isMonsterClickable)
                            TileMatrix[i,j].SetClickable(TileMatrix[i,j].GetEmpty());
                        else
                            TileMatrix[i,j].SetClickable(true);

                    }else
                    {

                        TileMatrix[i,j].SetClickable(false);

                    }

                }

            }

        }//Set A Range Of Tiles Clickable
    

    public static GridManager instance;
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
    }//To call elements from this file

}
