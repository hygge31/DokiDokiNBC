using UnityEngine;

[CreateAssetMenu(fileName = "BodyPartData", menuName = "Body Part Data", order = 1)]
//public class BodyPartData : ScriptableObject
//{
//    public string spritePath; // 스프라이트 리소스 파일 경로
//    public string animatorPath; // 애니메이터 리소스 파일 경로

//    public RuntimeAnimatorController LoadAnimator()
//    {
//        return Resources.Load<RuntimeAnimatorController>(animatorPath);
//    }
//}

public class BodyPartData : ScriptableObject
{
    [Header("Sprites")]
    public Sprite sprite; // 몸통 또는 머리 스프라이트
    [Header("Animator")]
    public RuntimeAnimatorController animator; // 애니메이션 컨트롤러
}