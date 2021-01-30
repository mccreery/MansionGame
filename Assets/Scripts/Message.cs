using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Message : MonoBehaviour
{
    public FloatGradient opacity;

    public Color color = Color.black;

    private float startTime = Mathf.NegativeInfinity;
    private float Opacity => opacity[Time.time - startTime];

    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        Color newColor = color;
        newColor.a *= Opacity;

        text.color = newColor;
    }

    public bool ShowMessage(string message)
    {
        if (Time.time < startTime || Time.time > startTime + opacity.TotalTime)
        {
            text.text = message;
            startTime = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }
}
