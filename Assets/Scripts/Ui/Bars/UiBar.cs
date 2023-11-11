using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private float fillDurations;
    [SerializeField] private Ease fillEase;
    public void UpdateBar(int currentValue, int maxValue)
    {
        float currentProgress = (float)currentValue / (float)maxValue;
        progressBar.DOFillAmount(currentProgress, fillDurations).SetEase(fillEase);
    }
}
