using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTime : MonoBehaviour
{
    [SerializeField] Text dialogText;

    public void Setdialog(string dialog)
    {
        dialogText.text = dialog;
    }
}
