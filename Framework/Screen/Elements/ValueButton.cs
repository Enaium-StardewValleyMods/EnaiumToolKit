﻿using EnaiumToolKit.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;

namespace EnaiumToolKit.Framework.Screen.Elements
{
    public class ValueButton : Element
    {
        public int Current;
        public int Min;
        public int Max;

        public ValueButton(string title, string description) : base(title, description)
        {
        }

        public override void Render(SpriteBatch b, int x, int y)
        {
            Hovered = Render2DUtils.isHovered(Game1.getMouseX(), Game1.getMouseY(), x, y, Width, Height);

            Render2DUtils.drawRect(b, x, y, Width, Height, Hovered ? Color.Wheat : Color.White);
            FontUtils.DrawHvCentered(b, $"{Title}:({Min}-{Max}){Current}", x + Width / 2, y + Height / 2);
        }

        public override void MouseLeftClicked(int x, int y)
        {
            if (Current < Max)
            {
                Current += 1;
            }

            base.MouseLeftClicked(x, y);
        }

        public override void MouseRightClicked(int x, int y)
        {
            if (Current > Min)
            {
                Current -= 1;
            }

            base.MouseRightClicked(x, y);
        }
    }
}