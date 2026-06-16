using System.Drawing.Drawing2D;

namespace AgroControl
{
    public partial class settings : Form
    {
        public settings()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            var title = new Label
            {
                Text = "Settings",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 45, 60),
                Location = new Point(25, 20),
                AutoSize = true
            };
            Controls.Add(title);

            var subtitle = new Label
            {
                Text = "Configure device, database connection, and alert limits.",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(140, 150, 170),
                Location = new Point(27, 55),
                AutoSize = true
            };
            Controls.Add(subtitle);

            var panels = new (string, string[])[]
            {
                ("Database Connection", new[] { "Host: db.ezuqrhdtpwbxnatzlzrt.supabase.co", "Database: postgres", "User: postgres", "SSL: Required" }),
                ("Reading Interval", new[] { "Interval: 30 minutes", "Last reading: 11:30 AM", "Next reading: 12:00 PM" }),
                ("Alert Limits", new[] { "Temp min: 22 °C", "Temp max: 28 °C", "Soil min: 30 %", "Soil max: 60 %", "Humidity min: 60 %", "Humidity max: 80 %" })
            };

            int x = 25;
            foreach (var (pTitle, items) in panels)
            {
                var panel = new Panel
                {
                    Location = new Point(x, 85),
                    Size = new Size(300, 200),
                    BackColor = Color.White
                };
                panel.Paint += (s, e) =>
                {
                    var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                    using var path = new GraphicsPath();
                    int w = panel.Width, h = panel.Height;
                    path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                    path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                    path.CloseFigure(); panel.Region = new Region(path);
                };

                var lbl = new Label
                {
                    Text = pTitle,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.FromArgb(70, 72, 230),
                    Location = new Point(12, 10),
                    AutoSize = true
                };
                panel.Controls.Add(lbl);

                int iy = 38;
                foreach (var item in items)
                {
                    var li = new Label
                    {
                        Text = item,
                        Font = new Font("Segoe UI", 9),
                        ForeColor = Color.FromArgb(80, 90, 110),
                        Location = new Point(12, iy),
                        AutoSize = true
                    };
                    panel.Controls.Add(li);
                    iy += 24;
                }

                Controls.Add(panel);
                x += 320;
            }
        }
    }
}
