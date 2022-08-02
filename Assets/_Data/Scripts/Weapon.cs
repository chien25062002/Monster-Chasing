using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Weapon : MonoBehaviour
{
    public float direction;
    public float speed = 50f;
    private float liveTime = 1.5f;
    public GameObject owner;
    public GameObject target;
    [SerializeField] protected AIDestinationSetter destination;

    private void Awake() {
        destination = GetComponent<AIDestinationSetter>();
        destination.target = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
            Destroy(gameObject);
    }

    public void FlipX() {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (direction > 0)
            sr.flipX = false;
        if (direction < 0)
            sr.flipX = true;
    }
}
