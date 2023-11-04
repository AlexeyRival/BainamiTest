using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public WaveData[] waves;
    public int wavecounter=0;
    public int totalhp;
    public static WaveController instance;
    private List<Enemy> enemys;
    private int waveSum;
    private int spawnDistance=16;
    private HandCar car;

    public void AddEnemy(Enemy enemy) 
    {
        enemys.Add(enemy);
    }
    public void RemoveEnemy(Enemy enemy) 
    {
        enemys.Remove(enemy);
    }
    
    public Enemy GetNearest(Vector2 point, float distance) 
    {
        int id = -1;
        float mindis = distance;
        float d;
        for (int i = 0; i < enemys.Count; ++i) 
        {
            d = Vector2.Distance(point, new Vector2(enemys[i].transform.position.x, enemys[i].transform.position.y));
            if (d < mindis) 
            {
                id = i;
                mindis = d;
            }
        }
        if (id == -1) return null;
        return enemys[id];
    }

    public int GetHpSum() 
    {
        if (enemys.Count == 0) { NextWave(); }
        int sum = 0;
        for (int i = 0; i < enemys.Count; ++i)
        {
            sum += enemys[i].hp;
        }
        return sum;
    }
    public int GetWaveMaxHp() 
    {
        return waveSum;
    }

    private void NextWave() 
    {
        wavecounter++;
        int sum=0;
        float x = 0;

        //игра может продолжаться и после максимально заложенной волны, однако зомби будут идти кучнее
        int waveid = wavecounter < waves.Length ? wavecounter : (waves.Length - 1);
        for (int i = 0; i < waves[waveid].enemys.Length; ++i) 
        {
            sum += waves[waveid].enemys[i].count * waves[waveid].enemys[i].enemy.data.hp;
            for (int j = 0; j < waves[waveid].enemys[i].count; ++j)
            {
                x += Random.Range(1f, 4f) / (wavecounter + 1);
                Instantiate(waves[waveid].enemys[i].enemy, new Vector3(spawnDistance + x, Random.Range(-1f, 1f)+transform.position.y, 0), Quaternion.identity);
            }
        }
        waveSum = sum;
    }
    public void RestartWave() 
    {
        car.hp = car.data.hp;
        wavecounter--;
        for (int i = 0; i < enemys.Count; ++i) 
        {
            Destroy(enemys[i].gameObject);
        }
        enemys.Clear();
        NextWave();
    }

    private void Awake()
    {
        enemys = new List<Enemy>();
        instance = this;
    }
    private void Start()
    {
        NextWave();
        car = GameObject.FindGameObjectWithTag("Handcar").GetComponent<HandCar>();
    }
}
