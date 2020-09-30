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
			$"Got removal request of {h.transform.position}".Log();
			if (!healthBars.ContainsKey(h.transform.position))
				return;

			HealthBar bar = healthBars[h.transform.position];
			if(bar == null)
				return;

			Destroy(healthBars[h.transform.position].gameObject);
			healthBars.Remove(h.transform.position);

		}

		public bool ExistHealthBar(Vector3 pos)
		{
			return healthBars.ContainsKey(pos);
		}
		
		public HealthBar GetHealthBar(Vector3 pos)
		{
			return healthBars[pos];
		}
	}
}