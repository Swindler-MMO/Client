using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Swindler.Game
{
	public class HealthBar : MonoBehaviour
	{

		[SerializeField] private Image foregroundImage;
		[SerializeField] private float updateSpeedSeconds;
		[SerializeField] private float positionOffset;

		private Health _health;

		public void ModifyHealth(float amount)
		{
			_health.ModifyHealth(amount);
		}
		
		public void SetHealth(Health h)
		{
			_health = h;
			_health.OnHealthChanged += HandleHealthChanged;
		}

		private void HandleHealthChanged(float pct)
		{
			StartCoroutine(ChangeToPercent(pct));
		}

		private IEnumerator ChangeToPercent(float percent)
		{

			float preChangePct = foregroundImage.fillAmount;
			float elapsed = 0f;

			while (elapsed < updateSpeedSeconds)
			{
				elapsed += Time.deltaTime;
				foregroundImage.fillAmount = Mathf.Lerp(preChangePct, percent, elapsed / updateSpeedSeconds);
				yield return null;
			}

			foregroundImage.fillAmount = percent;
		}

		public void Remove()
		{
			Destroy(_health.gameObject);
		}
		
		private void LateUpdate()
		{
			transform.position =
				Camera.main.WorldToScreenPoint(_health.transform.position + Vector3.up * positionOffset);
		}

		private void OnDestroy()
		{
			_health.OnHealthChanged -= HandleHealthChanged;
		}
	}
}