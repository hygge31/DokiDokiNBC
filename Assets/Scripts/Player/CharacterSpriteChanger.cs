using UnityEngine;

public class CharacterSpriteChanger : MonoBehaviour
{
    [SerializeField] private SpriteRenderer headRenderer; // 머리 스프라이트 렌더러
    [SerializeField] private SpriteRenderer bodyRenderer; // 몸통 스프라이트 렌더러

    // 머리와 몸통에 대한 데이터를 받아와서 적용하는 메서드
    public void ApplyBodyPartData(BodyPartData headData, BodyPartData bodyData)
    {
        if (headRenderer != null && headData != null)
        {
            headRenderer.sprite = headData.sprite;
            headRenderer.GetComponent<Animator>().runtimeAnimatorController = headData.animator;
        }
        if (bodyRenderer != null && bodyData != null)
        {
            bodyRenderer.sprite = bodyData.sprite;
            bodyRenderer.GetComponent<Animator>().runtimeAnimatorController = bodyData.animator;
        }
    }
}

//public class CharacterSpriteChanger : MonoBehaviour
//{
//    [SerializeField] private SpriteRenderer headRenderer; // 머리 스프라이트 렌더러
//    [SerializeField] private SpriteRenderer bodyRenderer; // 몸통 스프라이트 렌더러

//    // 머리와 몸통에 대한 데이터를 받아와서 적용하는 메서드
//    public void ApplyBodyPartData(BodyPartData headData, BodyPartData bodyData)
//    {
//        if (headRenderer != null && headData != null)
//        {
//            Sprite headSprite = Resources.Load<Sprite>(headData.spritePath);
//            if (headSprite != null)
//            {
//                headRenderer.sprite = headSprite;
//                headRenderer.GetComponent<Animator>().runtimeAnimatorController = headData.LoadAnimator();
//            }
//            else
//            {
//                Debug.LogError("머리 데이터 로드 오류: " + headData.spritePath);
//            }
//        }
//        if (bodyRenderer != null && bodyData != null)
//        {
//            Sprite bodySprite = Resources.Load<Sprite>(bodyData.spritePath);
//            if (bodySprite != null)
//            {
//                bodyRenderer.sprite = bodySprite;
//                bodyRenderer.GetComponent<Animator>().runtimeAnimatorController = bodyData.LoadAnimator();
//            }
//            else
//            {
//                Debug.LogError("몸통 데이터 로드 오류: " + bodyData.spritePath);
//            }
//        }
//    }
//}