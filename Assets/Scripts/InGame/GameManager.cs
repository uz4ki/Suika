using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using Fruits;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int score;
    private bool _onGame;
    
    [HideInInspector] public int nowFruit;
    [HideInInspector] public int nextFruit;
    private int _count;
    public bool OnGame => _onGame;
    public bool isWaiting;
    [SerializeField] private Dropper dropper;
    public Transform Dropper => dropper.transform;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private CameraHandler cameraHandler;
    [SerializeField] private GameObject gameOverObject;

    private void Start()
    {
        InitializeGame();
    }
    private void Update()
    {


        if (!OnGame)
        {
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
        if (isWaiting) return;
        

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
        
        if (Input.GetKey(KeyCode.E))
        {
           // rotationX += 1f;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            //rotationX -= 1f;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }
        
        RotateTarget(rotationX);
        dropper.MoveDropper(moveX);
    }

    private void InitializeGame()
    {
        score = 0;
        _onGame = true;
        nowFruit = UnityEngine.Random.Range(0, 4);
        nextFruit = UnityEngine.Random.Range(0, 4);
        FruitManager.Instance.OnFruitUpdated.Invoke();
        _count = 0;
    }

    private async UniTask DropNewFruit()
    {
        isWaiting = true;
        var fruit = FruitManager.Instance.InstantiateFruit(nowFruit, dropper.Position);
        RefreshFruitCount();
        FruitManager.Instance.OnFruitUpdated.Invoke();
        await FruitManager.Instance.WaitFruitCollision(fruit);
        isWaiting = false;
    }

    
    private void RefreshFruitCount()
    {
        _count++;
        nowFruit = nextFruit;
        if ((_count + 20) % 30 != 0)
        {
            nextFruit = UnityEngine.Random.Range(0, 4);
        }
        else
        {
            nextFruit = -1;
        }
    }
    
    private void RotateTarget(float diff)
    {
        dropper.transform.Rotate(Vector3.up * diff * rotationSpeed);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverObject.SetActive(true);
        _onGame = false;
    }
}
