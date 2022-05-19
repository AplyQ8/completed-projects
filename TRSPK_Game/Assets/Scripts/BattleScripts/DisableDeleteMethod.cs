using System;
using UnityEngine;

public class DisableDeleteMethod: MonoBehaviour
{
    private GameObject _playerSide;
    private GameObject _playerField;

    private void Awake()
    {
        _playerSide = GameObject.Find("PlayerSide");
        _playerField = _playerSide.transform.GetChild(0).GetChild(0).gameObject;

        DisableComponents(_playerField);
    }

    private void DisableComponents(GameObject playerField)
    {
        for (int i = 0; i < playerField.transform.childCount; i++)
        {
            if (playerField.transform.GetChild(i).childCount > 0)
            {
                playerField.transform.GetChild(i).GetChild(0).GetComponent<DeleteFromSlotScript>().enabled = false;
            }
        }
    }
}