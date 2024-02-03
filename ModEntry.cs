using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

namespace FishingAcademy
{
    internal sealed class ModEntry : Mod
    {
        const float MIN_FISHING_PROGRESS = 0.1f;

        // Mod entry point
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            Farmer player = Game1.player;
            if (!Context.IsWorldReady || player == null)
                return;

            if (Game1.activeClickableMenu is BobberBar bobberBar && // Player is fishing
                player.CurrentTool is FishingRod fishingRod &&      // Player is holding a fishing rod
                fishingRod.UpgradeLevel == 0)                       // That rod is the Bamboo Pole
            {
                // Prevent the progress bar from going under a given threshold MIN_FISHING_PROGRESS
                float distanceFromCatching = Helper.Reflection.GetField<float>(bobberBar, "distanceFromCatching").GetValue();
                if (distanceFromCatching < MIN_FISHING_PROGRESS)
                {
                    Helper.Reflection.GetField<float>(bobberBar, "distanceFromCatching").SetValue(MIN_FISHING_PROGRESS);
                }
            }
        }
    }
}
