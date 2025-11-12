using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    [SerializeField]
    private Text displayedText;
    [SerializeField]
    private Shadow[] shadows;

    public enum Animation
    {
        Default,
        Sleepy,
        Appear,
        ScaleAndAppear
    }

    public bool AutoDestroy;
    public float DestroyDelay;

    float timer;
    Animator animator;

    private Color shadowsColor
    { set 
        {
            foreach (var shadow in shadows)
            {
                shadow.effectColor = value;
            }
        }
    }
    private Color textColor { set { displayedText.color = value; } }
    public string text { set { displayedText.text = value; } }

    public void Set(string displayedText, Color textColor, Color shadowColor, Animation animation = Animation.Default)
    {
        text = displayedText;
        shadowsColor = shadowColor;
        this.textColor = textColor;

        animator = GetComponent<Animator>();

        if (animator != null && animation != Animation.Default)
            animator.Play(animation.ToString());
    }

    private void Update()
    {
        if (!AutoDestroy)
        {
            enabled = false;
            return;
        }

        timer += Time.deltaTime;

        if (timer > DestroyDelay)
        {
            timer = 0;
            Destroy(gameObject);
        }
    }
}
