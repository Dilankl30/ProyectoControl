using System.Drawing.Drawing2D;

namespace AgroControl
{
    public partial class plantRecord : Form
    {
        public plantRecord()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            var title = new Label
            {
                Text = "Plant Records",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 45, 60),
                Location = new Point(25, 20),
                AutoSize = true
            };
            Controls.Add(title);

            var subtitle = new Label
            {
                Text = "Register and monitor all plants in your greenhouse.",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(140, 150, 170),
                Location = new Point(27, 55),
                AutoSize = true
            };
            Controls.Add(subtitle);

            var grid = new DataGridView
            {
                Location = new Point(25, 85),
                Size = new Size(600, 200),
                BackColor = Color.White,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(235, 238, 242),
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9),
                RowTemplate = { Height = 28 }
            };
            grid.Columns.Add("Plant", "Plant");
            grid.Columns.Add("Type", "Type");
            grid.Columns.Add("Planted On", "Planted On");
            grid.Columns.Add("Status", "Status");
            grid.Rows.Add("Tomato", "Fruit", "Apr 28, 2025", "Active");
            grid.Rows.Add("Lettuce", "Leafy Green", "Apr 30, 2025", "Active");
            grid.Rows.Add("Pepper", "Fruit", "Apr 29, 2025", "Active");
            grid.Rows.Add("Cucumber", "Fruit", "May 01, 2025", "Active");

            grid.Paint += (s, e) =>
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = new GraphicsPath();
                int w = grid.Width, h = grid.Height;
                path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                path.CloseFigure(); grid.Region = new Region(path);
            };

            Controls.Add(grid);
        }
    }
}
