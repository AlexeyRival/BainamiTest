using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject 
{
    public int hp=100;
    public int dmg=1;
    public float speed=1;
}