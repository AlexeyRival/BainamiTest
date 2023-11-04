using UnityEngine;

public class HandCar:MonoBehaviour
{
    public HandCarData data;
    public int hp;
    
    public GameObject heroprefab;
    private bool extrahero;
    
    public void Dmg(int dmg) 
    {
        hp -= dmg;
    }

    private void Start()
    {
        hp = data.hp;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !extrahero) 
        {
            extrahero = true;
            Instantiate(heroprefab);
        }
    }
}
