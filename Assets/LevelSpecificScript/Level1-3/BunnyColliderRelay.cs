using System;
using UnityEngine;

public class BunnyColliderRelay : MonoBehaviour
{
    public Action<int, Collider2D> onTriggerEnter2D;
    public int me = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter2D?.Invoke(me, collision);
    }
}
