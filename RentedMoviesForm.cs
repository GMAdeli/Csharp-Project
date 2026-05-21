using System;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class RentedMoviesForm : Form
    {
        private DataGridView dgvRentals;
        private Panel panelHeader;
        private Label lblHeader;
        private Button btnRefresh;
        private Button btnClose;
        private ComboBox cboMovieFilter;
        private Label lblFilter;

        public RentedMoviesForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Rented Movies";
            this.Size = new System.Drawing.Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 80;
            panelHeader.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);

            lblHeader = new Label();
            lblHeader.Text = "RENTED MOVIES";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            lblFilter = new Label();
            lblFilter.Text = "Filter by Movie:";
            lblFilter.Font = new System.Drawing.Font("Segoe UI", 10);
            lblFilter.Location = new System.Drawing.Point(20, 95);
            lblFilter.Size = new System.Drawing.Size(100, 25);

            cboMovieFilter = new ComboBox();
            cboMovieFilter.Font = new System.Drawing.Font("Segoe UI", 10);
            cboMovieFilter.Location = new System.Drawing.Point(130, 92);
            cboMovieFilter.Size = new System.Drawing.Size(200, 25);
            cboMovieFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMovieFilter.Items.Add("-- All Movies --");
            cboMovieFilter.SelectedIndex = 0;

            dgvRentals = new DataGridView();
            dgvRentals.Location = new System.Drawing.Point(20, 130);
            dgvRentals.Size = new System.Drawing.Size(850, 380);
            dgvRentals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRentals.AllowUserToAddRows = false;
            dgvRentals.AllowUserToDeleteRows = false;
            dgvRentals.ReadOnly = true;
            dgvRentals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRentals.BackgroundColor = System.Drawing.Color.White;
            dgvRentals.BorderStyle = BorderStyle.FixedSingle;
            dgvRentals.RowCount = 1;
            dgvRentals.Rows[0].Cells[0].Value = "No data available";

            btnRefresh = new Button();
            btnRefresh.Text = "REFRESH";
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(680, 515);
            btnRefresh.Size = new System.Drawing.Size(100, 35);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Click += BtnRefresh_Click;

            btnClose = new Button();
            btnClose.Text = "CLOSE";
            btnClose.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnClose.BackColor = System.Drawing.Color.LightGray;
            btnClose.Location = new System.Drawing.Point(790, 515);
            btnClose.Size = new System.Drawing.Size(80, 35);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblFilter);
            this.Controls.Add(cboMovieFilter);
            this.Controls.Add(dgvRentals);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(btnClose);
            this.Controls.Add(panelHeader);
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rented movies display coming soon!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}