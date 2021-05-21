using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using WhisperingDeath.Projectiles.Bosses;
using WhisperingDeath.Items.Boss;

namespace WhisperingDeath.NPCs.Bosses
{
    [AutoloadBossHead]
    public class Aakhotep : ModNPC
    {
        private float timer;
        float ProjectileTimer;
        float MaxTimer;
        float expertAdd;
        private bool spawned = false;
        private bool spawned2 = false;
        private bool spawned3 = false;
        private bool spawned4 = false;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aakhotep, The Forgotten One");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 84;
            npc.damage = 12;
            npc.defense = 9;
            npc.lifeMax = 2000;
            npc.boss = true;
            npc.DeathSound = SoundID.NPCDeath34;
            npc.value = 60f;
            npc.knockBackResist = 0f;
            aiType = NPCID.SandElemental;
            npc.aiStyle = 102;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/AridSandstorm");
            MaxTimer = 0; // this is the starting value which is important to assign to zero for the projectile code later on
            bossBag = ModContent.ItemType<AakhotepBag>();
        }


        public override void AI()
        {
            Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 32, npc.velocity.X * -0.5f, npc.velocity.Y * -0.5f);

            if (Main.player[npc.target].ZoneDesert)
            {
                npc.dontTakeDamage = false;
            }
            else
            {
                npc.dontTakeDamage = true;
                npc.damage = 60;
                if (npc.life >= npc.lifeMax * 0.5f || Main.expertMode && npc.life >= npc.lifeMax * (0.6f)) // here to make sure that the dashes which happen while outside of the biome don't interfere with the other dashes in the code
                {
                    timer++;
                    if (timer > 180) // after 120 ticks or 2 seconds
                    {
                        timer = 0; // reset the timer
                        npc.velocity.X = Math.Sign(npc.Center.X - Main.player[npc.target].Center.X) * 26f; // make the npc X speed go to the player
                                                                                                           // or
                        npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * 13f; // point to the player's center (X and Y)
                    }
                }
            }

            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest();
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.active = false;
                    return;
                }
            }

            ProjectileTimer++; // this starts the timer counting up by a value of 1 (ProjectileTimer += 1f; would do the same thing)

            if (ProjectileTimer >= MaxTimer) // this checks that the projectile's timer is over the maximum time you set
            {
                Projectile.NewProjectile(npc.Center, (player.Center - npc.Center).SafeNormalize(-Vector2.UnitY) * 8f, ModContent.ProjectileType<SandyBolt>(), 23, 6, Main.myPlayer); // this fires the projectile
                MaxTimer = 0; // sets this to zero to activate the code that generates a number
                ProjectileTimer = 0; // this resets the timer to zero
            }

            if (Main.expertMode)
                expertAdd = 0.1f;
            else
                expertAdd = 0f;

            if (MaxTimer == 0) // to make sure only 1 number is generated
            {
                if (npc.life >= npc.lifeMax * (0.7 + expertAdd)) // checks if the npc is at or above 80% / 70% life
                {
                    MaxTimer = 180 + Main.rand.Next(180); // goes anywhere from 3 to 6 seconds
                }
                else if (npc.life < npc.lifeMax * (0.7 + expertAdd) && npc.life >= npc.lifeMax * (0.5 + expertAdd)) // checks if npc is below 80% / 70% life but above 60% / 50% life
                {
                    MaxTimer = 120 + Main.rand.Next(120); // goes anywhere from 2 to 4 seconds
                }
                else if (npc.life < npc.lifeMax * (0.5 + expertAdd)) // this method is a bit different because we can't use expert add to add in a last phase. This checks if the npc is below 60% / 50% life
                {
                    if (Main.expertMode && npc.life < npc.lifeMax * 0.2) // checks that you are in expert made and that the npc is below 20 % life
                    {
                        MaxTimer = 60 + Main.rand.Next(60); // goes anywhere from 1 - 2 econds
                    }
                    else // if either not in expert or not below 20% life in wexpert, then it uses the longer time
                    {
                        MaxTimer = 60 + Main.rand.Next(120); // goes anywhere from 1 - 3 seconds
                    }
                }
            }



            if (!spawned && npc.life < npc.lifeMax * 0.8f)
            {
                spawned = true;
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.FlyingAntlion, 12, 6);
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.FlyingAntlion, 14, 6);
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Tumbleweed, 12, 6);
            }

            if (!spawned3 && npc.life < npc.lifeMax * 0.4)
            {
                spawned3 = true;
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.FlyingAntlion, 12, 6);
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.FlyingAntlion, 14, 6);
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.WalkingAntlion, 18, 6);
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.WalkingAntlion, 16, 6);
                NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Tumbleweed, 12, 6);
            }

            if (Main.expertMode && npc.life < npc.lifeMax * (0.6f))
            {
                timer++;
                if (timer > 180)
                {
                    timer = 0; // reset the timer
                    npc.velocity.X = Math.Sign(npc.Center.X - Main.player[npc.target].Center.X) * 16f; // make the npc X speed go to the player
                                                                                                       // or
                    npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * 8f; // point to the player's center (X and Y)

                    // do you mean to have both of these velocity changes going at the same time?
                }
                else
                {
                    aiType = NPCID.SandElemental;
                }

                if (!spawned2 && npc.life < npc.lifeMax * 0.6f)
                {
                    spawned2 = true;
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.FlyingAntlion, 12, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.WalkingAntlion, 12, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Tumbleweed, 12, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Tumbleweed, 12, 6);
                }

                if (!spawned4 && npc.life < npc.lifeMax * 0.2 && Main.expertMode)
                {
                    spawned4 = true;
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.FlyingAntlion, 12, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.FlyingAntlion, 14, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.WalkingAntlion, 18, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.WalkingAntlion, 16, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Tumbleweed, 12, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Tumbleweed, 12, 6);
                    NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.WalkingAntlion, 16, 6);
                }
            }
            else if (npc.life < npc.lifeMax * 0.5f)
            {
                timer++;
                if (timer > 180)
                {
                    timer = 0; // reset the timer
                    npc.velocity.X = Math.Sign(npc.Center.X - Main.player[npc.target].Center.X) * 12f; // make the npc X speed go to the player
                                                                                                       // or
                    npc.velocity = npc.DirectionTo(Main.player[npc.target].Center) * 6f; // point to the player's center (X and Y)

                    // do you mean to have both of these velocity changes going at the same time?
                }
                else
                {
                    aiType = NPCID.SandElemental;
                }
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f);
            npc.damage = (int)(npc.damage * 1.8f);
            npc.defense = (int)(npc.defense * 1.8f);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            name = "Aakhotep, The Forgotten One";
            potionType = ItemID.HealingPotion;
        }


        public override void NPCLoot()
        {
            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.FlyingAntlion, 12, 6);
            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Tumbleweed, 12, 6);
            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.Tumbleweed, 12, 6);
            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.WalkingAntlion, 18, 6);
            NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.WalkingAntlion, 16, 6);

            if (Main.rand.NextBool(10))
            {
                //Item.NewItem(npc.getRect(), ModContent.ItemType<AakhotepTrophy>());
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.NextBool(7))
                {
                    Item.NewItem(npc.getRect(), ModContent.ItemType<AakhotepMask>());
                }
                if (Main.rand.NextBool(4))
                {
                    //Item.NewItem(npc.getRect(), ModContent.ItemType<BoneSlicer>());
                }

                if (Main.rand.NextBool(4))
                {
                  //  Item.NewItem(npc.getRect(), ModContent.ItemType<uritem2>());
                }

                if (Main.rand.NextBool(4))
                {
                 //   Item.NewItem(npc.getRect(), ModContent.ItemType<uritem3>());
                }

                if (Main.rand.NextBool(4))
                {
                //    Item.NewItem(npc.getRect(), ModContent.ItemType<uritem4>());
                }

            }
        }
    }
}