using System.Collections.Generic;
using UnityEngine.UIElements;

class CustomProgressBar : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CustomProgressBar, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits
    {

        UxmlFloatAttributeDescription m_Float = new UxmlFloatAttributeDescription { name = "value" };

        public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
        {
            get { yield break; }
        }

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            ((CustomProgressBar)ve).value = m_Float.GetValueFromBag(bag, cc);
        }

    }
        public float value { get; set; }
}