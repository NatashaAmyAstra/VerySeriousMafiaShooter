using UnityEngine;

public class GunVisual : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private void Update() {
        int lookDirection = (int) Mathf.Sign(transform.position.x - _playerTransform.position.x);
        transform.localScale = new Vector3(1, lookDirection, 1);
    }
}
