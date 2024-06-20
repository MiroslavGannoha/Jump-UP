using UnityEngine;

public class BackgroundParalaxUI : MonoBehaviour
{
    public Transform playerCam;
    public RectTransform[] backgrounds;
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
        moveBackgrounds();
    }

    private void updateDelta()
    {
        delta = (Vector2)playerCam.position - lastPos;

        lastPos = playerCam.position;
    }

    private void moveBackgrounds()
    {
        for (int i = 0; i < backgrounds.Length; ++i)
        {
            float offsetMultiplier = offsetMultipliers.Length > i ? offsetMultipliers[i] : 1.0f;
            for (int j = 0; j < backgrounds[i].childCount; ++j)
            {
                var child = backgrounds[i].GetChild(j);
                var childTransform = child.GetComponent<RectTransform>();
                childTransform.anchoredPosition = new Vector2(
                    childTransform.anchoredPosition.x - delta.x * offsetMultiplier,
                    childTransform.anchoredPosition.y
                );
            }
        }
    }
}
