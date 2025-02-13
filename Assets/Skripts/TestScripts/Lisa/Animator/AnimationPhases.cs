using Unity.VisualScripting;
using UnityEngine;

public class AnimationPhases : MonoBehaviour
{
    public AnimationClip startAnim { get; }
    public AnimationClip loopAnim { get; }
    public AnimationClip endAnim { get; }
    public AnimationPhases(AnimationClip start, AnimationClip loop, AnimationClip end)
    {
        startAnim = start;
        loopAnim = loop;
        endAnim = end;
    }
}
