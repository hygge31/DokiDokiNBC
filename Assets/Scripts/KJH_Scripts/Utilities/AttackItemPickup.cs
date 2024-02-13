using UnityEngine;
using static UnityEditor.Progress;

public class AttackItemPickup : MonoBehaviour
{
    private IAttack item; // 전달할 아이템

    private void Awake()
    {
        item = GetComponent<IAttack>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerAttackController playerController = other.GetComponent<PlayerAttackController>();
            playerController.EquipItem(item);
            Destroy(gameObject);
        }
    }
}
