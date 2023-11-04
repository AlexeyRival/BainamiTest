using UnityEngine;

[CreateAssetMenu(menuName ="Data/Character")]
public class CharacterData : ScriptableObject 
{
    public Bullet bulletprefab;
    
    public int dmg = 1;

    [Range(5f,100f)]
    public float range = 20;
}