using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab4
{
    class Square
    {
        public Point square_point;
        Brush square_color;
        Brush Q_color;

        //if hint option is on. 
        public bool hint = false;
        //if queen is on the square.
        public bool queen = false;
        //if queen would eat the square.
        public bool queenAttacks = false;

        public Square(Point p, Brush b)
        {
            square_point = p;
            square_color = b;
        }

        //Paints square red for when hint is on. 
        public void PaintRed (Graphics g)
        {
            g.FillRectangle(Brushes.Red, square_point.X, square_point.Y, 50, 50);
            Q_color = Brushes.Black;
        }

        //color squares black or white and change Q color accordingly. 
        public void ColorSquare(Graphics g)
        {
            g.FillRectangle(square_color, square_point.X, square_point.Y, 50, 50);
            if (square_color == Brushes.Black)
                Q_color = Brushes.White;
            else
                Q_color = Brushes.Black;
        }

        public void PaintSquare(Graphics g)
        {
            if (queenAttacks)
            {
                if (hint) //if hint is on, paint safety red. 
                    PaintRed(g);
                else //paint safety correct colors. 
                    ColorSquare(g);
            }
            else
                ColorSquare(g);

            //draw borders
            g.DrawRectangle(Pens.Black, square_point.X, square_point.Y, 50, 50);
            if (queen)
                g.DrawString("Q", new Font("Arial", 30, FontStyle.Bold), Q_color, square_point.X, square_point.Y);
        }

        public void reset()
        {
            queen = false;
            queenAttacks = false;
        }
    }
}
