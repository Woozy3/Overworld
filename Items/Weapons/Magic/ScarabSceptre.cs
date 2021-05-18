using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Overworld.Items.Weapons.Magic
{
	public class ScarabSceptre : ModItem //Add sprite named "ScarabSceptre"
    {
        public override void SetStaticDefaults()
        {
			Item.staff[item.type] = true; //Ensures this is used correctly
		}
        public override void SetDefaults()
        {
			item.magic = true; //magic damage
			item.damage = 14;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 5;
			item.mana = 10;
			item.knockBack = 1.2f;
			item.rare = 2;
			item.value = 2750; //27 silver, 50 copper
			item.shoot = ModContent.ProjectileType<BeetleBolt>();
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			for(int i = 0; i < 5; i++) //Just some dust effects
            {
				Dust d = Main.dust[Dust.NewDust(position, 1, 1, 138, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f))];
				d.noGravity = true;
            }
			return true;
        }
    }
	public class BeetleBolt : ModProjectile //Add "BeetleBolt" sprite, I'm assuming same one posted in discord.
    {
        public override void SetDefaults()
        {
			projectile.magic = true;
			projectile.penetrate = 2;
			projectile.timeLeft = 300;
        }
        public override void AI()
        {
            if(Main.rand.NextFloat() <= 0.6f) //60% chance to spawn dust particles, which are usually a fun thing to do.
            {
				Dust d = Main.dust[Dust.NewDust(projectile.Center, projectile.width, projectile.height, 138)];
				d.noGravity = true;
            }
			projectile.rotation = projectile.velocity.ToRotation()-MathHelper.PiOver4; //You posted a 45 degree sprite, PiOver4 fixing that.
			Lighting.AddLight(projectile.Center, new Vector3(.2f, .2f, .05f));
        }
    }
}