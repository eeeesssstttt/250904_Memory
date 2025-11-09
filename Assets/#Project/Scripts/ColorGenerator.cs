using UnityEngine;

public class ColorGenerator : MonoBehaviour
// Generates color pool
{
    private int numberOfHues;
    private float[] hues;
    private float[] values = new float[] { 0.25f, 0.50f, 0.75f };
    private Color[] colors;

    public void Initialize(int numberOfHues)
    {
        this.numberOfHues = numberOfHues;
        colors = GeneratePalette();
    }

    public Color[] GeneratePalette()
    {
        Color[] colors = new Color[numberOfHues * values.Length];

        hues = new float[numberOfHues];

        int colorIndex = 0;

        for (int i = 0; i < numberOfHues; i++)
        {
            hues[i] = i * 1 / (float)numberOfHues;
        }

        for (int j = 0; j < hues.Length; j++)
        {
            for (int k = 0; k < values.Length; k++)
            {
                colors[colorIndex] = Color.HSVToRGB(hues[j], 1f, values[k]);
                colorIndex++;
            }
        }
        return colors;
    }
}