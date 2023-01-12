using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIPanel : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private bool _hideInAwake;

    [Header("Main")]
    [SerializeField] private GameObject MainGUI;

    protected virtual void Awake()
    {
        if (_hideInAwake)
        {
            Hide();
        }
    }

    public virtual void Show()
    {
        MainGUI.SetActive(true);
    }

    public virtual void Hide()
    {
        MainGUI.SetActive(false);
    }
}
