using Terraria;
using Terraria.ModLoader;

namespace WhisperingDeath.Projectiles.Bosses
{
    public class SandyBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sand Shard");
        }

        public override void SetDefaults()
        {

            projectile.width = 12;
            projectile.height = 12;
            projectile.damage = 23;
            projectile.aiStyle = 0;
            projectile.penetrate = 3;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 32, projectile.velocity.X * -0.5f, projectile.velocity.Y * -0.5f);   //spawns dust behind it, this is a spectral light blue dust
        }

    }
}
