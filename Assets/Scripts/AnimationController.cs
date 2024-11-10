using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animation anim;

    private void Start()
    {
        anim.Play();
        anim.wrapMode = WrapMode.Once;
        anim.clip.wrapMode = WrapMode.Once;
    }
}