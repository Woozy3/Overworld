using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
namespace Overworld.Projectiles.Minions
{
	public class FlyMinion : DashFly
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.NebulaChainsaw;
		//Changes the texture. Don't need a sprite for this.
		//To use normal autoload properties, just remove that line.
		public override void SetDefaults()
		{
			#region Normal Projectile Fields
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.damage = 30;
			projectile.width = 24;
			projectile.height = 48;
			projectile.minion = true;
			projectile.penetrate = -1;
			#endregion
			//Everything below here you *must* have included as part of the projectile code.
			maxSpeed = 9f; //Max speed the minion can travel normally
			canSeeThroughTiles = true; //Can the minion see through tiles?
			accellerationMult = 0.02f; //How fast does it passively accellerate? (this one doesn't really matter, you could make it 0)
			dashSpeed = 15f; //how fast does the projectile travel when dashing?
			dashLength = 20f; //Dash length, measured in ticks (20 is a non-insignificant amount)
			followDistance = 300f; //how far the minion can go away from the player (with some modifications)
			detectionRadius = 600f; //how far the minion can see
			hoverRadius = 160f; //how far does the minion hover from enemies while waiting to dash?
			dashTime = 10f; //cooldown between dashes.
		}
		public override void CheckActive() {} //Write check active code from example minion here, if you don't want them to be infinite.
		public override void SelectFrame() {} //This doesn't actually use the SelectFrame method, so you can choose to leave this blank.
	}
}
