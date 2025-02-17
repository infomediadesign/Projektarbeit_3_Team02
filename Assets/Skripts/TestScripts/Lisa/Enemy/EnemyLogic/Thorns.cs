using UnityEngine;

public class Thorns : MonoBehaviour
{
    public float moveRange = 5f;
    public float moveSpeed = 2f;

    private Vector3 startPosition;
    private float direction = 1f; //positiv oder negatic´v

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveRange * 2) - moveRange;
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}