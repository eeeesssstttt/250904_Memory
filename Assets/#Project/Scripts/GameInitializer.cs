using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    const float CARD_SIZE = 1.0f;

    [Header("Cards")]
    [Space]

    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 3;
    [SerializeField] private float gap = 0.5f;
    [SerializeField] private CardBehavior cardPrefab;
    private List<CardBehavior> deck = new();
    [SerializeField] private CardManager cardManager;
    [SerializeField] private ColorGenerator colorGenerator;
    [SerializeField] private int numberOfValues = 10;
    [SerializeField] private Color[] colors;
    [SerializeField] private float delayBeforeFaceDown = 1f;

    [Header("Victory")]
    [Space]

    [SerializeField] private VictoryManager victoryManager;
    [SerializeField] private string victoryScene;
    [SerializeField] private float delayBeforeLoadScene = 1f;

    private void Start()
    {
        if (rows * columns % 2 != 0)
        {
            Debug.LogError("The number of cards must be even.");
            return; //Start does not continue since there's an odd number of cards.
        }

        if (colors.Length < rows * columns / 2)
        {
            Debug.LogError("There must be enough colors to fill all cards.");
            return;
        }
        ObjectCreation();
        ObjectInitialization();
    }

    // We could have a separate object verification method.

    // private void ObjectVerification()
    // {
    //             if (rows * columns % 2 != 0)
    //     {
    //         Debug.LogError("The number of cards must be even.");
    //         return; //Start does not continue since there's an odd number of cards.
    //     }

    //     if (colors.Length < rows * columns / 2)
    //     {
    //         Debug.LogError("There must be enough colors to fill all cards.");
    //         return;
    //     }
    //     ObjectCreation();
    // }

    private void ObjectCreation()
    {
        Vector3 position;
        for (float x = 0.0f; x < columns * (CARD_SIZE + gap); x += CARD_SIZE + gap)
        {
            for (float z = 0.0f; z < rows * (CARD_SIZE + gap); z += CARD_SIZE + gap)
            {
                position = transform.position + Vector3.right * x + Vector3.forward * z;
                deck.Add(Instantiate(cardPrefab, position, Quaternion.identity));
            }
        }
        cardManager = Instantiate(cardManager);
        victoryManager = Instantiate(victoryManager);
        colorGenerator = Instantiate(colorGenerator);
    }

    private void ObjectInitialization()
    {
        colorGenerator.Initialize(numberOfValues);
        this.colors = colorGenerator.GeneratePalette();
        cardManager.Initialize(deck, colors, delayBeforeFaceDown, victoryManager);
        victoryManager.Initialize(victoryScene, delayBeforeLoadScene);
    }
}