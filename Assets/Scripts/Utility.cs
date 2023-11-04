using UnityEngine;

public class Utility
{
    public static Quaternion LookAt2D(Transform transform, Transform target)
    {
        Quaternion rotation = Quaternion.LookRotation
            (target.transform.position - transform.position, transform.TransformDirection(Vector3.up));
        return new Quaternion(0, 0, rotation.z, rotation.w);
    }
    public static Quaternion LookAt2D(Transform transform, Vector3 target)
    {
        Quaternion rotation = Quaternion.LookRotation
            (target - transform.position, transform.TransformDirection(Vector3.up));
        return new Quaternion(0, 0, rotation.z, rotation.w);
    }
}

