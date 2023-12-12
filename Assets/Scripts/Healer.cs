using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private float _speed;

    private int _addingHealth = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.AddHealth(_addingHealth);
            gameObject.SetActive(false);
        }
    }
}