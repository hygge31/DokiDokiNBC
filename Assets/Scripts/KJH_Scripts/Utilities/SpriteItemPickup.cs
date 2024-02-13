using UnityEngine;

public class SpriteItemPickup : MonoBehaviour
{
    [SerializeField] private BodyPartData headData; // 머리 데이터
    [SerializeField] private BodyPartData bodyData; // 몸통 데이터

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterSpriteChanger characterSpriteChanger = collision.GetComponent<CharacterSpriteChanger>();
            if (characterSpriteChanger != null)
            {
                characterSpriteChanger.ApplyBodyPartData(headData, bodyData);
            }
            Destroy(gameObject);
        }
    }
}

