using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ReLogic.Graphics;

namespace TELEphoneMod
{
    public class TELEphone : Mod
    {
        public static bool isMapTeleporterValid = false;

        public override void PostDrawFullscreenMap(ref string mouseText)
{
            Main.spriteBatch.DrawString(Main.fontMouseText, "RightClickToTeleport", new Vector2(15, Main.screenHeight - 80), Color.White);
            Terraria.GameInput.PlayerInput.SetZoom_Unscaled();
            if (Main.mouseRight)// && Main.keyState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.LeftControl))
            {
                Player player = Main.player[Main.myPlayer];

                if (!isMapTeleporterValid || player.HasBuff(31))// || player.statLife < 400)
                {
                    // Logger.Info("Map Teleporter not valid!");
                    return;
                }

                player.AddBuff(31, 300);
                isMapTeleporterValid = false;

                int mapWidth = Main.maxTilesX * 16;
                int mapHeight = Main.maxTilesY * 16;
                Vector2 cursorPosition = new Vector2(Main.mouseX, Main.mouseY);

                cursorPosition.X -= Main.screenWidth / 2;
                cursorPosition.Y -= Main.screenHeight / 2;

                Vector2 mapPosition = Main.mapFullscreenPos;
                Vector2 cursorWorldPosition = mapPosition;

                cursorPosition /= 16;
                cursorPosition *= 16 / Main.mapFullscreenScale;
                cursorWorldPosition += cursorPosition;
                cursorWorldPosition *= 16;


                cursorWorldPosition.Y -= player.height;
                if (cursorWorldPosition.X < 0) cursorWorldPosition.X = 0;
                else if (cursorWorldPosition.X + player.width > mapWidth) cursorWorldPosition.X = mapWidth - player.width;
                if (cursorWorldPosition.Y < 0) cursorWorldPosition.Y = 0;
                else if (cursorWorldPosition.Y + player.height > mapHeight) cursorWorldPosition.Y = mapHeight - player.height;
        
                if (Main.netMode == 0)
                {
                    player.Teleport(cursorWorldPosition, 1, 0);
                    player.position = cursorWorldPosition;
                    player.velocity = Vector2.Zero;
                    player.fallStart = (int)(player.position.Y / 16f); 
                }
                else // client mode
                {
                    NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, cursorWorldPosition.X, cursorWorldPosition.Y, 1, 0, 0);
                }


            }

            base.PostDrawFullscreenMap(ref mouseText);

        }

    }

}