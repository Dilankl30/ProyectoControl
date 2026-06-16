namespace AgroControl
{
    partial class dashboard : Form
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
            components = new System.ComponentModel.Container();
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 750);
            FormBorderStyle = FormBorderStyle.None;
            Name = "dashboard";
            Text = "dashboard";
            BackColor = Color.FromArgb(245, 247, 250);
        }
    }
}
