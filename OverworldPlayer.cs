using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Overworld
{
	public class OverworldPlayer : ModPlayer //ModPlayer classes are fun as all hell.
	{
		public int[] timers = {0}; //Using this as a custom field. Will be reset on entering the world
		public bool spikeShoot; //reset = false
		
		#region Minion Buffs
		//Just put these in a region so you don't have to see the tons of minions buffs lul.
		public bool akkhotepMinion; //reset = false
		#endregion
		
		public override void OnEnterWorld(Player player)
		{
			for(int i = 0; i < timers.length; i++)
			{
				timers[i] = 0;
			}
		}
		
		public override void ResetEffects() //Used for when buffs wear off or the player unequips an accessory.
		{
			spikeShoot = false; 
			akkhotepMinion = false; //Necessary to make sure this is reset properly.
		}
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if(spikeShoot && Main.rand.NextFloat() <= 0.4f) //If we have the accessory equipped AND a 40% chance
			{
				//This could be done in Hurt hook, I prefer to do it in PreHurt however.
				Projectile p = Main.projectile[Projectile.NewProjectile(player.Center, new Vector2(Main.rand.NextFloat(-1, 1)*5.5f, Main.rand.NextFloat(-1,1)*5.5f), ProjectileID.JungleSpike, 20, 4f, player.whoAmI); //Shoot a projectile from the player.
				p.friendly = true; //Makes the projectile friendly
				p.hostile = false; //And not hostile
			}
			return true;
		}
		
		public override void PostUpdateEquips()
		{
			bool doesAura = true; //Just disable this if you don't like the damaging aura. I've made sure to balance it at least somewhat, don't worry.
			float radius = 6*16f; //6 tiles, each tile is 16 pixels
			if(doesAura)
			{
				timers[0]++;
				if(spikeShoot && timers[0] >= 1*60) //5*60 is every 5 seconds
				{
					for(int i = 0; i < Main.maxNPCs; i++)
					{
						if(Vector2.Distance(player.Center, Main.npc[i].Center) <= radius)
							Main.npc[i].StrikeNPC(5, 0f, player.X > Main.npc[i].X ? -1 : 1);
					}
				}
			}
		}
		
		public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) //I like to use draw effects for dusts around the player.
		{
			bool doesAura = true; //Same as above.
			if (spikeShoot && doesAura)
			{
				for (int i = 0; i < 14; i++)
				{
				    int d = Dust.NewDust(player.Center + Vector2.One.RotatedByRandom(MathHelper.ToRadians(360f))*(6*16f), 1, 1, 138, 0f, 0f, default, default, 0.8f);
				    Main.dust[d].velocity = Vector2.Normalize(new Vector2(Main.dust[d].position.X - player.Center.X, Main.dust[d].position.Y - player.Center.Y)) * 1.4f;
				    Main.dust[d].noGravity = true;
				}
			}
		}
	}
}
