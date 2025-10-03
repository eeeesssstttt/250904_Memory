using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
// could also be called GameManager
{
    private float delayBeforeFaceDown;
    private List<CardBehavior> deck;
    private Color[] colors;
    private CardBehavior memorizedCard = null;
    private int matchesFound = 0;
    private VictoryManager victoryManager;

    public void Initialize(List<CardBehavior> deck, Color[] colors, float delayBeforeFaceDown, VictoryManager victoryManager)
    {
        this.delayBeforeFaceDown = delayBeforeFaceDown;
        this.deck = deck;
        this.colors = colors;
        this.victoryManager = victoryManager;
        int colorIndex;

        // Selecting colors and assigning them to pairs of cards

        int cardIndex;
        List<int> colorsAlreadyInGame = new();
        List<CardBehavior> cards = new(deck); // clones deck.

        for (int _ = 0; _ < deck.Count / 2; _++)
        // _ means we're not using it in the actual loop, just to count through things.
        {
            colorIndex = Random.Range(0, colors.Length);

            colorsAlreadyInGame.Add(colorIndex);

            while (colorsAlreadyInGame.Contains(colorIndex))
            {
                colorIndex = Random.Range(0, colors.Length);
            }

            colorsAlreadyInGame.Add(colorIndex);

            cardIndex = Random.Range(0, cards.Count);
            cards[cardIndex].Initialize(colors[colorIndex], colorIndex, this);
            cards.RemoveAt(cardIndex);

            cardIndex = Random.Range(0, cards.Count);
            cards[cardIndex].Initialize(colors[colorIndex], colorIndex, this);
            cards.RemoveAt(cardIndex);

            // prepping variables to compare cards and log matches
            memorizedCard = null;
            matchesFound = 0;
        }
    }

    public void CardIsClicked(CardBehavior card)
    {
        if (card.IsFaceUp) return;
        card.FaceUp();
        if (memorizedCard != null)
        // we use != instead of IS NOT because = operator in Unity was redefined for objects: 
        // object is null when it's null or when it's about to be destroyed.
        // this is only with Unity objects, and memoCard is a GameObject.
        {
            if (card.IndexColor == memorizedCard.IndexColor)
            {
                matchesFound++;
                if (matchesFound == deck.Count / 2)
                {
                    victoryManager.LaunchVictory();
                }
            }
            else
            {
                memorizedCard.FaceDown(delayBeforeFaceDown);
                card.FaceDown(delayBeforeFaceDown);
            }
            memorizedCard = null;
        }
        else
        {
            memorizedCard = card;
        }
    }
}