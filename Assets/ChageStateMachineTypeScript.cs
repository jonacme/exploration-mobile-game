using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChageStateMachineTypeScript : MonoBehaviour
{
    private UIDocument ui;
    private Button button;

    void Start()
    {
        ui = GetComponent<UIDocument>();
        button = ui.rootVisualElement.Q<Button>("ChangeState");
        button.clicked += ChangeState;
    }

    private void Update()
    {
        if (!LoadManager.instance.useClassesInsteadOfIf)
            button.text = "If based";
        else
            button.text = "Class based";
    }

    public static void ChangeState()
    {
        LoadManager.instance.useClassesInsteadOfIf = !LoadManager.instance.useClassesInsteadOfIf;
    }
}
