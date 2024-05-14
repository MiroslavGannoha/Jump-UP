using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class CustomProgressBar : BindableElement
{
    private float _val;
    private float _cost;

    [Range(0, 1)]
    [UxmlAttribute, CreateProperty]
    public float val
    {
        get => _val;
        set
        {
            _val = value;
            UpdateProgressBar();
        }
    }

    [Range(0, 1)]
    [UxmlAttribute, CreateProperty]
    public float cost
    {
        get => _cost;
        set
        {
            _cost = value;
            UpdateProgressBar();
        }
    }

    private VisualElement progressElem => this.Q<VisualElement>("cpb-progress");
    private VisualElement costElem => this.Q<VisualElement>("cpb-cost");
    private VisualElement leftoverElem => this.Q<VisualElement>("cpb-leftover");

    public CustomProgressBar()
    {
        // RegisterCallback<ChangeEvent<>>(e =>
        // {
        //     Debug.Log("CHANGE");
        //     Debug.Log(e.newValue);
        // });
        // RegisterCallback<AttachToPanelEvent>(e =>
        // {
        //     Debug.Log("A");
        // });
        // RegisterCallback<DetachFromPanelEvent>(e =>
        // {
        //     Debug.Log("D");
        // });
    }

    private void UpdateProgressBar()
    {
        if (progressElem != null)
        {
            progressElem.style.height = new StyleLength(new Length(_val * 100, LengthUnit.Percent));
        }
        if (costElem != null)
        {
            costElem.EnableInClassList("rounded-top", _val > 0.99);
            costElem.EnableInClassList("rounded-bottom", _cost > 0.99);
            costElem.style.height = new StyleLength(new Length(_cost * 100, LengthUnit.Percent));
        }
        if (costElem != null) {
            leftoverElem.EnableInClassList("rounded-top", _val > 0.99 && _cost == 0);
        }

    }
}
