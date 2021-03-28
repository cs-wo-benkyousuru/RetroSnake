using System;
using System.Collections.Generic;
using System.Drawing;

namespace RetroSnake
{
    public static class RetroSnakeExtend
    {
        public static void DrawFramedRectangle(this Graphics graphics, Pen pen, Brush brush, Rectangle rectangle)
        {
            graphics.DrawRectangle(pen, rectangle);
            graphics.FillRectangle(brush, new Rectangle(new Point(rectangle.X + 1, rectangle.Y + 1), new Size(rectangle.Width - 1, rectangle.Width - 1)));
        }
    }
    public class Fruit
    {
        public Fruit(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
        public void CreateFruit(Snake snake)
        {
            Random r = new Random();
            do
            {
                CurrentPosition = new Rectangle(new Point(20 * r.Next(0, Width / 20), 20 * r.Next(0, Height / 20)), new Size(20, 20));
            } while (snake.Contains(CurrentPosition));
        }
        public void DrawFruit(Graphics graphics)
        {
            graphics.DrawFramedRectangle(new Pen(Color.Black), new SolidBrush(Color.Green), CurrentPosition);
        }
        public Rectangle CurrentPosition { get; private set; }
        private int Width;
        private int Height;
    }
    public class Snake
    {
        public Snake(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            Body.AddFirst(new Rectangle(new Point(Width / 2, Height / 2), new Size(20, 20)));
            Body.AddFirst(new Rectangle(new Point(Width / 2 + 20, Height / 2), new Size(20, 20)));
        }
        public void DrawSnake(Graphics graphics)
        {
            foreach (var i in Body)
            {
                graphics.DrawFramedRectangle(new Pen(Color.Black), new SolidBrush(Color.Red), i);
            }
        }
        public bool Eaten(Fruit fruit)
        {
            return fruit.CurrentPosition == Body.First.Value;
        }
        public void Move()
        {
            var HeadPoint = Body.First.Value.Location;
            Point p = new Point();
            switch(CurrentDirection)
            {
                case Direction.Up:
                    p = new Point(HeadPoint.X, HeadPoint.Y - 20);
                    break;
                case Direction.Down:
                    p = new Point(HeadPoint.X, HeadPoint.Y + 20);
                    break;
                case Direction.Left:
                    p = new Point(HeadPoint.X - 20, HeadPoint.Y);
                    break;
                case Direction.Right:
                    p = new Point(HeadPoint.X + 20, HeadPoint.Y);
                    break;
            }
            Body.AddFirst(new Rectangle(p, new Size(20, 20)));
            Body.RemoveLast();
        }
        public void Lengthen()
        {
            Body.AddLast(Body.Last.Value);
        }
        public bool Dead()
        {
            if (Body.First.Value.X < 0 || Body.First.Value.X == Width || Body.First.Value.Y < 0 || Body.First.Value.Y == Height)
                return true;
            var tmp = Body.First.Value;
            Body.RemoveFirst();
            if (Body.Contains(tmp)) return true;
            Body.AddFirst(tmp);
            return false;
        }
        private LinkedList<Rectangle> Body = new LinkedList<Rectangle>();
        public enum Direction { Up, Down, Left, Right };
        public Direction CurrentDirection { get; set; } = Direction.Left;
        public bool Contains(Rectangle rectangle)
        {
            return Body.Contains(rectangle);
        }
        private int Width;
        private int Height;
    }
}