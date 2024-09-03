using UnityEngine;

public class AnimationOffset : MonoBehaviour
{
    public float animationStartDelay = 0.5f; // Delay before the animation starts

    void Start()
    {
        // Get the Animator component attached to this GameObject
        Animator animator = GetComponent<Animator>();

        // Offset the animation by pausing the Animator for the specified time
        animator.enabled = false; // Disable Animator initially
        StartCoroutine(StartAnimationWithDelay(animator));
    }

    System.Collections.IEnumerator StartAnimationWithDelay(Animator animator)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(animationStartDelay);

        // Enable the Animator after the delay
        animator.enabled = true;
    }
}
