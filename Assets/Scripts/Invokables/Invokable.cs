using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Invokable : MonoBehaviour
{
    
    //Initialize Variable
	[Header("Stats Variables")]
		[SerializeField] protected int MaxHealth =1;
		[SerializeField] protected float CurrentHealth=1f;
		[SerializeField] protected int MaxRessources=1;
		[SerializeField] protected float CurrentRessources=1f;
		[SerializeField] protected float Speed = 0.5f;
		[SerializeField] protected float Attack=0.5f;
		[SerializeField] protected float Power=0.5f;
		[SerializeField] protected float PhysicalDefense=0.5f;
		[SerializeField] protected float MagicalDefense=0.5f;
		[SerializeField] protected string Name=null;
	
	[Header("Annex Datas")]
		
		[SerializeField] protected int Team,Slot;
		[SerializeField] protected Sprite Portrait;
		[SerializeField] private Sprite DeadSprite;
		[SerializeField] protected enum ClassOfMonster {Fighter,Range,Mobile}
		[SerializeField] protected enum NatureOfMonster {Neutral=2,Coward=1,Brave=3} //Value are set to calculate the odds the monster will have to obey
		[SerializeField] protected bool isDead=false;

	[Header("Action Variables")]
		[SerializeField] protected bool move=true,baseAction=true,specialAction=true;
		[SerializeField] protected int MoveRange=2;
		[SerializeField] protected Vector2 PosToGo;
		[SerializeField] protected bool isWalking=false;

	[Header("Attack Variables")]
		[SerializeField] protected Skill Base,Special;


	[Header("Animation States")]
		[SerializeField] Animator InvokableAnimator;
		[SerializeField] private SpriteRenderer MonsterSprite;
		//Animation Settable
		[SerializeField] private const string IsIdle="IsIdle";
		[SerializeField] private const string IsWalking="IsWalking";
		[SerializeField] private const string IsAttacking="IsAttacking";
		[SerializeField] private const string IsDying="IsDying";
		[SerializeField] private const string IsSpecialAttacking="IsSpecialAttacking";


	//GETTERS
		public int GetRange(){return MoveRange;}//Getter For MoveRange
		public int GetBaseRange(){return Base.GetRange();}//Getter For Range Of Base Attack
		public int GetSpecialRange(){return Special.GetRange();}//Getter For Range Of Special Attack
		public string GetBaseName(){return Base.GetName();}//Getter for the name of the base attack
		public string GetSpecialName(){return Special.GetName();}//Getter for the name of the special attack
		public float GetSpeed(){return Speed;}//Getter for Speed
		public float GetMaxHealth(){return MaxHealth;}//Getter for MaxHealth
		public float GetHealth(){return CurrentHealth;}//Getter For Health		
		public float GetMaxRessources(){return MaxRessources;}//Getter for MaxRessources
		public string GetName(){return Name;}//Getter For Name
		public bool GetMove(){return move;}//Getter For Move
		public bool GetBase(){return baseAction;}//Getter For BaseAction
		public bool GetSpecial(){return specialAction;}//Getter For SpecialAction
		public Sprite GetPortrait(){return Portrait;}//Getter For Portrait
		public int GetTeam(){return Team;}//Getter For Team
		public int GetSlot(){return Slot;}//Getter for Slot
		public Vector2 GetPos(){return this.transform.position;}//Getter for MonsterPos
		public Sprite GetDeathSprite(){return DeadSprite;}//Getter for DeadSprite
		public string GetBaseText(){return Base.GetText();}//Getter for text of the base action
		public string GetSpecialText(){return Special.GetText();}//Gettet for text of the special action
		public bool IsDead(){return isDead;}//Getter for isDead

	//SETTERS
		public void SetTeam(int TeamToJoin){Team=TeamToJoin;}//Set Team Value

		public void SetSlot(int src){Slot=src;}//Set Slot Value

		public void SetMove(Vector2 Pos){PosToGo=Pos;}//Set the target where to move

	//SPRITES
		public void Flip()
		{

			MonsterSprite.flipX=true;

		}//Reverse the sprite

		public void SetAnimation(string AnimationToSet)
		{

			InvokableAnimator.SetBool(IsIdle,AnimationToSet==IsIdle);
			InvokableAnimator.SetBool(IsWalking,AnimationToSet==IsWalking);
			InvokableAnimator.SetBool(IsAttacking,AnimationToSet==IsAttacking);
			InvokableAnimator.SetBool(IsDying,AnimationToSet==IsDying);
			InvokableAnimator.SetBool(IsSpecialAttacking,AnimationToSet==IsSpecialAttacking);

		}//Set the animation called within AnimationToSet for the Invokable running there

	//ACTIONS

		public void BaseAttack(Tile Target)
		{

			SetAnimation(IsAttacking);
			StartCoroutine(WaitForAttackTime());
			UIManager.instance.UpdateActionPoint();
			string MonsterTargeted=Target.GetInvokableOnTile();
			int MonsterTeam=Target.GetInvokableTeam();
			TeamManager.instance.GetTeamMonsterByName(MonsterTeam,MonsterTargeted).TakeDamage(Base.GetDamage(),Base.GetDamageType());

		}//Launch the base attack

		public void SpecialAttack(Tile Target)
		{

			SetAnimation(IsSpecialAttacking);
			StartCoroutine(WaitForAttackTime());
			UIManager.instance.UpdateActionPoint();
			string MonsterTargeted=Target.GetInvokableOnTile();
			int MonsterTeam=Target.GetInvokableTeam();
			TeamManager.instance.GetTeamMonsterByName(MonsterTeam,MonsterTargeted).TakeDamage(Special.GetDamage(),Special.GetDamageType());

		}//Launch the special attack

		IEnumerator WaitForAttackTime()
	    {

	        yield return new WaitForSecondsRealtime((float)1.5);
	        SetAnimation(IsIdle);

	    }//Waiter for the end of the attack animation

	//DAMAGE SYSTEM

		public void TakeDamage(float DamageToTake,Skill.TypeOfDamage DamageType)
		{

			switch(DamageType)
			{

				case Skill.TypeOfDamage.Physical :

					TakePhysicalDamage(DamageToTake);
					HealthChecker();
					break;

				case Skill.TypeOfDamage.Magical :

					TakeMagicalDamage(DamageToTake);
					HealthChecker();
					break;

				case Skill.TypeOfDamage.Mental :

					TakeMentalDamage(DamageToTake);
					break;

				case Skill.TypeOfDamage.None :

					CurrentHealth-=DamageToTake;
					HealthChecker();
					break;

				default :
					break;

			}

			

		}//Receive an event to take damage from an attack

		private void TakePhysicalDamage(float DamageToTake)
		{

			CurrentHealth-=DamageToTake-PhysicalDefense;

		}

		private void TakeMagicalDamage(float DamageToTake)
		{

			CurrentHealth-=DamageToTake-MagicalDefense;

		}

		private void TakeMentalDamage(float DamageToTake)
		{

			CurrentHealth-=DamageToTake;
			HealthChecker();

		}

		private float PhysicalDamageCalc(float DamageToTake)
		{

			return DamageToTake-PhysicalDefense;

		}//Calculator for Physical Damage

		private float MagicalDamageCalc(float DamageToTake)
		{

			return DamageToTake-MagicalDefense;

		}//Calculator for Magical Damage

	//DEATH SYSTEM

		private bool HealthChecker()
		{

			GameContentManager.instance.GetTeamSlot(Team,Slot).UpdateHealth(CurrentHealth);

			if(0>=CurrentHealth)
			{

				Death();
				StartCoroutine(WaitForDeathTime());
				return true;
				

			}else
				return false;

		}//Check if the monster is dead or not, then do the correct animation/action

		IEnumerator WaitForDeathTime()
	    {
	        yield return new WaitForSecondsRealtime((float)1.5);
	        Object.DestroyImmediate(this.gameObject);
	    }//Wait for the death animation time

		private void Death()
		{

			SetAnimation(IsDying);
			GridManager.instance.ClearTile(this.transform.position);
			GameManager.instance.SetMonsterDead(Team,Slot);
			isDead=true;

		}//Launch the Death animation then delete the instance

	//MOVEMENT

		public void StartWalking()
		{

			if(GridManager.instance.IsTileEmpty(PosToGo))
			{

				isWalking=true;
				SetAnimation(IsWalking);
				GridManager.instance.ClearTile(this.transform.position);
				GridManager.instance.FillTile(PosToGo,Content.ContentType.Monster,Name);
				UIManager.instance.UpdateActionPoint();

			}

		}//Launch the move action

	//CHECKERS

		public bool CheckRange(Vector2 SelectedPos)
		{

			if(Mathf.Abs(this.transform.position.x-SelectedPos.x)<=MoveRange && Mathf.Abs(this.transform.position.y-SelectedPos.y)<=MoveRange)
				return true;
			else
				return false;

		}//Check if the move range is respected

	
	//GLOBALS
	
		public void Update()
		{

			if(isWalking)
			{

				this.transform.position=Vector2.MoveTowards(this.transform.position,PosToGo,0.5f*Time.deltaTime);
				if((Vector2)transform.position==PosToGo)
				{

					isWalking=false;
					PosToGo=new Vector2(-1,-1);
					SetAnimation(IsIdle);

				}

			}//Ensure movement when invokable are beetween two pos

		}

}
