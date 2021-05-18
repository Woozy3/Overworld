public class SandSplitter : ModItem //For autoloading texture, name sprite file "SandSplitter." Else, ask me about overriding textures.
    {
        public override void SetDefaults()
        {
            item.ranged = true; //Ranged damage/item
            item.damage = 9;
            item.useTime = 18;
            item.useAnimation = 18;
            item.autoReuse = true; //allows the item to be automatically reused.
            item.useAmmo = Terraria.ID.AmmoID.Arrow; //Ammo type, this will use arrows
            item.shootSpeed = 7f; //7f is pretty average, I usually go for 'round 6f. This is pixels per tick as I recall
            item.shoot = Terraria.ID.ProjectileID.WoodenArrowFriendly; //Everything that shoots a projectile needs shoot to be defined
            item.knockBack = 0.7f; //Low knockback
            item.crit = 3;
            item.useStyle = 5; //bows and guns and books and staves
            item.value = 3000; //30 silver
            item.rare = 2; //Light green
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (type == Terraria.ID.ProjectileID.WoodenArrowFriendly)
                type = ModContent.ProjectileType<SandArrow>();
            //If the user is using a normal arrow, or the endless quiver, this changes the projectile type.
            return true;
        }
    }
    public class SandArrow : ModProjectile
    {
        //Have a projectile sprite named sand arrow.
        public override void SetStaticDefaults()
        {
            Terraria.ID.ProjectileID.Sets.TrailCacheLength[projectile.type] = 5; //The length of trail. I think this would look cool
            Terraria.ID.ProjectileID.Sets.TrailingMode[projectile.type] = 0; //trail type (0 is position, which is fine)
        }
        public override void SetDefaults()
        {
            projectile.ranged = true; //ensures this is a ranged projectile
            projectile.timeLeft = 600; //1 min iirc
            projectile.penetrate = 1; //The projectile will always hit.
            //Change width and height to exported sprite size. Sprite should face upwards.
            projectile.width = 10;
            projectile.height = 16;
        }
        public override void AI()
        {
            projectile.velocity.Y += 0.01f; //adjust this as you see fit to increase or decrease effect of gravity.
            projectile.rotation = projectile.velocity.ToRotation();
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.NextFloat() <= 0.1f) //A lot of this arrow will be shot, so give it only a 10% chance to spawn a sand tornado
            {
                Projectile p = Main.projectile[Projectile.NewProjectile(projectile.Center, Vector2.Zero, Terraria.ID.ProjectileID.SandnadoHostile, projectile.damage / 2, 0f, projectile.owner)];
                p.timeLeft = 60; //this stays for only about a second.
            }
        }
    }