using UnityEngine;

public class BackgroundParalaxUI : MonoBehaviour
{
    public Transform playerCam;
    public RectTransform[] layers;
    public float[] offsetMultipliers;

    private Vector2 lastPos = Vector2.zero;
    private Vector2 delta = Vector2.zero;

    void Start()
    {
        lastPos = playerCam.position;
    }

    void Update()
    {
        updateDelta();
        moveBackgroundLayers();
    }

    private void updateDelta()
    {
        delta = (Vector2)playerCam.position - lastPos;

        lastPos = playerCam.position;
    }

    private void moveBackgroundLayers()
    {
        if (delta.x == 0)
        {
            return;
        }
        bool isGoingForward = delta.x > 0;
        bool isGoingBackwards = delta.x < 0;
        for (int i = 0; i < layers.Length; ++i)
        {
            float offsetMultiplier = offsetMultipliers.Length > i ? offsetMultipliers[i] : 1.0f;
            RectTransform lastChild = null;
            RectTransform firstChild = null;
            for (int j = 0; j < layers[i].childCount; ++j)
            {
                var childTransform = layers[i].GetChild(j).GetComponent<RectTransform>();
                if (lastChild == null)
                {
                    lastChild = childTransform;
                }
                else if (childTransform.anchorMax.x > lastChild.anchorMax.x)
                {
                    lastChild = childTransform;
                }

                if (firstChild == null)
                {
                    firstChild = childTransform;
                }
                else if (childTransform.anchorMin.x < firstChild.anchorMin.x)
                {
                    firstChild = childTransform;
                }
            }

            for (int j = 0; j < layers[i].childCount; ++j)
            {
                var childTransform = layers[i].GetChild(j).GetComponent<RectTransform>();
                float minX = childTransform.anchorMin.x;
                float maxX = childTransform.anchorMax.x;
                if (isGoingForward && childTransform.anchorMax.x < 0)
                {
                    var childDelta = childTransform.anchorMax.x - childTransform.anchorMin.x;
                    minX = lastChild.anchorMax.x;
                    maxX = lastChild.anchorMax.x + childDelta;
                }
                else if (isGoingBackwards && childTransform.anchorMin.x > 1)
                {
                    var childDelta = childTransform.anchorMax.x - childTransform.anchorMin.x;
                    maxX = firstChild.anchorMin.x;
                    minX = firstChild.anchorMin.x - childDelta;
                }
                float offset = delta.x * offsetMultiplier;
                childTransform.anchorMin = new Vector2(minX - offset, childTransform.anchorMin.y);
                childTransform.anchorMax = new Vector2(maxX - offset, childTransform.anchorMax.y);
            }
        }
    }
}
