using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace Overworld.Items.Weapons.Melee
{
	public class BoneSlider : ModItem
	{
		 public override void SetDefaults()
        {
			item.damage = 11;
			item.melee = true;
			item.width = 20;
			item.height = 20;
			item.useTime = 7;
			item.useAnimation = 25;
			item.channel = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 1f;
			item.value = 4000; //40 silver
			item.rare = 2; //light green
			item.autoReuse = true;
			item.shoot = ModContent.ProjectileType<SlicerBone>();
			item.shootSpeed = 20f;
		}
	}
	public class SlicerBone : ModProjectile
    {
        public override void SetStaticDefaults()
        {
			Main.projFrames[projectile.type] = 5;
		}
        //Copy/pasted from ExampleMod because I'm lazy. Will provide projectile sprite.
        public override void SetDefaults()
		{
			projectile.scale = 2f;
			projectile.width = 6;
			projectile.height = 12;
			projectile.aiStyle = 20; //Want to get rid of drill sounds? Go find Arkhalis aiStyle, 'cuz I'm too lazy to.
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = true;
			projectile.ownerHitCheck = true; //so you can't hit enemies through walls
			projectile.melee = true;
		}

		public override Color? GetAlpha(Color lightColor) => new Color(255, 255, 150, 150);

        public override void AI()
		{
			projectile.rotation -= MathHelper.PiOver2*Main.player[projectile.owner].direction; //Trig rotation stuff
			int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 138, default, 1.9f); //came with ExampleMod copypaste
			Main.dust[dust].noGravity = true;
			projectile.ai[0]++;
			if(projectile.ai[0]%3 == 0)
            {
				projectile.frame++;
				if (projectile.frame > 4)
					projectile.frame = 0;
            }
		}
	}
}