using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    [SerializeField] private List<Action> BufferQueue=new List<Action>();

    public void AddAction(Invokable SRC,string TypeSRC,Tile Target)
    {

        //BufferQueue.Add(new Action(SRC,TypeSRC,Target));
        ResolveAction(new Action(SRC,TypeSRC,Target));

    }

    public void ResolveAllActions()
    {

        for(BufferQueue.Sort(new ActionComp());BufferQueue.Count!=0;ResolveAction(BufferQueue[0])){}

    }

    private void ResolveAction(Action actionToResolve)
    {

        //BufferQueue.RemoveAt(0);
        switch(actionToResolve.GetActionType())
        {

            case GameManager.MOVE :
                actionToResolve.Move();
                break;

            case GameManager.BASE :
                actionToResolve.BaseAttack();
                break;

            case GameManager.SPECIAL :
                actionToResolve.SpecialAttack();
                break;

            default :
                break;

        }

    }

    public static ActionManager instance;
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
    }

    public class Action
    {

        private float Speed;
        private Invokable Launcher;
        //private enum ActionType{Move,Base,Special}
        private string Type;
        private Tile Target;

        public Action(Invokable SRC, string TypeSRC,Tile TargetSRC)
        {
            Launcher=SRC;
            Speed=Launcher.GetSpeed();
            Type=TypeSRC;
            Target=TargetSRC;

        }//Constructor

        public void Move()
        {

            Launcher.SetMove(Target.GetPos());
            Launcher.StartWalking();

        }

        public void BaseAttack()
        {

            Launcher.BaseAttack(Target);

        }

        public void SpecialAttack()
        {

            Launcher.SpecialAttack(Target);

        }

        //GETTERS
        public float GetSpeed(){ return Speed; }
        public string GetActionType(){ return Type;}

    }

    public class ActionComp : IComparer<Action>
    {

        public int Compare(Action x, Action y)
        {
            
            if(x.GetSpeed().CompareTo(y.GetSpeed())!=0)
                return x.GetSpeed().CompareTo(y.GetSpeed());
            else
                return 0;

        }
    }


}

