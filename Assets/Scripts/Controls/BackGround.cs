using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackGround : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private MovementData _movementData;
    [SerializeField] private float _speed = 2f;
    [Header("Jump Effect")]
    [SerializeField] private Vector3 _originPosition;


    private void Start()
    {
        _originPosition = transform.position;
    }

    public float GetCurrentSpeed()
    {
        return _speed;
    }
    public void ResetSpeed()
    {
        _speed = _movementData.baseSpeed;
    }
    public void ResetPosition()
    {
        transform.position = _originPosition;
    }
    public void Reset()
    {
        ResetSpeed();
        ResetPosition();
    }
    void FixedUpdate()
    {
        if (!GameManager.instance._IsLive)
            return;
        // 속도 증가
        _speed += _movementData.acceleration * Time.fixedDeltaTime;
        _speed = Mathf.Clamp(_speed, 0, _movementData.maxSpeed);
        // 이동
        transform.position = new Vector3(transform.position.x - _speed * Time.fixedDeltaTime * _movementData.dashEffect, _originPosition.y - GameManager.instance._jumpHeight * _movementData.jumpEffect, _originPosition.z);

        if (transform.position.x < -GameManager.instance._TileSize)
        {
            RepositionGroundTile();
        }
    }
    void LateUpdate()
    {
        if (!GameManager.instance._IsLive)
            return;
    }

    void RepositionGroundTile()
    {
        transform.Translate(Vector3.right * GameManager.instance._TileSize * 2); // 오른쪽 방향으로 꾸준히 옮기기만 해주면 된다.
    }
}