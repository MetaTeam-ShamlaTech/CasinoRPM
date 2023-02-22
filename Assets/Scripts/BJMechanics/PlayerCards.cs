using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;

public class PlayerCards : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<Transform> playerCardsTransform;
    [SerializeField] private List<int> cardValuesList;
    [SerializeField] public TextMeshPro cardValueTxt;
    [SerializeField] private List<CardArranger> elements = new List<CardArranger>();
    [SerializeField] CardSetter cardSetter;
    private MeshRenderer meshRenderer;
    private Transform deckPos, dealPos;
    private float progress, elapsedTime;
    private int cardCount, cardTotal;
    private const float DURATION = 0.3f;
    private bool running;
    public List<PlayerCardsSet> cardsSet { get; set; }
    #endregion Variables

    public IEnumerator DealPlayerCards(Transform DeckPos, GameObject Card)
    {
        if(!running)
        {
            running = true;
            playerCardsTransform[^1].gameObject.SetActive(false);
            StartCoroutine(DealCards(DeckPos, transform, Card, playerCardsTransform));
            playerCardsTransform[^1].gameObject.SetActive(true);
            yield return null;
        }
    }

    private IEnumerator DealCards(Transform deckPos, Transform dealPos, GameObject Card, List<Transform> dealCards)
    {
        this.dealPos = dealPos;
        this.deckPos = deckPos;
        ArrangeCards(this.dealPos);
        meshRenderer = dealCards[^1].gameObject.GetComponent<MeshRenderer>();
        cardValuesList.Add(cardSetter.SetCardTextures(meshRenderer, cardsSet[cardCount].CardSuit, cardsSet[cardCount].CardValue));
        cardCount++;
        yield return StartCoroutine(PositionLerp(this.deckPos.position, this.dealPos.position, this.deckPos, Card, dealCards));
        yield return null;
    }

    private IEnumerator PositionLerp(Vector3 startPos, Vector3 endPos, Transform dealCardPos, GameObject Card, List<Transform> dealCards)
    {
        progress = elapsedTime = 0;
        while (progress < 1)
        {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / DURATION;
            dealCards[^1].SetPositionAndRotation(Vector3.Lerp(startPos, endPos, progress), Quaternion.Lerp(Quaternion.Euler(0, -30f, 0), Quaternion.Euler(0, -180, 0), progress));
            dealCards[^1].localScale = Vector3.Lerp(Vector3.one, Vector3.one * 10, progress);
            yield return null;
        }
        dealCards[^1].SetPositionAndRotation(endPos, Quaternion.Euler(0, -180, 0));
        dealCards[^1].localScale = Vector3.one * 10;
        running = false;
        StartCoroutine(RotateCard(dealCards[^1]));
        dealCards.Add(Instantiate(Card, dealCardPos).transform);
        elements.Add(dealCards[^2].GetComponent<CardArranger>());
        yield return null;
    }

    private IEnumerator RotateCard(Transform dealCard)
    {
        progress = elapsedTime = 0;
        while (progress < 1)
        {
            elapsedTime += Time.deltaTime;
            progress = elapsedTime / DURATION;
            dealCard.rotation = Quaternion.Lerp(Quaternion.Euler(0, -180, 0), Quaternion.Euler(180, -180, 0), progress);
            yield return null;
        }
        dealCard.rotation = Quaternion.Euler(180, -180, 0);
        cardTotal = CountCardTotal();
        yield return null;
    }

    private void ArrangeCards(Transform dealPos)
    {
        if (elements.Count < 1)
            return;

        dealPos.position = elements[^1].transform.position - (Vector3.right * 0.3f);
        dealPos.position = new Vector3(dealPos.position.x, dealPos.position.y, dealPos.position.z);
        foreach (var element in elements)
            element.ArrangeCards(0.3f);
    }

    private int CountCardTotal()
    {
        cardTotal += cardValuesList[^1];
        if (cardTotal > 21 && cardValuesList.Contains(11))
            cardValuesList[cardValuesList.IndexOf(11)] = 1;

        cardTotal = cardValuesList.Sum();

        if (cardTotal < 10)
            cardValueTxt.text = string.Concat("0", cardTotal.ToString());
        else
            cardValueTxt.text = cardTotal.ToString();
        return cardTotal;
    }

    public void RestoreCards()
    {
        playerCardsTransform[0].SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0, -30f, 0));
        playerCardsTransform[0].localScale = Vector3.one;

        for(int i = 1; i < playerCardsTransform.Count; )
        {
            Destroy(playerCardsTransform[i].gameObject);
            playerCardsTransform.RemoveAt(i);
        }

        dealPos.position = new Vector3(0.143f, dealPos.position.y, dealPos.position.z);
        cardCount = cardTotal = 0;
        cardValuesList.Clear();
        cardsSet.Clear();
        elements.Clear();
        cardValueTxt.text = "00";
    }
}