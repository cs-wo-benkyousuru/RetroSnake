using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RetroSnake;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            graphics = pictureBox1.CreateGraphics();
            snake = new Snake(pictureBox1.Width, pictureBox1.Height);
            fruit = new Fruit(pictureBox1.Width, pictureBox1.Height);
            
            fruit.CreateFruit(snake);
            fruit.DrawFruit(graphics);
        }
        private Snake snake;
        private Fruit fruit;
        private Graphics graphics;
        //Graphics graphics = panel1.CreateGraphics();
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(snake.Dead())
            {
                timer1.Enabled = false;
                MessageBox.Show("Game Over!", caption: "Oops");
                Environment.Exit(0);
            }
            graphics.Clear(pictureBox1.BackColor);
            fruit.DrawFruit(graphics);
            snake.DrawSnake(graphics);
            snake.Move();
            if (snake.Eaten(fruit))
            {
                fruit.CreateFruit(snake);
                snake.Lengthen();
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var key = e.KeyChar;
            switch (key)
            {
                case 'W':
                case 'w':
                    snake.CurrentDirection = Snake.Direction.Up;
                    break;
                case 'S':
                case 's':
                    snake.CurrentDirection = Snake.Direction.Down;
                    break;
                case 'A':
                case 'a':
                    snake.CurrentDirection = Snake.Direction.Left;
                    break;
                case 'D':
                case 'd':
                    snake.CurrentDirection = Snake.Direction.Right;
                    break;
                default:
                    break;
            }
        }
    }
}
