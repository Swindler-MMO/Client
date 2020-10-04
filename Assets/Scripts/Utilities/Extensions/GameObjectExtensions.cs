using Swindler.Game;
using UnityEngine;

namespace Swindler.Utilities.Extensions
{
	public static class GameObjectExtensions
	{

		public static Health AddHealh(this GameObject o, float maxHealth, Vector3 pos, bool modify = false)
		{
			Health h = o.AddComponent<Health>();
			h.SetMaxHealth(maxHealth);
			
			if(modify)
				h.ModifyHealth(-1);
			
			o.transform.position = pos;

			return h;
		}
		
	}
}