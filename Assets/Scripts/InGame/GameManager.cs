using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using Fruits;
using InGame;
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

    private bool _isCherry =>
        WonderManager.Instance.isWonder && WonderManager.Instance.state == WonderState.CherryShotGun;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DropNewFruit().Forget();
        }
        
        var rotationX = 0f;
        var moveX = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX += 1f;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX -= 1f;
        }
        
        //RotateTarget(rotationX);
        dropper.MoveDropper(moveX);
    }

    private void InitializeGame()
    {
        score = 0;
        Time.timeScale = 1f;
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
        if (WonderManager.Instance.isWonder)
        {
            WonderManager.Instance.RefreshWonderCount();
            if (_isCherry)
            {
                nextFruit = -2;
                return;
            }

            if (FruitManager.Instance.isBigSmall)
            {
                nextFruit = -4 + UnityEngine.Random.Range(0, 2);
                return;
            }
        }
        if ((_count + 25) % 35 != 0)
        {
            nextFruit = UnityEngine.Random.Range(0, 4);
        }
        else
        {
            nextFruit = -1;
        }
    }

    public async UniTask ToggleView()
    {
        await cameraHandler.ToggleView();
    }
    
    private void RotateTarget(float diff)
    {
        dropper.transform.Rotate(Vector3.up * diff * rotationSpeed);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverObject.SetActive(true);
        if (FruitManager.Instance.isTopView) ToggleView().Forget();
        _onGame = false;
    }

    public void ToggleDropperPos()
    {
        if (WonderManager.Instance.isWonder) dropper.ToTop().Forget();
        else dropper.ToBottom().Forget();
    }
}
