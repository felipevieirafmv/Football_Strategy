using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;

public abstract class BasePiece
{
    public virtual void DrawPiece(Graphics g) { }
    public virtual void DrawEmptyPiece(Graphics g) { }

}
