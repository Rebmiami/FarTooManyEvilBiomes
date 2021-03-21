using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using FarTooManyEvilBiomes.Biomes;

using static Terraria.ModLoader.ModContent;

namespace FarTooManyEvilBiomes
{
	public static class BiomeTiles
	{
		public static bool[] experimentTiles = TileID.Sets.Factory.CreateBoolSet(TileType<Biomes.ExperimentBiome.Tiles.ExperimentTile>());

		static BiomeTiles()
		{

		}
	}
}
