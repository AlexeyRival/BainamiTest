using UnityEngine;
using Spine.Unity;
[RequireComponent(typeof(SkeletonAnimation))]
public class Enemy:Unit
{
    public EnemyData data;

    public int hp=100;
    public float speed;

    private HandCar target;
    private int maxhp;

    public Transform hpBar;

    private readonly string DEATH = "death";
    private readonly string ATTACK = "shoot";
    public void OnAnimationEvent(Spine.TrackEntry trackEntry, Spine.Event e) 
    {
        if (e.Data.Name == ATTACK) 
        {
            target.Dmg(data.dmg);
        }
    }

    public void Dmg(int dmg) 
    {
        hp -= dmg;
        if (hp <= 0) { Die(); }
        hpBar.transform.localScale = new Vector3(hp * 1f / maxhp, 1, 1);
    }
    private void Die()
    {
        Destroy(gameObject, .6f);
        ChangeStateTo(State.idle);
        WaveController.instance.RemoveEnemy(this);
        skeleton.AnimationName = DEATH;
    }

    private void Walk()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < 3) 
        {
            ChangeStateTo(State.attack);
        }
    }
    private void Run()
    {
        transform.Translate(-speed * 2 * Time.deltaTime, 0, 0);
        if (Mathf.Abs(transform.position.x - target.transform.position.x) < 3)
        {
            ChangeStateTo(State.attack);
        }
    }
    private void Attack() { }
    
    private void Start()
    {
        skeleton.AnimationState.Event += OnAnimationEvent;

        hp = data.hp;
        maxhp = hp;
        speed = data.speed;

        WaveController.instance.AddEnemy(this);
        ChangeStateTo(State.walk);
        target = GameObject.FindGameObjectWithTag("Handcar").GetComponent<HandCar>();
    }
    private void Update()
    {
        switch (state)
        {
            case State.idle:break;
            case State.walk:Walk(); break;
            case State.run: Run(); break;
            case State.attack: Attack(); break;
        }
    }
}
