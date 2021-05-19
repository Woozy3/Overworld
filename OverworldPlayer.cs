using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Overworld
{
	public class OverworldPlayer : ModPlayer //ModPlayer classes are fun as all hell.
	{
		public bool spikeShoot; //reset = false
		
		public override void ResetEffects() //Used for when buffs wear off or the player unequips an accessory.
		{
			spikeShoot = false; 
		}
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if(spikeShoot && Main.rand.NextFloat() <= 0.4f) //If we have the accessory equipped AND a 40% chance
			{
				//This could be done in Hurt hook, I prefer to do it in PreHurt however.
				Projectile p = Main.projectile[Projectile.NewProjectile(player.Center, new Vector2(Main.rand.NextFloat(-1, 1)*5.5f, Main.rand.NextFloat(-1,1)*5.5f), ProjectileID.JungleSpike, 30, 4f, player.whoAmI); //Shoot a projectile from the player.
				p.friendly = true; //Makes the projectile friendly
				p.hostile = false; //And not hostile
			}
			return true;
		}
	}
}