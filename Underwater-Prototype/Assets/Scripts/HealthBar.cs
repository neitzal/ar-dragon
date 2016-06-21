using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	private float initWidth;
	private RectTransform rectTransform;

	private 

	void Start() {
		
		rectTransform = GetComponent<RectTransform>();
		initWidth = rectTransform.rect.width;
	}

	public void UpdateHealth(float health) {
//		rectTransform.rect.Set(rectTransform.rect.x, rectTransform.rect.y, , rectTransform.rect.height);
		rectTransform.sizeDelta = new Vector2(initWidth * health / 100.0f, rectTransform.sizeDelta.y);
//		Debug.LogFormat("Update Health: {0}", health);
//		Debug.LogFormat("New width: {0} (should be {1})", rectTransform.rect.width, initWidth * health / 100.0f);

//		if (health < 50.0f) {
//			.SetColor(new Color(1, 0.0f, 0.0f));
//		}



	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.A)) {
			GameObject.FindObjectOfType<SnakeHealth>().ApplyDamage(10);
		}
	}
}
