using UnityEngine;

namespace DefaultNamespace
{
    public class Dropper : MonoBehaviour
    {
        [SerializeField] private Transform point;
        public Vector3 Position => point.position;
        [SerializeField] private float moveLimit;
        [SerializeField] private float moveSpeed;

        public void MoveDropper(float diff)
        {
            if (Mathf.Abs(point.localPosition.x + diff) > moveLimit) return;
            
            point.localPosition += diff * moveSpeed * Vector3.right;
        }
    }
}