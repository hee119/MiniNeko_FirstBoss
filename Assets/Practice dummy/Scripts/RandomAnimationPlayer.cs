using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationPlayer : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AnimationSet[] animationSets;

    Dictionary<string, string[]> animations = new();

    void Start()
    {
        foreach (var animationSet in animationSets)
        {
            animations.Add(animationSet.Name, animationSet.Animations);
        }
    }

    /// <summary>
    /// Play Random animaton from collection specified in Inspector
    /// </summary>
    /// <param name="animationSetName">Name of animation set from which random animation will be played</param>
    public void Play(string animationSetName)
    {
        // Check if animation name exist
        if (!animations.ContainsKey(animationSetName))
        {
            Debug.LogWarning($"No animation set named {animationSetName} is declared in inspector of {gameObject.name}!");
            return;
        }

        string[] setOfAnimations = animations[animationSetName];
        int pickedIndex = Random.Range(0, setOfAnimations.Length);
        string pickedAnimation = setOfAnimations[pickedIndex];

        //animator.enabled = false;
        //animator.enabled = true;

        animator.Play(pickedAnimation);
    }

    [System.Serializable]
    public class AnimationSet
    {
        public string Name;
        public string[] Animations;
    }

}
