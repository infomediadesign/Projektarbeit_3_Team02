using Unity.VisualScripting;
using UnityEngine;

public class AnimationPhases 
{
    public AnimationClip startAnim { get; private set; }
    public AnimationClip loopAnim { get; private set; }
    public AnimationClip endAnim { get; private set; }
    public AnimationPhases(AnimationClip start, AnimationClip loop, AnimationClip end)
    {
        startAnim = start;
        loopAnim = loop;
        endAnim = end;
    }
}
