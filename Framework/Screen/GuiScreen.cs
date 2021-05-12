﻿using System.Collections.Generic;
using System.Linq;
using EnaiumToolKit.Framework.Screen.Components;
using EnaiumToolKit.Framework.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;

namespace EnaiumToolKit.Framework.Screen
{
    public class GuiScreen : IClickableMenu
    {
        private List<Component> _components = new List<Component>();

        protected GuiScreen()
        {
            _components.Clear();
            Initialization();
            ModEntry.GetInstance().Helper.Events.Display.WindowResized += (sender, args) => { Initialization(); };
        }

        private void Initialization()
        {
            _components.Clear();
            Init();
        }

        protected virtual void Init()
        {
        }

        public override void draw(SpriteBatch b)
        {
            foreach (var component in _components.Where(component => component.Visibled))
            {
                component.Render(b);
                if (component.Hovered && !component.Description.Equals(""))
                {
                    var descriptionWidth = FontUtils.GetWidth(component.Description) + 50;
                    var descriptionHeight = FontUtils.GetHeight(component.Description) + 50;

                    drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 396, 15, 15), 0, 0, descriptionWidth,
                        descriptionHeight, Color.Wheat, 4f, false);
                    FontUtils.DrawHvCentered(b, component.Description, descriptionWidth / 2, descriptionHeight / 2);
                }
            }

            const string text = "EnaiumToolKit By Enaium";
            FontUtils.Draw(b, text, 0, Game1.viewport.Height - FontUtils.GetHeight(text));

            drawMouse(b);
            base.draw(b);
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            foreach (var component in _components.Where(component =>
                component.Visibled && component.Enabled && component.Hovered))
            {
                component.MouseLeftClicked(x, y);
                Game1.playSound("drumkit6");
            }

            base.receiveLeftClick(x, y, playSound);
        }

        public override void releaseLeftClick(int x, int y)
        {
            foreach (var component in _components.Where(component =>
                component.Visibled && component.Enabled && component.Hovered))
            {
                component.MouseLeftReleased(x, y);
            }

            base.releaseLeftClick(x, y);
        }

        public override void receiveRightClick(int x, int y, bool playSound = true)
        {
            foreach (var component in _components.Where(component =>
                component.Visibled && component.Enabled && component.Hovered))
            {
                component.MouseRightClicked(x, y);
            }

            base.receiveRightClick(x, y, playSound);
        }

        public override void receiveScrollWheelAction(int direction)
        {
            foreach (var component in _components.Where(component =>
                component.Visibled && component.Enabled && component.Hovered))
            {
                component.MouseScrollWheelAction(direction);
            }

            base.receiveScrollWheelAction(direction);
        }

        protected void AddComponent(Component component)
        {
            _components.Add(component);
        }

        protected void AddComponentRange(params Component[] component)
        {
            _components.AddRange(component);
        }

        protected void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }

        protected void RemoveComponentRange(params Component[] component)
        {
            foreach (var variable in component)
            {
                _components.Remove(variable);
            }
        }

        protected void OpenScreenGui(IClickableMenu clickableMenu)
        {
            if (Game1.activeClickableMenu is TitleMenu)
            {
                TitleMenu.subMenu = clickableMenu;
            }
            else
            {
                Game1.activeClickableMenu = clickableMenu;
            }
        }
    }
}