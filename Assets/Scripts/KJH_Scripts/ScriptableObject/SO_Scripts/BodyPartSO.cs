using UnityEngine;

[CreateAssetMenu(fileName = "BodyPartData", menuName = "KJH_SO/Body Part Data", order = 1)]

public class BodyPartSO : ScriptableObject
{
    [Header("Sprites")]
    public Sprite sprite; // 몸통 또는 머리 스프라이트
    [Header("Animator")]
    public RuntimeAnimatorController animator; // 애니메이션 컨트롤러
}