using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private float initWidth;
	private Vector2 initPos;
	private RectTransform rectTransform;
	private CanvasRenderer canvasRenderer;

	public Color healthyColor = new Color(0.8f, 1.0f, 0.85f);
	public Color damagedColor = new Color(1.0f, 0.1f, 0.2f);

	void Start() {
		canvasRenderer = GetComponent<CanvasRenderer>();
		rectTransform = GetComponent<RectTransform>();
		initWidth = rectTransform.rect.width;
		initPos = rectTransform.anchoredPosition;
		UpdateHealth(100);
	}

	public void UpdateHealth(float health) {
		rectTransform.sizeDelta = new Vector2(initWidth * health / 100.0f, rectTransform.sizeDelta.y);
		rectTransform.anchoredPosition = new Vector2(initPos.x - initWidth * (100 - health) / 200, initPos.y);

		canvasRenderer.SetColor(new Color(
			(health*healthyColor.r + (100 - health)*damagedColor.r)/100,
			(health*healthyColor.g + (100 - health)*damagedColor.g)/100,
			(health*healthyColor.b + (100 - health)*damagedColor.b)/100));
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			GameObject.FindObjectOfType<SnakeHealth>().ApplyDamage(10);
		}
	}
}
