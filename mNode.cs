using System.Drawing;
using System.Windows.Forms;



namespace myGraf
{
    public class mNode
    {
        private int x, y;
        public int X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        private Color nodeColor;
        public Color NodeColor { get { return nodeColor; } set { nodeColor = value; } }
        private sbyte level;
        public sbyte Level { get { return level; } set { level = value; } }
        public mNode(int x, int y) { this.x = x; this.y = y; viewed = true; nodeColor = Color.LightGray; level = -1; }
        private bool viewed;
        public bool Viewed { get { return viewed; } set { viewed = value; } }
        public void Draw(object sender, PaintEventArgs e, int chars)
        {
            if (this.x > 0)
            {
                e.Graphics.FillEllipse(new SolidBrush(nodeColor), x - 2, y - 2, 20, 20);
                e.Graphics.DrawEllipse(new Pen(Color.Blue, 2), x - 2, y - 2, 20, 20);
                if (chars < 9) e.Graphics.DrawString((chars + 1).ToString(), new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((int)(204))), new SolidBrush(Color.Black), x + 3, y + 3);
                else e.Graphics.DrawString((chars + 1).ToString(), new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((int)(204))), new SolidBrush(Color.Black), x, y);
                if (level >= 0) e.Graphics.DrawString(level.ToString(), new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((int)(204))), new SolidBrush(Color.Black), x - 20, y - 20);
            }
        }
    } 
}
