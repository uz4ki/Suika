using Cysharp.Threading.Tasks;
using DefaultNamespace;

namespace InGame
{
    public enum WonderState
    {
        CherryShotGun = 0,
        Driving = 1,
        TopView = 2,
        NoConvert = 3,
        FromBottom = 4,
        NoControl = 5,
        EvolutionShuffle = 6
    }
    public class WonderManager : SingletonMonoBehaviour<WonderManager>
    {
        public bool isWonder;
        public async UniTask WonderAnimation()
        {
            GameManager.Instance.isWaiting = true;
            
            
            GameManager.Instance.isWaiting = false;
        }
    }
}