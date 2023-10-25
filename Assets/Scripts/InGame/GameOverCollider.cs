using UnityEngine;

namespace InGame
{
    public class GameOverCollider : MonoBehaviour
    {
        private void OnTriggerStay(Collider collider)
        {
            if (!GameManager.Instance.OnGame) return;
            if (!collider.gameObject.CompareTag("Fruit")) return;
            if (GameManager.Instance.isWaiting) return;
            GameManager.Instance.GameOver();
        }
    }
}
