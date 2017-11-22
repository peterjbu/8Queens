using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1 : Form
    {
        int queens = 0;
        Square[,] board;

        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(625, 700);
            board = new Square[8, 8];
            //initializing board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0) //alternate every other square
                        board[i, j] = new Square(new Point(100 + 50 * i, 100 + 50 * j), Brushes.White);
                    else
                        board[i, j] = new Square(new Point(100 + 50 * i, 100 + 50 * j), Brushes.Black);
                }
            }
        }
        
      

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Point click = scale(e.X, e.Y);
            if (click.X >= 0 && click.X < 8 && click.Y < 8 && click.Y >= 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (board[click.X, click.Y].queenAttacks == false)
                    {
                        //if the queen is allowed in that space, place queen and update
                        board[click.X, click.Y].queen = true;
                        queens++;
                        if (queens == 8)
                        {
                            MessageBox.Show("You did it!");
                        }
                        Invalidate();
                    }
                    else
                    {
                        //queen not allowed here
                        System.Media.SystemSounds.Beep.Play();
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (board[click.X, click.Y].queen)
                    {
                        //Remove the queen and update 
                        QueensPath(click, false);
                        queens--;
                        Invalidate();
                    }
                }
            }
        }

        //drawing board
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("You have " + queens + " queens on the board.", Font, Brushes.Black, new Point(200, 25));
            foreach (Square square in board)
            {
                if (square.queen) //a queen is on that square
                {
                    QueensPath(scale(square.square_point.X, square.square_point.Y), true);
                }
                square.PaintSquare(e.Graphics);

            }
        }

        private Point scale(int X, int Y)
        {
            return new Point((X - 100) / 50, (Y - 100) / 50);
        }

        private void QueensPath(Point click, bool present)
        {
            //Place queen on that square
            board[click.X, click.Y].queen = present;

            
            int up_diag = Math.Abs(click.X - click.Y); //this is the index where the positive diagonal begins on the y axis.
            int down_diag = Math.Abs(click.X + click.Y); //this is the index where the negative diagonal begins on the y axis.
            int tooLarge_diag = Math.Abs(click.X + click.Y - 7); //this is the where the negtive diagnonal begins where y = 8. 

            for (int i = 0; i < 8; i++)
            {
                board[i, click.Y].queenAttacks = present; //vertical
                board[click.X, i].queenAttacks = present; //horizontal
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = up_diag; j < 8; j++)
                {
                    if (i + up_diag == j && click.X <= click.Y)
                    {
                        board[i, j].queenAttacks = present; //diagonal
                    }
                    else if (i + up_diag == j && click.X > click.Y)
                    {
                        board[j, i].queenAttacks = present; //mirrored diagonal
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (click.X + click.Y < 8)
                {
                    for (int j = down_diag; j >= 0; j--)
                    {
                        if (down_diag - i == j)
                            board[i, j].queenAttacks = present; //diagonal

                    }
                }
                else if (click.X + click.Y > 7)
                {
                    for (int k = tooLarge_diag; k < 8; k++)
                    {
                        if (tooLarge_diag + i == k)
                            board[k, 7 - i].queenAttacks = present; //diagonal when starting index is too high
                    }
                }

            }
        }
            
            
        

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Square square in board)
            {
                square.reset();
                queens = 0;
            }
            Invalidate();
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (Square square in board)
                {
                    square.hint = true;
                }
            }
            else
            {
                foreach (Square square in board)
                {
                    square.hint = false;
                }
            }
            Invalidate();
        }
    }
}
