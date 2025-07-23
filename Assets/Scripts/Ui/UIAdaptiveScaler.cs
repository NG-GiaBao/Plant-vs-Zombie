using UnityEngine;
using UnityEngine.UI;

public class UIAdaptiveScaler : MonoBehaviour
{
    public CanvasScaler canvasScaler;
    private int lastScreenWidth;
    private int lastScreenHeight;

    void Start()
    {
        if (canvasScaler == null)
            canvasScaler = GetComponent<CanvasScaler>();

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;
        Debug.Log($"{lastScreenWidth} and {lastScreenHeight}");

        UpdateCanvasScaler();
    }

    void Update()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {

            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            Debug.Log($"{lastScreenWidth} and {lastScreenHeight}");
            UpdateCanvasScaler();
        }
    }

    void UpdateCanvasScaler()
    {
        float refAspect = (float)canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;
        float screenAspect = (float)Screen.width / Screen.height;
        float ratio = refAspect / screenAspect;

        canvasScaler.matchWidthOrHeight = ratio <= 1f ? 1f : 0f;
        // 1 = ưu tiên căn chỉnh theo height (thường cho tablet)
        // 0 = ưu tiên căn chỉnh theo width (thường cho điện thoại)
    }
}
