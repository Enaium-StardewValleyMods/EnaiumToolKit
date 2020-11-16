﻿using System.Collections.Generic;
using EnaiumToolKit.Framework.Screen.Elements;
using EnaiumToolKit.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace EnaiumToolKit.Framework.Screen
{
    public class ScreenGui : IClickableMenu
    {
        private List<Element> _elements;
        private int _index;
        private int _maxElement;

        public ScreenGui()
        {
            _elements = new List<Element>();
            _index = 0;
            _maxElement = 7;
            width = 832;
            height = 576;
            var centeringOnScreen = Utility.getTopLeftPositionForCenteringOnScreen(width, height);
            xPositionOnScreen = (int) centeringOnScreen.X;
            yPositionOnScreen = (int) centeringOnScreen.Y + 32;
        }

        public override void draw(SpriteBatch b)
        {
            drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), xPositionOnScreen, yPositionOnScreen,
                width, height, Color.White, 4f);
            var y = yPositionOnScreen + 20;

            for (int i = _index, j = 0; j < (_elements.Count >= _maxElement ? _maxElement : _elements.Count); i++, j++)
            {
                var variable = _elements[i];

                if (variable.Visibled && variable.Enabled)
                {
                    variable.Render(b, xPositionOnScreen + 15, y + j * 78);
                    if (variable.Hovered)
                    {
                        var descriptionWidth = FontUtils.GetWidth(variable.Description) + 50;
                        var descriptionHeight = FontUtils.GetHeight(variable.Description) + 50;

                        drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 396, 15, 15), 0, 0, descriptionWidth,
                            descriptionHeight, Color.Wheat, 4f, false);
                        FontUtils.DrawHvCentered(b, variable.Description, descriptionWidth / 2, descriptionHeight / 2);
                    }
                }
            }

            drawMouse(b);
            base.draw(b);
        }

        public override void receiveLeftClick(int x, int y, bool playSound)
        {
            foreach (var variable in _elements)
            {
                if (variable.Visibled && variable.Enabled && variable.Hovered)
                {
                    variable.MouseLeftClicked(x, y);
                    Game1.playSound("drumkit6");
                }
            }

            base.receiveLeftClick(x, y, playSound);
        }

        public override void releaseLeftClick(int x, int y)
        {
            foreach (var variable in _elements)
            {
                if (variable.Visibled && variable.Enabled && variable.Hovered)
                {
                    variable.MouseLeftReleased(x, y);
                }
            }

            base.releaseLeftClick(x, y);
        }

        public override void receiveRightClick(int x, int y, bool playSound)
        {
            foreach (var variable in _elements)
            {
                if (variable.Visibled && variable.Enabled && variable.Hovered)
                {
                    variable.MouseRightClicked(x, y);
                }
            }

            base.receiveRightClick(x, y);
        }

        public override void receiveScrollWheelAction(int direction)
        {
            if (direction > 0 && _index > 0)
            {
                _index--;
            }
            else if (direction < 0 && _index + (_elements.Count >= _maxElement ? _maxElement : _elements.Count) <
                _elements.Count)
            {
                _index++;
            }

            base.receiveScrollWheelAction(direction);
        }

        public void AddElement(Element element)
        {
            _elements.Add(element);
        }

        public void AddElementRange(params Element[] element)
        {
            _elements.AddRange(element);
        }
    }
}