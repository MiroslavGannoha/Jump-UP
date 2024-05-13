using Unity.Collections;
using UnityEngine;

public class FollowGO : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    float YOffset = 0.5f;

    void Update()
    {
        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y - YOffset,
            target.position.z
        );
        transform.position = targetPosition;
    }
}
