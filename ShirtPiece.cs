// using System;
// using System.Drawing;
// using System.Windows.Forms;

// namespace Views;

// public class ShirtPiece : BasePiece
// {
//     public RectangleF Rect { get; set; }
//     public Image Image { get; set; }

//     public ShirtPiece(Graphics g, RectangleF rect, Image image)
//     {
//         this.Rect = rect;
//         this.Image = image;
//     }

//     public override void DrawPiece(Graphics g)
//     {
//         float realWidth = location.Width;
//         var realSize = new SizeF(location.Width, location.Height);
 
//         var deslocX = grabDesloc?.X ?? 0;
//         var deslocY = grabDesloc?.Y ?? 0;

//         PointF position = new PointF(location.X + deslocX, location.Y + deslocY);
//         RectangleF rect = new RectangleF(position, realSize);
 
//         bool cursorIn = rect.Contains(cursor);
 
//         if (!cursorIn && (deslocX != 0 || deslocY != 0))
//             rect = new RectangleF(location.Location, realSize);


//         var pen = new Pen(cursorIn ? Color.Green : Color.Black, 1f);
//         var yellowPen = new Pen(Color.Black, 3f);
//         var whitePen = new Pen(Color.Black, 2f);
 
//         g.FillRectangle(Brushes.LightBlue, rect);
//         g.DrawRectangle(pen, rect.X, rect.Y, realWidth, rect.Height);

//         if (!cursorIn || !isDown)
//             return rect;
         
//         if (grabStart == null)
//         {
//             DrawEmptyPiece(location);
//             DrawShirt(image,location);
//             grabStart = cursor;
//             return rect;
//         }
        
//         shirtDesloc = new PointF(location.X = cursor.X - grabStart.Value.X, location.Y = cursor.Y - grabStart.Value.Y);
//         grabDesloc = new PointF(cursor.X - grabStart.Value.X, cursor.Y - grabStart.Value.Y);
//     }
// }