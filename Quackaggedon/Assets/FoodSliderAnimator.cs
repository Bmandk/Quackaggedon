using DuckClicker;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodSliderAnimator : MonoBehaviour
{
    public Slider progressSlider;
    public Animator _sliderAnimator;
    public TextMeshProUGUI _progressText;

    public DuckFeeder duckFeeder;

    public void UpdateSliderProgress()
    {
        if (gameObject.activeInHierarchy == false)
            return;

        var newVal = duckFeeder.FoodThrown / duckFeeder.NextDuckCost;
        if (newVal == 0)
        {
            FinishThisProgressLevel();
        }
        else
        {
            SetProgress(newVal);
        }

        _sliderAnimator.SetTrigger("Pulse");
        _progressText.text = $"{NumberUtility.FormatNumber(duckFeeder.FoodThrown)}/{NumberUtility.FormatNumber(duckFeeder.NextDuckCost)}";
    }

    private float _targetProgress;
    private float _timeScale = 0;
    private bool _lerpingProgress = false;
    private float _fillSpeed = 3;
    private Coroutine _progressLerp;

    public void FinishThisProgressLevel()
    {
        _timeScale = 0;

        if (_progressLerp != null)
        {
            StopCoroutine(_progressLerp);
            _lerpingProgress = false;
        }
        if (!_lerpingProgress)
            _progressLerp = StartCoroutine(LerpProgressToFinish());

    }

    public void SetProgress(double progress)
    {
        _targetProgress = (float)progress;
        _timeScale = 0;

        if (_progressLerp != null)
        {
            StopCoroutine(_progressLerp);
            _lerpingProgress = false;
        }
        if (!_lerpingProgress)
        {
            _progressLerp = StartCoroutine(LerpProgress());
        }

    }

    private IEnumerator LerpProgress()
    {
        float startHealth = progressSlider.value;

        _lerpingProgress = true;

        while (_timeScale < 1)
        {
            _timeScale += Time.deltaTime * _fillSpeed;
            progressSlider.value = Mathf.Lerp(startHealth, _targetProgress, _timeScale);
            yield return null;
        }
        _lerpingProgress = false;
    }

    private IEnumerator LerpProgressToFinish()
    {
        float startHealth = progressSlider.value;

        _lerpingProgress = true;

        while (_timeScale < 1)
        {
            _timeScale += Time.deltaTime * _fillSpeed;
            progressSlider.value = Mathf.Lerp(startHealth, 1, _timeScale);

            yield return null;
        }

        _sliderAnimator.SetTrigger("Fill");
        progressSlider.value = 0;
        _lerpingProgress = false;
    }

}
