using UnityEngine;
using UnityEngine.UI;

namespace LastShift
{
    public class InteractionPrompt : MonoBehaviour
    {
        [Header("Styling")]
        [SerializeField] private string prefix = "[E]";
        [SerializeField] private int fontSize = 24;
        [SerializeField] private float verticalOffset = -150f;
        [SerializeField] private Color textColor = new Color(1f, 1f, 1f, 0.92f);
        [SerializeField] private Color outlineColor = new Color(0f, 0f, 0f, 0.9f);

        private Text label;
        private string currentText = string.Empty;
        private bool initialized;

        public bool IsVisible => initialized && gameObject.activeSelf;

        private void Awake()
        {
            BuildUI();
            Hide();
        }

        public void Show(string text)
        {
            if (!initialized)
                BuildUI();

            if (currentText == text && gameObject.activeSelf)
                return;

            currentText = text;
            label.text = BuildDisplay(text);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private string BuildDisplay(string text)
        {
            if (string.IsNullOrEmpty(prefix))
                return text;

            return prefix + " " + text;
        }

        private void BuildUI()
        {
            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            scaler.matchWidthOrHeight = 0.5f;

            GameObject labelGameObject = new GameObject(
                "Label",
                typeof(RectTransform),
                typeof(CanvasRenderer),
                typeof(Text),
                typeof(Outline));
            labelGameObject.transform.SetParent(transform, false);

            RectTransform rect = labelGameObject.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = new Vector2(0f, verticalOffset);
            rect.sizeDelta = new Vector2(800f, 64f);

            label = labelGameObject.GetComponent<Text>();
            label.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            label.fontSize = fontSize;
            label.alignment = TextAnchor.MiddleCenter;
            label.horizontalOverflow = HorizontalWrapMode.Overflow;
            label.verticalOverflow = VerticalWrapMode.Overflow;
            label.color = textColor;
            label.raycastTarget = false;
            label.text = string.Empty;

            Outline outline = labelGameObject.GetComponent<Outline>();
            outline.effectColor = outlineColor;
            outline.effectDistance = new Vector2(1.5f, -1.5f);
            outline.useGraphicAlpha = true;

            initialized = true;
        }
    }
}
