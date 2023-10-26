using Cysharp.Threading.Tasks;
using DefaultNamespace;
using DG.Tweening;
using Fruits;
using GUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.PostProcessing;

namespace InGame
{
    public enum WonderState
    {
        CherryShotGun = 0,
        Driving = 1,
        TopView = 2,
        BigSmall = 3,
        FromBottom = 4,
        EvolutionShuffle = 5
    }
    public class WonderManager : SingletonMonoBehaviour<WonderManager>
    {
        [SerializeField] private WonderFlowerView flowerView;
        [SerializeField] private PostProcessVolume processVolume;
        [SerializeField] private ViewWonderText viewWonderText;
        public bool isWonder;
        public WonderState state;
        public int counter;

        public UnityEvent onCounterUpdate;
        public async UniTask WonderAnimation()
        {
            flowerView.gameObject.SetActive(true);
            GameManager.Instance.isWaiting = true;
            flowerView.Animation().Forget();
            StartWonder();
            await DOVirtual.Float(0f, 1f, 1f, x => processVolume.weight = x).SetEase(Ease.OutQuad).ToUniTask();
            flowerView.gameObject.SetActive(false);
            GameManager.Instance.isWaiting = false;
        }
        
        public async UniTask DewonderAnimation()
        {
            GameManager.Instance.isWaiting = true;
            await DOVirtual.Float(1f, 0f, 1f, x => processVolume.weight = x).SetEase(Ease.OutQuad).ToUniTask();
        }

        private void StartWonder()
        {
            counter = 9;
            state = (WonderState) UnityEngine.Random.Range(0, 6);
            
            if (state == (WonderState)0)
            {
                GameManager.Instance.nowFruit = -2;
                GameManager.Instance.nextFruit = -2;
                FruitManager.Instance.OnFruitUpdated.Invoke();
            }
            if (state == (WonderState)2)
            {
                GameManager.Instance.ToggleView().Forget();
            }

            if (state == WonderState.BigSmall)
            {
                GameManager.Instance.nowFruit = -3;
                GameManager.Instance.nextFruit = -4;
                FruitManager.Instance.OnFruitUpdated.Invoke();

            }
            if (state == (WonderState) 4)
            {
                GameManager.Instance.ToggleDropperPos();
            }

            
            onCounterUpdate.Invoke();
            isWonder = true;
            
            if (state == (WonderState) 5)
            {
                FruitManager.Instance.RefreshEvoCircle();
            }
            
            viewWonderText.UpdateText();
        }

        public async UniTask RefreshWonderCount()
        {
            counter--;
            if (counter == 0)
            {
                DewonderAnimation().Forget();
                if (state == (WonderState) 2)
                {
                    GameManager.Instance.isWaiting = true;
                    await GameManager.Instance.ToggleView();
                }
                if (state == (WonderState) 4)
                {
                    GameManager.Instance.ToggleDropperPos();
                }

                isWonder = false;
                
                if (state == (WonderState) 5)
                {
                    FruitManager.Instance.RefreshEvoCircle();
                }
                
                viewWonderText.UpdateText();
            }
            onCounterUpdate.Invoke();
        }
    }
}