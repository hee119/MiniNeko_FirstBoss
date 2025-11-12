using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public GameObject TextDisplay;
    public Color ShadowsColor;
    public Color TextColor;
    private static Color g_ShadowsColor;
    private static Color g_TextColor;
    private static GameObject g_textDisplay;
    private static bool isSet;

    public static void SpawnText(string Text, Vector3 position) 
    {
        var textDisplayScript = Instantiate(g_textDisplay, position, Quaternion.identity).GetComponent<TextDisplay>();
        textDisplayScript.Set(Text, g_TextColor, g_ShadowsColor);
    }

    public static void SpawnText(string Text, Vector3 position, Color textColor, Color outlineColor)
    {
        var textDisplayScript = Instantiate(g_textDisplay, position, Quaternion.identity).GetComponent<TextDisplay>();
        textDisplayScript.Set(Text, textColor, outlineColor);
    }

    void Start()
    {
        if (isSet) { return; }

        g_textDisplay = TextDisplay;
        g_ShadowsColor = ShadowsColor;
        g_TextColor = TextColor;

        isSet = true;
    }
}
