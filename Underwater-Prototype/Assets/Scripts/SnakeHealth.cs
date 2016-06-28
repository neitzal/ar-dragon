using UnityEngine;
using System.Collections;

public class SnakeHealth : MonoBehaviour {
	public float initialHealth = 100f;
	public float HealthPoints { get; set; }

	private GameManagement gameManagement;
	private HealthBar healthBar;
	private Animator damageOverlayAnimator;


	void Start() {
		var gameManagements = GameObject.FindObjectsOfType<GameManagement>();
		if (gameManagements.Length != 1) {
			throw new UnityException("There has to be exactly one GameManagement Script instance!");
		}

		var healthBars = GameObject.FindObjectsOfType<HealthBar>();
		if (healthBars.Length != 1) {
			throw new UnityException("There has to be exactly one HelathBar Script instance!");
		}

		gameManagement = gameManagements[0];
		healthBar = healthBars[0];
		HealthPoints = initialHealth;

		damageOverlayAnimator = GameObject.Find("DamageOverlay").GetComponent<Animator>();
	}


	public void ApplyDamage(float amount) {
		HealthPoints = Mathf.Max(HealthPoints - amount, 0);

		damageOverlayAnimator.SetTrigger("Damaged");

		healthBar.UpdateHealth(HealthPoints);

		if (HealthPoints <= 0) {
			gameManagement.OnSnakeDead();
		}


	}
}
