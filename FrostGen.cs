using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Overworld.Ore;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Xna;

namespace Overworld.Ore
{
    class FrostGen : ModWorld
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int IceIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Ice"));
            if (IceIndex != -1)
            {
                tasks.Insert(IceIndex + 1, new PassLegacy("Frostjade", GenFrostJade));
            }
        }
        public void GenFrostJade(GenerationProgress progress)
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            progress.Message = "Generating Frostjade Shards";
            for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
            {
                int i = WorldGen.genRand.Next(0, Main.maxTilesX);
                int j = WorldGen.genRand.Next((int)WorldGen.worldSurfaceLow, Main.maxTilesY);
                if (Main.tile[i, j].type == TileID.SnowBlock || Main.tile[i, j].type == TileID.IceBlock || Main.tile[i, j].type == TileID.BreakableIce || Main.tile[i, j].type == TileID.FleshIce || Main.tile[i, j].type == TileID.CorruptIce)
                {
                    WorldGen.TileRunner(i, j, (double)WorldGen.genRand.Next(2, 9), WorldGen.genRand.Next(2, 9), ModContent.TileType<FrostjadeShardTile>());
                    WorldGen.TileRunner(i, j, (double)WorldGen.genRand.Next(2, 9), WorldGen.genRand.Next(2, 9), ModContent.TileType<FrostjadeShardTile>());
                    WorldGen.TileRunner(i, j, (double)WorldGen.genRand.Next(2, 9), WorldGen.genRand.Next(2, 9), ModContent.TileType<FrostjadeShardTile>());
                    WorldGen.TileRunner(i, j, (double)WorldGen.genRand.Next(2, 9), WorldGen.genRand.Next(2, 9), ModContent.TileType<FrostjadeShardTile>());
                    WorldGen.TileRunner(i, j, (double)WorldGen.genRand.Next(2, 9), WorldGen.genRand.Next(2, 9), ModContent.TileType<FrostjadeShardTile>());
                }
            }
        }
    }
}
  