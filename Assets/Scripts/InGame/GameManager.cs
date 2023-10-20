using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using Fruits;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private bool _onGame;
    public bool OnGame => _onGame;
    public bool isWaiting;
    [SerializeField] private Dropper dropper;
    public Transform Dropper => dropper.transform;
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropNewFruit().Forget();
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
        dropper.MoveDropper(moveX);
    }

    private async UniTask DropNewFruit()
    {
        isWaiting = true;
        var fruit = FruitManager.Instance.InstantiateFruit(UnityEngine.Random.Range(0, 4), dropper.Position);
        await FruitManager.Instance.WaitFruitCollision(fruit);
        isWaiting = false;
    }

    private void RotateTarget(float diff)
    {
        dropper.transform.Rotate(Vector3.up * diff * rotationSpeed);
    }
}
