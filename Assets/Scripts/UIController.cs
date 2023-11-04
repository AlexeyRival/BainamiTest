using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening;

public class UIController : MonoBehaviour
{

    public Slider HPSlider, enemyHPSlider;
    public Text waveText, HPText, enemyHPText;
    public GameObject RestartWindow;

    private int hp, enemyHp, hpsum;
    private int maxHp, maxEnemyHp;
    private int wave;
    private bool restart;

    private HandCar handCar;

    private void RedrawHP() 
    {
        hp = handCar.hp;
        HPSlider.value = hp * 1f / maxHp;
        HPText.text = hp.ToString();
    }
    private void RedrawEnemyHP()
    {
        enemyHp = hpsum;
        enemyHPSlider.value = enemyHp * 1f / maxEnemyHp;
        enemyHPText.text = enemyHp.ToString();
    }
    private void RedrawWaveCounter() 
    {
        wave = WaveController.instance.wavecounter;
        waveText.text = $"Wave {wave}";
        maxEnemyHp = WaveController.instance.GetWaveMaxHp();
    }

    private void GameOver() 
    {
        restart = true;
        RestartWindow.transform.DOMoveY(Screen.height * .5f, 2f, true);
    }
    private void Restart() 
    {
        restart = false;
        Time.timeScale = 1;
        RestartWindow.transform.DOMoveY(Screen.height * 1.5f, .1f, true);
    }

    private void Start()
    {
        handCar = GameObject.FindGameObjectWithTag("Handcar").GetComponent<HandCar>();
        maxHp = handCar.data.hp;
    }
    private void Update()
    {
        if (handCar.hp != hp) { RedrawHP(); if (restart && handCar.hp > 0) { Restart(); } }
        if (hp <= 0)
        {
            if (!restart) { GameOver(); }
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0, Time.unscaledDeltaTime * .5f);
        }
        hpsum = WaveController.instance.GetHpSum();
        if (hpsum != enemyHp) { RedrawEnemyHP(); }
        if (WaveController.instance.wavecounter != wave) { RedrawWaveCounter(); }
    }
}
