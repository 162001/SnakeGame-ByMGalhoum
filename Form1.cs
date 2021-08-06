using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class SnakeGame : Form
    {
        private List<Circle> Snake = new List<Circle>(); //For Creating A list Arry for Snake
        private Circle food = new Circle(); //For Craeting A Single Circle Called Foood

        public SnakeGame()
        {
            InitializeComponent();
            new Settings();
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();
            startGame();

        }
        private void updateScreen(object sender,EventArgs e)
        {
            if(Settings.GameOver==true)
            {
                if(Input.KeyPress(Keys.Enter))
                {
                    startGame();
                }
            }
            else
            {
                if(Input.KeyPress(Keys.Right)&&Settings.direction !=Directions.Left)
                {
                    Settings.direction = Directions.Right;
                }
                else if(Input.KeyPress(Keys.Left) && Settings.direction != Directions.Right)
                {
                    Settings.direction = Directions.Left;
                }
                else if (Input.KeyPress(Keys.Up) && Settings.direction != Directions.Down)
                {
                    Settings.direction = Directions.Up;
                }
                else if (Input.KeyPress(Keys.Down) && Settings.direction != Directions.Up)
                {
                    Settings.direction = Directions.Down;
                }
                movePlayer();
            }
            pictureBox1.Invalidate();
        }
        private void movePlayer()
        {
            for (int i=Snake.Count-1;i>=0;i--)
            {
                if(i==0)
                {
                    switch(Settings.direction)
                    {
                        case Directions.Right:
                            Snake[i].x ++;
                            break;
                        case Directions.Left:
                            Snake[i].x--;
                            break;
                        case Directions.Up:
                            Snake[i].y--;
                            break;
                        case Directions.Down:
                            Snake[i].y++;
                            break;
                    }
                    int maxXpos = pictureBox1.Size.Width / Settings.Width;
                    int maxYpos = pictureBox1.Size.Height / Settings.Height;

                    if(Snake[i].x < 0||Snake[i].y < 0||Snake[i].x > maxXpos||Snake[i].y > maxYpos)
                    {
                        die();
                    }
                    for(int j=1;j<Snake.Count;j++)
                    {
                        if(Snake[i].x==Snake[j].x && Snake[i].y==Snake[j].y)
                        {
                            die();
                        }
                    }
                    if(Snake[0].x==food.x && Snake[0].y==food.y)
                    {
                        eat();
                    }
                }
                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

        private void updateG(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if(Settings.GameOver==false)
            {
                Brush snakeColour;

                for(int i=0;i<Snake.Count;i++)
                {
                    if(i==0)
                    {
                        snakeColour = Brushes.Black;
                    }
                    else
                    {
                        snakeColour = Brushes.Green;
                    }
                    canvas.FillEllipse(snakeColour,
                        new Rectangle(
                           Snake[i].x*Settings.Width,
                           Snake[i].y*Settings.Height,
                           Settings.Width,Settings.Height
                            ));

                    canvas.FillEllipse(Brushes.Red,
                       new Rectangle(
                          food.x * Settings.Width,
                          food.y * Settings.Height,
                          Settings.Width, Settings.Height
                           ));
                }
            }
            else
            {
                string gameOver = "Game Over \n" + "Your Final Score is" + Settings.Score + "\n Enter press to Restart again!";
                label3.Text = gameOver;
                label3.Visible = true;
            }
        }
         private void startGame()
        {
            label3.Visible = false;
            new Settings();
            Snake.Clear();
            Circle head = new Circle { x = 10, y = 5 };
            Snake.Add(head);
            label2.Text = Settings.Score.ToString();
            generateFood();
        }
        private void generateFood()
        {
            int maxXpos = pictureBox1.Size.Width / Settings.Width;
            int maxYpos = pictureBox1.Size.Height / Settings.Height;
            Random rnd = new Random();
            food = new Circle { x = rnd.Next(0, maxXpos), y = rnd.Next(0, maxYpos) };
        }
        private void eat()
        {
            Circle body = new Circle();
            {
                int X = Snake[Snake.Count - 1].x,
                  Y = Snake[Snake.Count - 1].x;
            };
            Snake.Add(body);
            Settings.Score += Settings.Points;
            label2.Text = Settings.Score.ToString();
            generateFood();
        }
        private void die()
        {
            Settings.GameOver = true;
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {

        }
    }
}
