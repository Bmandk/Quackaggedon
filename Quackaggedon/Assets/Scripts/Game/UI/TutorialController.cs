using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public enum TutorialArrowDirection
{
    Up,
    Down,
    Left,
    Right
}

public static class TutorialController
{
    private static GameObject tutorialArrowPrefab;
    private static GameObject tutorialArrowInstance;
    
    private static Transform canvasTransform;
    
    public static int TutorialIndex;

    public static void Reset()
    {
        tutorialArrowPrefab = References.Instance.tutorialArrowPrefab;
        canvasTransform = GameObject.FindObjectOfType<Canvas>().transform;
        TutorialIndex = 0;
    }
    
    public static void ShowTutorialArrowUI(Transform target, Vector2 offset, TutorialArrowDirection direction, int tutorialIndex, bool isWorldPosition)
    {
        if (TutorialIndex != tutorialIndex)
        {
            return;
        }
        
        Quaternion rotation = Quaternion.identity;
        
        switch (direction)
        {
            case TutorialArrowDirection.Up:
                rotation = Quaternion.Euler(0, 0, 0);
                break;
            case TutorialArrowDirection.Down:
                rotation = Quaternion.Euler(0, 0, 180);
                break;
            case TutorialArrowDirection.Left:
                rotation = Quaternion.Euler(0, 0, 90);
                break;
            case TutorialArrowDirection.Right:
                rotation = Quaternion.Euler(0, 0, 270);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
        
        tutorialArrowInstance = GameObject.Instantiate(tutorialArrowPrefab, target.position, rotation, canvasTransform);
        tutorialArrowInstance.transform.SetSiblingIndex(6);
        TutorialArrow tutorialArrow = tutorialArrowInstance.GetComponent<TutorialArrow>();
        tutorialArrow.target = target;
        //tutorialArrow.offset = offset;
        tutorialArrow.isWorldPosition = isWorldPosition;
    }
    
    public static void HideTutorialArrow()
    {
        Object.Destroy(tutorialArrowInstance);
        TutorialIndex++;
    }
}
