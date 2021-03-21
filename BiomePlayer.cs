using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace FarTooManyEvilBiomes
{
	public class BiomePlayer : ModPlayer
	{
		public bool zoneExperimental = false;

        public override void UpdateBiomes()
        {
            zoneExperimental = BiomeWorld.ExperimentTiles > 50;
            base.UpdateBiomes();
        }
    }
}
