using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
namespace Overworld.Projectiles.Minions
{
	public abstract class Minion : ModProjectile
    {
		private bool justCreated = true;
		public sealed override void AI()
		{
			CheckActive();
			Behavior();
			CreateDust();
			if (justCreated)
			{
				OnCreated();
				justCreated = false;
			}
		}

		/// <summary>
		/// This is what will be used to see if the player still has the buff for the minion. See ExampleMod's minions for example. :L
		/// </summary>
		public abstract void CheckActive();

		/// <summary>
		/// The AI of the minion. This will be overriden in the child class.
		/// </summary>
		public abstract void Behavior();

		/// <summary>
		/// Code to run When the minion is created. Useful for assigning defaults outside of SetDefaults(), as well as Main.NewText() debugging.
		/// </summary>
		public virtual void OnCreated() { }

		/// <summary>
		/// Use this hook to create dust particles.
		/// </summary>
		public virtual void CreateDust() { }

		/// <summary>
		/// Use this hook to select the projectile frame, if necessary.
		/// </summary>
		public virtual void SelectFrame() { }
	}
}