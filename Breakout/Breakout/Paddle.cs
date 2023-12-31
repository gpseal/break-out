﻿/*
 * Moves paddle, draws paddle
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Breakout
{
    public class Paddle
    {
        private const int TAILHEIGHT = 4;
        private int PADDLESPEED = 10;

        private Graphics bufferGraphics;
        private Point position;
        private int paddleWidth;
        private int height;
        private int introSpeed;
        private int aniFrame;
        private Size playArea;
        
        private Rectangle rectangle;
        private List<Ball> ballList;
        private List<DropBall> dropBallList;

        //paddle textures etc
        private TextureBrush tbrush;
        private Image paddleImage;
        private Brush tail1;
        private Brush tail2;
        private Brush tail3;
        private Brush tail4;
        private Brush underWater;
        private TextureBrush engineBrush;
        private Image engine;
        private int level;

        public Paddle(Point position, Color colour, Graphics bufferGraphics, int paddleWidth, int height, Size playArea, List<Ball> ballList, List<DropBall> dropBallList, int level)
        {
            this.level = level;
            this.dropBallList = dropBallList;
            this.ballList = ballList;
            //this.ball = ball;
            this.playArea = playArea;
            this.height = height;
            this.paddleWidth = paddleWidth;
            this.bufferGraphics = bufferGraphics;
            this.position = position;
            introSpeed = 15;
            aniFrame = 0;
            rectangle = new Rectangle(position.X, position.Y, paddleWidth, height);

            //paddle animations
            paddleImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("p" + (aniFrame).ToString());
            tbrush = new TextureBrush(paddleImage);
            tail1 = new SolidBrush(Color.FromArgb(190, 204, 245, 255));
            tail2 = new SolidBrush(Color.FromArgb(150, 204, 245, 255));
            tail3 = new SolidBrush(Color.FromArgb(100, 204, 245, 255));
            tail4 = new SolidBrush(Color.FromArgb(50, 204, 245, 255));
            underWater = new SolidBrush(Color.FromArgb(50, 51, 51, 204));
            engine = (Bitmap)Properties.Resources.ResourceManager.GetObject("engine");
            engineBrush = new TextureBrush(engine);
            
        }

        public void Intro()
        {
            position.Y -= introSpeed;
            introSpeed--;
        }

        //moves paddle left, animates flame effect from right side of paddle
        public void MoveLeft()
        {
            if (position.X > 0)
            {
                position.X -= PADDLESPEED;

                bufferGraphics.FillRectangle(tail1, position.X + paddleWidth + PADDLESPEED, position.Y + 7, PADDLESPEED, TAILHEIGHT);
                bufferGraphics.FillRectangle(tail1, position.X + paddleWidth + PADDLESPEED, position.Y + 14, PADDLESPEED, TAILHEIGHT);

                bufferGraphics.FillRectangle(tail2, position.X + paddleWidth + PADDLESPEED, position.Y + 7, PADDLESPEED * 2, TAILHEIGHT);
                bufferGraphics.FillRectangle(tail2, position.X + paddleWidth + PADDLESPEED, position.Y + 14, PADDLESPEED * 2, TAILHEIGHT);

                bufferGraphics.FillRectangle(tail3, position.X + paddleWidth + 3 * PADDLESPEED, position.Y + 7, PADDLESPEED * 2, TAILHEIGHT);
                bufferGraphics.FillRectangle(tail3, position.X + paddleWidth + 3 * PADDLESPEED, position.Y + 14, PADDLESPEED * 2, TAILHEIGHT);

                bufferGraphics.FillRectangle(tail4, position.X + paddleWidth + 5 * PADDLESPEED, position.Y + 7, PADDLESPEED * 2, TAILHEIGHT);
                bufferGraphics.FillRectangle(tail4, position.X + paddleWidth + 5 * PADDLESPEED, position.Y + 14, PADDLESPEED * 2, TAILHEIGHT);

                engineBrush.Transform = new Matrix(100.0f / 100.0f, 0.0f, 0.0f, 20.0f / 20.0f, position.X + 8, position.Y); //adjusts position of texture
                bufferGraphics.FillRectangle(engineBrush, position.X + paddleWidth - 10, position.Y, height, height);
            }
        }

        //moves paddle right, animates flame effect from left side of paddle
        public void MoveRight()
        {
            if (position.X + paddleWidth < playArea.Width)
            {
                position.X += PADDLESPEED;
                bufferGraphics.FillRectangle(tail1, position.X - PADDLESPEED*2, position.Y + 7, PADDLESPEED, TAILHEIGHT);
                bufferGraphics.FillRectangle(tail1, position.X - PADDLESPEED*2, position.Y + 14, PADDLESPEED, TAILHEIGHT);

                bufferGraphics.FillRectangle(tail2, position.X - PADDLESPEED * 3, position.Y + 7, PADDLESPEED * 2, TAILHEIGHT);
                bufferGraphics.FillRectangle(tail2, position.X - PADDLESPEED * 3, position.Y + 14, PADDLESPEED * 2, TAILHEIGHT);

                bufferGraphics.FillRectangle(tail3, position.X -  PADDLESPEED * 5, position.Y + 7, PADDLESPEED * 2, TAILHEIGHT);
                bufferGraphics.FillRectangle(tail3, position.X -  PADDLESPEED * 5, position.Y + 14, PADDLESPEED * 2, TAILHEIGHT);

                bufferGraphics.FillRectangle(tail4, position.X - PADDLESPEED * 7, position.Y + 7, PADDLESPEED * 4, TAILHEIGHT);
                bufferGraphics.FillRectangle(tail4, position.X - PADDLESPEED * 7, position.Y + 14, PADDLESPEED * 4, TAILHEIGHT);

                engineBrush.Transform = new Matrix(100.0f / 100.0f, 0.0f, 0.0f, 20.0f / 20.0f, position.X-10, position.Y); //adjusts position of texture https://docs.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-fill-a-shape-with-an-image-texture?view=netframeworkdesktop-4.8
                bufferGraphics.FillRectangle(engineBrush, position.X - PADDLESPEED, position.Y, height, height);
            }
        }

        //Draw paddle
        public void Draw()
        {
            if (aniFrame == 20)
            {
                aniFrame = 0;
            }
            rectangle.X = position.X;
            rectangle.Y = position.Y;

            //animates paddle
            paddleImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("p" + (aniFrame).ToString()); //changes image to next frame in set
            tbrush = new TextureBrush(paddleImage); //applies new texture brush
            tbrush.Transform = new Matrix(100.0f / 100.0f, 0.0f, 0.0f, 20.0f / 20.0f, position.X, position.Y); //adjusts position of texture  https://docs.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-fill-a-shape-with-an-image-texture?view=netframeworkdesktop-4.8
            bufferGraphics.FillRectangle(tbrush, position.X, position.Y, paddleWidth, height); //draws image using textureBrush
            aniFrame++;

            //changes paddle texture for level 2
            if (level == 2)
            {
                bufferGraphics.FillRectangle(underWater, position.X, position.Y, paddleWidth, height);
            }
        }
        
        public Rectangle Rectangle { get => rectangle; set => rectangle = value; }
    }
}
