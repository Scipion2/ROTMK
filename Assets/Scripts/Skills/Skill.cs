using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    
	[SerializeField] protected string SkillName=null;
	[SerializeField] protected float Cost=0;
	[SerializeField] protected float Damage=0;
	[SerializeField] protected int MinRange=1,MaxRange=1;
	[SerializeField] protected int[] AreaSize=new int[]{0,0,0,0};
	[SerializeField] protected bool isAllyTargetable=false;
	[SerializeField] protected bool isEnnemyTargetable=false;
	[SerializeField] public enum TypeOfDamage{None,Magical,Physical,Mental}
	[SerializeField] public enum TypeOfSkill{Cac,Range,Buff,Debuff}
	[SerializeField] protected TypeOfDamage DamageType;
	[SerializeField] protected TypeOfSkill SkillType;
	[SerializeField] protected Projectile Projectile;
	[SerializeField] protected string SkillDetails;

	public float GetDamage(){return Damage;}//Getter for damage
	public TypeOfDamage GetDamageType(){return DamageType;}//Getter for DamageType
	public string GetText(){return SkillDetails;}//Getter for SkillDetails
	public int GetRange(){return MaxRange;}//Getter for Range
	public string GetName(){return SkillName;}//Getter for SkillName

	public abstract void Launch();

}
