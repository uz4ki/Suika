using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private bool _onGame;
    public bool OnGame => _onGame;
    public bool isWaiting;
    [SerializeField] private Transform target;
    public Transform Target => target;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private CameraHandler cameraHandler;

    private void Update()
    {
        
        if (GameManager.Instance.isWaiting) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            cameraHandler.ToggleView().Forget();
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
        
        RotateTarget(rotationX);
    }

    private void RotateTarget(float diff)
    {
        target.Rotate(Vector3.up * diff * rotationSpeed);
    }
}
