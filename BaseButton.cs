using System;
using System.Drawing;
using System.Windows.Forms;

namespace Views;

public abstract class BaseButton
{
    public PointF? Location { get; set; }

    public virtual void DrawTeam (Graphics g, Image image, RectangleF Rect) { }
    public virtual void DrawSelected (Graphics g, Image image, RectangleF Rect) { }
}
