using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace MovieRental
{
    public partial class RentedMoviesForm : Form
    {
        private DataGridView dgvRentals;
        private Panel panelHeader;
        private Label lblHeader;
        private Button btnReturn;
        private Button btnExportReport;
        private ContextMenuStrip contextMenu;

        private List<RentedMovieView> currentDisplayData;

        public RentedMoviesForm()
        {
            InitializeComponent();
            LoadRentals();
        }

        private void InitializeComponent()
        {
            this.Text = "Currently Rented Movies";
            this.Size = new System.Drawing.Size(1100, 600);
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
            lblHeader.Text = "CURRENTLY RENTED MOVIES";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            dgvRentals = new DataGridView();
            dgvRentals.Location = new System.Drawing.Point(20, 100);
            dgvRentals.Size = new System.Drawing.Size(1060, 380);
            dgvRentals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRentals.AllowUserToAddRows = false;
            dgvRentals.AllowUserToDeleteRows = false;
            dgvRentals.ReadOnly = true;
            dgvRentals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRentals.BackgroundColor = System.Drawing.Color.White;
            dgvRentals.BorderStyle = BorderStyle.FixedSingle;
            dgvRentals.MultiSelect = false;

            contextMenu = new ContextMenuStrip();

            ToolStripMenuItem sortById = new ToolStripMenuItem("&Sort by ID");
            sortById.Click += SortById_Click;

            ToolStripMenuItem sortByName = new ToolStripMenuItem("&Sort by Name");
            sortByName.Click += SortByName_Click;

            ToolStripMenuItem sortByPrice = new ToolStripMenuItem("&Sort by Price");
            sortByPrice.Click += SortByPrice_Click;

            ToolStripMenuItem separator = new ToolStripMenuItem("-");

            ToolStripMenuItem refreshMenu = new ToolStripMenuItem("&Refresh");
            refreshMenu.Click += BtnRefresh_Click;

            contextMenu.Items.Add(sortById);
            contextMenu.Items.Add(sortByName);
            contextMenu.Items.Add(sortByPrice);
            contextMenu.Items.Add(separator);
            contextMenu.Items.Add(refreshMenu);

            dgvRentals.ContextMenuStrip = contextMenu;

            btnReturn = new Button();
            btnReturn.Text = "&Return Selected Movie";
            btnReturn.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnReturn.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnReturn.ForeColor = System.Drawing.Color.White;
            btnReturn.Location = new System.Drawing.Point(20, 500);
            btnReturn.Size = new System.Drawing.Size(180, 40);
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.Click += BtnReturn_Click;

            btnExportReport = new Button();
            btnExportReport.Text = "&Export Report";
            btnExportReport.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnExportReport.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnExportReport.ForeColor = System.Drawing.Color.White;
            btnExportReport.Location = new System.Drawing.Point(940, 500);
            btnExportReport.Size = new System.Drawing.Size(140, 40);
            btnExportReport.FlatStyle = FlatStyle.Flat;
            btnExportReport.Click += BtnExportReport_Click;

            this.Controls.Add(dgvRentals);
            this.Controls.Add(btnReturn);
            this.Controls.Add(btnExportReport);
            this.Controls.Add(panelHeader);
        }

        private void LoadRentals()
        {
            var activeRentals = Rental.RentalsList.Where(r => r.ReturnDate == null).ToList();

            currentDisplayData = new List<RentedMovieView>();

            foreach (var rental in activeRentals)
            {
                Movie movie = Movie.GetAllMovies().FirstOrDefault(m => m.MovieId == rental.MovieId);
                Client client = Client.GetAllClients().FirstOrDefault(c => c.ClientId == rental.ClientId);

                if (movie != null && client != null)
                {
                    currentDisplayData.Add(new RentedMovieView
                    {
                        RentalId = rental.RentalId,
                        MovieId = movie.MovieId,
                        MovieTitle = movie.MovieTitle,
                        PricePerDay = movie.PricePerDay,
                        ClientName = $"{client.FirstName} {client.LastName}",
                        ClientEmail = client.Email,
                        RentalDate = rental.RentalDate,
                        ReturnDate = "Active",
                        TotalPrice = rental.TotalPrice.ToString("F2") + " lei"
                    });
                }
            }

            BindDataToGrid();
        }

        private void BindDataToGrid()
        {
            var displayGridData = currentDisplayData.Select(d => new
            {
                d.MovieId,
                d.MovieTitle,
                d.ClientName,
                d.ClientEmail,
                RentalDate = d.RentalDate.ToString("yyyy-MM-dd"),
                ReturnDate = d.ReturnDate,
                d.TotalPrice
            }).ToList();

            dgvRentals.DataSource = null;
            dgvRentals.DataSource = displayGridData;

            if (dgvRentals.Columns.Contains("MovieId"))
                dgvRentals.Columns["MovieId"].HeaderText = "Movie ID";
            if (dgvRentals.Columns.Contains("MovieTitle"))
                dgvRentals.Columns["MovieTitle"].HeaderText = "Movie Title";
            if (dgvRentals.Columns.Contains("ClientName"))
                dgvRentals.Columns["ClientName"].HeaderText = "Client Name";
            if (dgvRentals.Columns.Contains("ClientEmail"))
                dgvRentals.Columns["ClientEmail"].HeaderText = "Client Email";
            if (dgvRentals.Columns.Contains("RentalDate"))
                dgvRentals.Columns["RentalDate"].HeaderText = "Rental Date";
            if (dgvRentals.Columns.Contains("ReturnDate"))
                dgvRentals.Columns["ReturnDate"].HeaderText = "Status";
            if (dgvRentals.Columns.Contains("TotalPrice"))
                dgvRentals.Columns["TotalPrice"].HeaderText = "Total Price";
        }

        private void SortById_Click(object sender, EventArgs e)
        {
            if (currentDisplayData != null)
            {
                currentDisplayData = currentDisplayData.OrderBy(d => d.MovieId).ToList();
                BindDataToGrid();
            }
        }

        private void SortByName_Click(object sender, EventArgs e)
        {
            if (currentDisplayData != null)
            {
                currentDisplayData = currentDisplayData.OrderBy(d => d.MovieTitle).ToList();
                BindDataToGrid();
            }
        }

        private void SortByPrice_Click(object sender, EventArgs e)
        {
            if (currentDisplayData != null)
            {
                currentDisplayData = currentDisplayData.OrderBy(d => d.PricePerDay).ToList();
                BindDataToGrid();
            }
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            if (dgvRentals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a rental to return!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int movieId = (int)dgvRentals.SelectedRows[0].Cells["MovieId"].Value;
            string movieTitle = dgvRentals.SelectedRows[0].Cells["MovieTitle"].Value.ToString();

            DialogResult result = MessageBox.Show($"Return '{movieTitle}'?", "Confirm Return",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var rental = Rental.RentalsList.FirstOrDefault(r => r.MovieId == movieId && r.ReturnDate == null);
                if (rental != null)
                {
                    rental.ReturnDate = DateTime.Now;

                    var movie = Movie.GetAllMovies().FirstOrDefault(m => m.MovieId == movieId);
                    if (movie != null)
                    {
                        movie.IsAvailable = true;
                    }

                    Rental.SaveToFile();
                    Movie.SaveToFile();

                    MessageBox.Show($"'{movieTitle}' returned successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadRentals();

                    var mainForm = Application.OpenForms.OfType<MenuForm>().FirstOrDefault();
                    if (mainForm != null) mainForm.UpdateStatusStrip();
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadRentals();
        }

        private void BtnExportReport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Text Files|*.txt";
                saveDialog.Title = "Export Active Rentals Report";
                saveDialog.FileName = $"ActiveRentals_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                    {
                        writer.WriteLine("CURRENTLY RENTED MOVIES REPORT");
                        writer.WriteLine($"Generated: {DateTime.Now}");
                        writer.WriteLine(new string('-', 90));
                        writer.WriteLine();

                        writer.WriteLine("Movie ID   Movie Title                    Client Name               Client Email              Rental Date   Total Price");
                        writer.WriteLine(new string('-', 90));

                        foreach (DataGridViewRow row in dgvRentals.Rows)
                        {
                            if (row.IsNewRow) continue;

                            string movieId = row.Cells["MovieId"].Value?.ToString() ?? "";
                            string movieTitle = row.Cells["MovieTitle"].Value?.ToString() ?? "";
                            string clientName = row.Cells["ClientName"].Value?.ToString() ?? "";
                            string clientEmail = row.Cells["ClientEmail"].Value?.ToString() ?? "";
                            string rentalDate = row.Cells["RentalDate"].Value?.ToString() ?? "";
                            string totalPrice = row.Cells["TotalPrice"].Value?.ToString() ?? "";

                            writer.WriteLine($"{movieId,-9} {movieTitle,-30} {clientName,-25} {clientEmail,-25} {rentalDate,-12} {totalPrice}");
                        }

                        writer.WriteLine();
                        writer.WriteLine(new string('-', 90));
                        writer.WriteLine($"Total active rentals: {dgvRentals.Rows.Count}");
                    }

                    MessageBox.Show($"Report exported to:\n{saveDialog.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private class RentedMovieView
        {
            public int RentalId { get; set; }
            public int MovieId { get; set; }
            public string MovieTitle { get; set; }
            public double PricePerDay { get; set; }
            public string ClientName { get; set; }
            public string ClientEmail { get; set; }
            public DateTime RentalDate { get; set; }
            public string ReturnDate { get; set; }
            public string TotalPrice { get; set; }
        }
    }
}