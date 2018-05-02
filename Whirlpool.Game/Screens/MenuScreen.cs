﻿using OpenTK;
using OpenTK.Graphics;
using Whirlpool.Core.Render;
using System.Collections.Generic;
using System.Drawing;
using UI = Whirlpool.Core.UI;

namespace Whirlpool.Game.Screens
{
    public class MenuScreen : Screen
    {
        public override void Init()
        {
            Size size = MainGame.windowSize;
            AddComponents(new List<RenderComponent>() {
                new UI.Label()
                {
                    text = "OSLO",
                    font = new UI.Font("Content\\Fonts\\Heebo-Bold.ttf", Color4.White, 128, 24),
                    position = new Vector2(20, 75)
                },
                new UI.Label()
                {
                    text = "welcome to oslo.",
                    font = new UI.Font("Content\\Fonts\\Heebo-Bold.ttf", Color4.White, 48, 0),
                    position = new Vector2(20, 140)
                },
                new UI.Label()
                {
                    text = "Play Online\nPlay With Friends\nSettings\nQuit",
                    font = new UI.Font("Content\\Fonts\\Montserrat-Regular.ttf", Color4.White, 32, 0),
                    position = new Vector2(20, 200),
                    lineSpacing = 24
                }
            });
            base.Init();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}