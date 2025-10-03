using UnityEngine;

public class ColorGenerator : MonoBehaviour
// Generates color pool to choose from when assigning card colors.
{
    int numberOfValues;
    float[] values;
    float[] hues = new float[] { 25, 50, 75 };

    public void Initialize(int numberOfValues)
    {
        this.numberOfValues = numberOfValues;
    }

    public Color[] GeneratePalette()
    {
        Color[] colors = new Color[numberOfValues * hues.Length];
        values = new float[numberOfValues];
        int colorIndex = 0;
        for (int i = 0; i < numberOfValues; i++)
        {
            values[i] = i * 1 / (float)numberOfValues;
        }
        for (int j = 0; j < hues.Length; j++)
        {
            for (int k = 0; k < values.Length; k++)
            {
                colors[colorIndex] = Color.HSVToRGB(hues[j], 1, values[k]);
                //DOESN'T WORK - Check how to properly create color 
                colorIndex++;
                Debug.Log(colors[colorIndex]);
            }
        }
        return colors;
    }
}
