using UnityEngine;
using Spine.Unity;

[RequireComponent(typeof(SkeletonAnimation))]
public class Unit : MonoBehaviour 
{
    public State state;
    protected SkeletonAnimation skeleton;
    private void Awake()
    {
        skeleton = GetComponent<SkeletonAnimation>();
    }

    protected void ChangeStateTo(State state)
    {
        if (state != this.state)
        {
            this.state = state;
            skeleton.AnimationName = state.ToString();
        }
    }
    public enum State 
    {

        idle,
        attack,
        walk,
        run,
        ability_active
    }
}
