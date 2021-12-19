using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShadowPlayer))]
public class PlayerMove : MonoBehaviour
{
    private ShadowPlayer player;

    [Header("To Link")]
    public Transform graphic;

    [Header("Properties")]
    public float moveSpeed;


    [Header("States")]
    public Vector3 velocity;

    private float rotationAngle;


    private void Awake()
    {
        player = GetComponent<ShadowPlayer>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        Move(move);

        UpdatePosition();
    }


    private void UpdatePosition()
    {
        float x = transform.position.x + velocity.x * Time.deltaTime;
        float y = transform.position.y + velocity.y * Time.deltaTime;
        float z = transform.position.z + velocity.z * Time.deltaTime;

        y = Mathf.Max(y, 0.01f);
        z = Mathf.Min(z, -0.01f);

        transform.position = new Vector3(x, y, z);
    }

    void Move(Vector2 direction)
    {
        Vector3 vertical;
        if(direction.y > 0 && player.wallTouching > ShadowPlayer.TOUCH_DISTANCE)
            vertical = Vector3.up * direction.y;
        else if (direction.y < 0 && player.floorTouching > ShadowPlayer.TOUCH_DISTANCE) 
            vertical = Vector3.forward * direction.y;
        else
            vertical = (player.floorTouching > ShadowPlayer.TOUCH_DISTANCE ? Vector3.forward : Vector3.up) * direction.y;

        velocity = direction.x * moveSpeed * Vector3.right + moveSpeed * vertical;
    }

}



