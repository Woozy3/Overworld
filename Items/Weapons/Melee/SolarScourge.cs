using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
namespace Overworld.Items.Weapons.Melee
{
public class SolarScourge : ModItem
    {
        public override void SetDefaults()
        {
			item.useTime = 34;
			item.useAnimation = 34;
			item.useStyle = 1;
			item.rare = 3;
			item.width = 42;
			item.height = 42;
			item.melee = true;
			item.damage = 37;
			item.shootSpeed = 16f;
			item.autoReuse = true;
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			if (player.whoAmI == Main.myPlayer)
			{
				for (int i = 0; i < Main.rand.Next(7, 9); i++)
				{
					Projectile p = Main.projectile[Projectile.NewProjectile(player.itemLocation, Vector2.Normalize(Main.MouseWorld - player.itemLocation).RotatedByRandom(MathHelper.PiOver4 * 0.2f) * Main.rand.NextFloat(0.9f, 1.1f)*item.shootSpeed*0.5f, ModContent.ProjectileType<Firebolt>(), damage, knockBack, player.whoAmI)];
					p.velocity.X *= 2.2f;
					p.position += p.velocity * 1.5f;
				}
			}
        }
    }
	public class Firebolt : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.DD2FlameBurstTowerT1Shot;
        public override void SetStaticDefaults()
        {
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}
        public override void SetDefaults()
		{
			projectile.height = 16;
			projectile.width = 16;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.penetrate = 1;
		}
        public override void AI()
        {
			projectile.velocity.Y *= 1.056f;
			projectile.velocity.Y += 0.08f*projectile.ai[0];
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			if (projectile.wet)
				projectile.Kill();
			if (projectile.ai[0] < 10f)
				projectile.ai[0] += 0.5f;
        }
        public override void Kill(int timeLeft)
		{
			// If the projectile dies without hitting an enemy, crate a small explosion that hits all enemies in the area.
			if (projectile.penetrate == 1)
			{
				// Makes the projectile hit all enemies as it circunvents the penetrate limit.
				projectile.maxPenetrate = -1;
				projectile.penetrate = -1;

				int explosionArea = 60;
				Vector2 oldSize = projectile.Size;
				// Resize the projectile hitbox to be bigger.
				projectile.position = projectile.Center;
				projectile.Size += new Vector2(explosionArea);
				projectile.Center = projectile.position;

				projectile.tileCollide = false;
				projectile.velocity *= 0.01f;
				// Damage enemies inside the hitbox area
				projectile.Damage();
				projectile.scale = 0.01f;

				//Resize the hitbox to its original size
				projectile.position = projectile.Center;
				projectile.Size = new Vector2(10);
				projectile.Center = projectile.position;
			}
			for(int i = 0; i < 16; i++)
            {
				Dust.NewDust(projectile.Center, 32, 32, Main.rand.Next(new int[] { 6, 31 }), Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
            }
		}
		public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 200);
		public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}