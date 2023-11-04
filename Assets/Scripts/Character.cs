using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity.AnimationTools;
using Spine.Unity;

[RequireComponent(typeof(SkeletonAnimation))]
public class Character : Unit
{
    public CharacterData data;
    public Bullet bullet;

    private Enemy target;

    public Transform gunRoot;
    public Transform firePoint;

    private readonly string FIRE = "shoot";

    public void OnAnimationEvent(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name == FIRE)
        {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation).Construct(target, data.dmg);
        }
    }

    private void Idle() { Scan(); }
    private void Attack() {
        if (!target) { Scan();return; }
        gunRoot.rotation= Utility.LookAt2D(gunRoot,target.transform); 
    }
    private void AbilityActive() { }


    private void Scan() 
    {
        target = WaveController.instance.GetNearest(new Vector2(transform.position.x, transform.position.y), data.range);
        if (!target)
        {
            ChangeStateTo(State.idle);
        }
        else 
        {
            ChangeStateTo(State.attack);
        }
    }

    private void Start()
    {
        skeleton.AnimationState.Event += OnAnimationEvent;
    }
    private void Update()
    {
        switch (state)
        {
            case State.idle:Idle(); break;
            case State.attack:Attack(); break;
            case State.ability_active:AbilityActive(); break;
            default:break;
        }
    }
}
