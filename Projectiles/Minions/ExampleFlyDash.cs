using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
namespace Overworld.Projectiles.Minions
{
	public class FlyMinion : DashFly
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.NebulaChainsaw; //changes the texture, don't worry about adding a sprite for this.
		public override void SetDefaults()
		{
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.damage = 30;
			projectile.width = 24;
			projectile.height = 48;
			projectile.minion = true;
			maxSpeed = 9f; //Max speed the minion can travel horizontally
			canSeeThroughTiles = true; //Can the minion see through tiles?
			accellerationMult = 0.02f;
			dashSpeed = 15f;
			dashLength = 20f;
			followDistance = 300f;
			detectionRadius = 600f;
			projectile.penetrate = -1;
			hoverRadius = 160f;
			dashTime = 10f;
		}
		public override void CheckActive() { } //Write check active code from example minion here, if you don't want them to be infinite.
		public override void SelectFrame() {} //This doesn't actually use the SelectFrame method, so you can choose to leave this blank.
	}
}