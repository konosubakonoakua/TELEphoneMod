using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TELEphone.Items
{
    public class TELEphoneItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("TELEphone"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("A really TELE Phone. Use with caution!");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.IceMirror);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MagicMirror);
            recipe.AddIngredient(ItemID.Teleporter, 99);
            recipe.AddIngredient(ItemID.TrifoldMap);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateInventory(Player player)
        {
            // player.statDefense += 2;
            TELEphone.isMapTeleporterValid = true;
        }

    }
}