using UnityEngine;

public class SpriteItemPickup : MonoBehaviour
{
    [SerializeField] private BodyPartSO headData; // 머리 데이터
    [SerializeField] private BodyPartSO bodyData; // 몸통 데이터

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerSpriteChanger characterSpriteChanger = collision.GetComponent<PlayerSpriteChanger>();
            if (characterSpriteChanger != null)
            {
                characterSpriteChanger.ApplyBodyPartData(headData, bodyData);
            }
            Destroy(gameObject);
        }
    }
}

