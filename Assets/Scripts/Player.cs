using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float paddingLeft = 0f;
    [SerializeField] float paddingRight = 0f;
    [SerializeField] float paddingTop = 0f;
    [SerializeField] float paddingBottom = 0f;
    bool isMovingByMouse = false;
    Vector2 rawInput;

    Vector2 minBounds;//(0, 0)
    Vector2 maxBounds;//(1, 1)

    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
        // Debug.Log(minBounds);
        // Debug.Log(maxBounds);
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
        // Debug.Log(rawInput);
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMovingByMouse = true;
            shooter.isFiring = true;
        }
        Vector2 deltaMove = rawInput * moveSpeed * Time.deltaTime;
        if (isMovingByMouse && Input.GetMouseButton(0))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            Vector2 moveDistance = (Vector2)(targetPosition - transform.position);
            Vector2 moveDirection = moveDistance.normalized;
            deltaMove = moveDirection * moveSpeed * Time.deltaTime;
            if (deltaMove.magnitude > moveDistance.magnitude)
            {
                deltaMove = moveDistance;
            }
        }
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + deltaMove.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + deltaMove.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;

        if (Input.GetMouseButtonUp(0))
        {
            isMovingByMouse = false;
            shooter.isFiring = false;
        }
    }
}
