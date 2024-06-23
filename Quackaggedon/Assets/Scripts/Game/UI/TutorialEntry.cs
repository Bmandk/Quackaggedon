using UnityEngine;

public class TutorialEntry : MonoBehaviour
{
    public int tutorialIndex;
    public bool playOnStart = true;
    public TutorialArrowDirection direction;
    public Vector2 offset = Vector2.up * 100f;
    public bool isWorldPosition;

    public void Start()
    {
        if (playOnStart)
        {
            ShowTutorialArrow();
        }
    }
    
    public void ShowTutorialArrow()
    {
        TutorialController.ShowTutorialArrowUI(transform, offset, direction, tutorialIndex, isWorldPosition);
    }
}