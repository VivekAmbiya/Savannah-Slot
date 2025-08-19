using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Reel : MonoBehaviour
{
   public RectTransform reelContainer;        // Parent container holding symbols
    public float symbolHeight;          // Height of one symbol
    public float spinSpeed = 800f;             // Pixels per second
    public float spinDuration = 2f;            // How long the reel spins
    public List<RectTransform> symbols;        // List of symbol RectTransforms
    public int visibleCount;               // Number of symbols visible on screen

    private float elapsedTime = 0f;
    private bool isSpinning = false;
    private Vector2 startPos;

    void Awake()
    {
        reelContainer = GetComponent<RectTransform>();
        startPos = reelContainer.anchoredPosition;
    }

    public void Spin()
    {
        elapsedTime = 0f;
        isSpinning = true;
        DOTween.Kill(reelContainer); // Cancel any previous tweens

        // Start Update loop for spinning
        StartCoroutine(SpinRoutine());
    }

    private System.Collections.IEnumerator SpinRoutine()
    {
        while (elapsedTime < spinDuration)
        {
            float moveDelta = spinSpeed * Time.deltaTime;
            reelContainer.anchoredPosition -= new Vector2(0, moveDelta);

            // Recycle symbols that move off-screen
            for (int i = 0; i < symbols.Count; i++)
            {
                if (symbols[i].anchoredPosition.y < -symbolHeight * visibleCount)
                {
                    float highestY = GetHighestSymbolY();
                    symbols[i].anchoredPosition = new Vector2(
                        symbols[i].anchoredPosition.x,
                        highestY + symbolHeight
                    );
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap to final symbol position
        SnapToClosestSymbol();
        isSpinning = false;
    }

    private float GetHighestSymbolY()
    {
        float highest = float.MinValue;
        foreach (var symbol in symbols)
        {
            if (symbol.anchoredPosition.y > highest)
                highest = symbol.anchoredPosition.y;
        }
        return highest;
    }

    private void SnapToClosestSymbol()
    {
        // Find the symbol closest to center and align it
        RectTransform closestSymbol = null;
        float closestDistance = float.MaxValue;

        foreach (var symbol in symbols)
        {
            float distance = Mathf.Abs(symbol.position.y - reelContainer.position.y);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestSymbol = symbol;
            }
        }

        if (closestSymbol != null)
        {
            float offset = closestSymbol.anchoredPosition.y;
            float targetY = -offset;

            reelContainer.DOAnchorPosY(targetY, 0.3f)
                .SetEase(Ease.OutCubic);
        }
    }
}