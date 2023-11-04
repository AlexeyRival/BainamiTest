using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed=1f;
    public Enemy target;
    public int dmg;

    private float realSpeed;
    private Vector3 endposition;
    private Vector3 startposition;
    private float timer;

    public void Construct(Enemy target, int dmg) 
    {
        this.target = target;
        this.dmg = dmg;

        transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
        startposition = transform.position;
        endposition = target.transform.position+new Vector3(0,0.5f,0);
        Utility.LookAt2D(transform, endposition);
        realSpeed = speed / Vector2.Distance(startposition, endposition)*40f;
    }

    //это можно было сделать и через transform.translate + OnTriggerEnter2D, но приведённый ниже вариант чуть производительнее.
    private void Update()
    {
        if (timer >= .9f) { Damage(); }
        timer += Time.deltaTime * realSpeed;
        transform.position = Vector3.Lerp(startposition, endposition,timer);
    }
    private void Damage() 
    {
        Destroy(gameObject);
        if(target)target.Dmg(dmg);
    }
}
