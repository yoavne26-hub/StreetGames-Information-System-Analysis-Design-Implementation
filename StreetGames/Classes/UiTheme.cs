using System.Drawing;
using System.Windows.Forms;

namespace StreetGames
{
    public static class UiTheme
    {
        public static void ApplyArcade(Form f)
        {
            f.BackColor = Color.FromArgb(30, 30, 40);
            f.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            foreach (Control c in f.Controls)
            {
                if (c is Button b)
                {
                    b.BackColor = Color.FromArgb(255, 193, 7);
                    b.ForeColor = Color.Black;
                    b.FlatStyle = FlatStyle.Flat;
                }

                if (c is Label l)
                {
                    l.ForeColor = Color.White;
                }
            }
        }
    }
}
