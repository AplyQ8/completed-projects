using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject undoButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject redoButton;
    [SerializeField] private GameObject finishButton;

    public void ReadyMethod()
    {
        undoButton.SetActive(true);
        nextButton.SetActive(true);
        redoButton.SetActive(true);
        finishButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
