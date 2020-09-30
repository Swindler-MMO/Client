using System;
using UnityEngine;

namespace Swindler.Game
{
	public class Health : MonoBehaviour
	{
		
		public static event Action<Health> OnHealthAdded = delegate {  }; 
		public static event Action<Health> OnHealthRemoved = delegate {  };

		public float CurrentHealth { get; private set; }
		
		public event Action<float> OnHealthChanged = delegate {  };
		
		[SerializeField] private float maxHealth = 100f;

		private void Start()
		{
			CurrentHealth = maxHealth;
			OnHealthAdded(this);
		}

		public void ModifyHealth(float amount)
		{
			CurrentHealth += amount;
			
			OnHealthChanged(CurrentHealth / maxHealth);
		}

		private void OnDestroy()
		{
			OnHealthRemoved(this);
		}

		public void SetMaxHealth(float f)
		{
			maxHealth = f;
			if (CurrentHealth > maxHealth)
				CurrentHealth = maxHealth;
		}
	}
}