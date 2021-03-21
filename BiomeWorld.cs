using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using FarTooManyEvilBiomes.Biomes;

namespace FarTooManyEvilBiomes
{
	public class BiomeWorld : ModWorld
	{
		public static int ExperimentTiles = 0;

		public override void ResetNearbyTileEffects()
		{
			ExperimentTiles = 0;
		}

		public override void TileCountsAvailable(int[] tileCounts)
		{
			ExperimentTiles = tileCounts[ModContent.TileType<Biomes.ExperimentBiome.Tiles.ExperimentTile>()];
			base.TileCountsAvailable(tileCounts);
		}
	}
}
