using System;
using System.Collections.Generic;
using Swindler.Utilities.Extensions;
using UnityEngine;

namespace Swindler.Game
{
	public class HealthBarController : MonoBehaviour
	{

		public static HealthBarController Instance { get; private set; }
		
		[SerializeField] private HealthBar healthBarPrefab;

		private Dictionary<Vector3, HealthBar> healthBars = new Dictionary<Vector3, HealthBar>();
		
		private void Awake()
		{
			Instance = this;
			Health.OnHealthAdded += AddHealthBar;
			Health.OnHealthRemoved += RemoveHealthBar;
		}

		private void AddHealthBar(Health h)
		{
			if (healthBars.ContainsKey(h.transform.position))
				return;
			
			HealthBar healthBar = Instantiate(healthBarPrefab, transform);
			healthBars.Add(h.transform.position, healthBar);
			healthBar.SetHealth(h);

		}
		
		private void RemoveHealthBar(Health h)
		{
			Vector3 pos = h.transform.position;
			if (!healthBars.ContainsKey(pos))
				return;

			HealthBar bar = healthBars[pos];
			if(bar == null)
				return;

			Destroy(healthBars[pos].gameObject);
			healthBars.Remove(pos);

		}

		public bool ExistHealthBar(Vector3 pos)
		{
			return healthBars.ContainsKey(pos);
		}
		
		public HealthBar GetHealthBar(Vector3 pos)
		{
			return !healthBars.ContainsKey(pos) ? null : healthBars[pos];
		}
	}
}