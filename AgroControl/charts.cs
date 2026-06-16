using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;

namespace AgroControl
{
    public partial class charts : Form
    {
        public charts()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            var title = new Label
            {
                Text = "Charts",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 45, 60),
                Location = new Point(25, 20),
                AutoSize = true
            };
            Controls.Add(title);

            var subtitle = new Label
            {
                Text = "Visualize temperature, humidity and soil moisture trends.",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(140, 150, 170),
                Location = new Point(27, 55),
                AutoSize = true
            };
            Controls.Add(subtitle);

            var chart = new Chart { Location = new Point(25, 85), Size = new Size(950, 400), BackColor = Color.White };
            chart.Paint += (s, e) =>
            {
                var g = e.Graphics; g.SmoothingMode = SmoothingMode.AntiAlias;
                using var path = new GraphicsPath();
                int w = chart.Width, h = chart.Height;
                path.AddArc(0, 0, 10, 10, 180, 90); path.AddArc(w - 10, 0, 10, 10, 270, 90);
                path.AddArc(w - 10, h - 10, 10, 10, 0, 90); path.AddArc(0, h - 10, 10, 10, 90, 90);
                path.CloseFigure(); chart.Region = new Region(path);
            };

            var ca = chart.ChartAreas.Add("Main");
            ca.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 242, 245);
            ca.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 242, 245);
            ca.BackColor = Color.White;

            var s1 = chart.Series.Add("Temperature (°C)");
            s1.ChartType = SeriesChartType.Spline; s1.Color = Color.FromArgb(255, 90, 70); s1.BorderWidth = 2;
            for (int i = 0; i < 24; i++) s1.Points.AddXY($"{i}h", 24 + new Random().NextDouble() * 4);

            var s2 = chart.Series.Add("Soil Moisture (%)");
            s2.ChartType = SeriesChartType.Spline; s2.Color = Color.FromArgb(50, 140, 255); s2.BorderWidth = 2;
            for (int i = 0; i < 24; i++) s2.Points.AddXY($"{i}h", 30 + new Random().NextDouble() * 20);

            var s3 = chart.Series.Add("Air Humidity (%)");
            s3.ChartType = SeriesChartType.Spline; s3.Color = Color.FromArgb(140, 80, 255); s3.BorderWidth = 2;
            for (int i = 0; i < 24; i++) s3.Points.AddXY($"{i}h", 60 + new Random().NextDouble() * 15);

            Controls.Add(chart);
        }
    }
}
