using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float radius = 6f;
    [SerializeField] private float height = 5f;
    private Transform _camera;
    private bool _isTopDownView;

    private void Awake()
    {
        _camera = Camera.main.transform;
        _target = GameManager.Instance.Target;
    }
    
    private void Update()
    {
        if (GameManager.Instance.isWaiting) return;
        RotateCamera();
    }

    private void LateUpdate()
    {
        if (!_isTopDownView) _camera.LookAt(_target.position);
    }

    private Vector3 CalcPos()
    {
        return _target.forward * radius * -1f + _target.position.y * Vector3.up; 
    }

    private void RotateCamera()
    {
        if (_isTopDownView)
        {
            _camera.forward = _target.forward;
            _camera.Rotate(Vector3.right * 90f);
        }
        else
        {
            _camera.position = CalcPos();
            _camera.LookAt(_target.position);
        }
    }

    public async UniTask ToggleView()
    {
        GameManager.Instance.isWaiting = true;
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

        GameManager.Instance.isWaiting = false;
    }
}
