using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace Overworld.Projectiles.Minions
{
	public class FlyMinion : DashFly
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.NebulaChainsaw;
		public override void SetDefaults()
		{
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.width = 24; //replace this with exported sprite size
			projectile.height = 48; //replace this with exported sprite size
			projectile.minion = true;
			maxSpeed = 8.7f; //Max speed the minion can travel horizontally
			canSeeThroughTiles = true; //Can the minion see through tiles? Set to true for now, most convenient.
			accellerationMult = 0.02f;
			dashSpeed = 16f;
			dashLength = 45f;
			followDistance = 300f;
			detectionRadius = 650f;
			projectile.penetrate = -1;
			hoverRadius = 180f;
			dashTime = 30f; //Cooldown between dashes
		}
        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            OverworldPlayer modPlayer = player.GetModPlayer<OverworldPlayer>();
            if (player.dead)
            {
                modPlayer.akkhotepMinion = false;
            }
            if (modPlayer.akkhotepMinion)
            { 
                projectile.timeLeft = 2; //Ensures the projectile doesn't die so long as they have the buff.
            }
        }
		public override void SelectFrame() //Unless the minion has more than one frame, we don't actually use this.
		{
		}
        public override bool ModifyRotation(int currentAI)
        {
			if(currentAI != 2)
			{
				//Projectile sprite should be facing upwards for this to work as intended.
				projectile.rotation = projectile.velocity.X > 0 ? MathHelper.PiOver2 : -MathHelper.PiOver2;
				projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
				return false;
			}
			//This only runs is currentAI == 2, so don't worry about else
			return true;
        }
    }
}