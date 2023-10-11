using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float radius = 6f;
    [SerializeField] private float height = 5f;
    private Transform _camera;
    private bool _isWaiting;
    private bool _isTopDownView;

    private float _angle = 0;

    private void Awake()
    {
        _camera = Camera.main.transform;   
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (_isWaiting) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            ToggleView().Forget();
            return;
        }
        
        var rotationX = 0f;
        var moveX = 0f;
        
        if (Input.GetKey(KeyCode.A))
        {
            rotationX += 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotationX -= 1f;
        }
        
        if (Input.GetKey(KeyCode.E))
        {
            moveX += 1f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            moveX -= 1f;
        }

        CalcRadius(rotationX);
        RotateTarget(rotationX);
        RotateCamera();

    }

    private void LateUpdate()
    {
        if (!_isTopDownView) _camera.LookAt(target.position);
    }

    private void CalcRadius(float diff)
    {
        _angle += diff * rotationSpeed;
        _angle %= 360f;
    }

    private Vector3 CalcPos()
    {
        return target.forward * radius * -1f + target.position.y * Vector3.up; 
    }

    private void RotateTarget(float diff)
    {
        target.Rotate(Vector3.up * diff * rotationSpeed);
    }

    private void RotateCamera()
    {
        if (_isTopDownView)
        {
            _camera.forward = target.forward;
            _camera.Rotate(Vector3.right * 90f);
        }
        else
        {
            _camera.position = CalcPos();
            _camera.LookAt(target.position);
        }
    }

    private async UniTask ToggleView()
    {
        _isWaiting = true;
        if (_isTopDownView)
        {
            _isTopDownView = false;
            var targetPos = CalcPos();
            await _camera.DOMove(targetPos, 0.3f).ToUniTask();
        }
        else
        {
            await _camera.DOMove(Vector3.up * height, 0.3f).ToUniTask();
            _isTopDownView = true;
        }

        _isWaiting = false;
    }
}
