namespace AgroControl
{
    partial class settings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 500);
            FormBorderStyle = FormBorderStyle.None;
            Name = "settings";
            Text = "Settings";
            BackColor = Color.FromArgb(245, 247, 250);
        }
    }
}
