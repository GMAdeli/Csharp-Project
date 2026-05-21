using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class RentedMoviesForm : Form
    {
        private DataGridView dgvRentals;
        private Panel panelHeader;
        private Label lblHeader;
        private Button btnRefresh;
        private Button btnReturn;
        private ComboBox cboMovieFilter;
        private Label lblFilter;

        public RentedMoviesForm()
        {
            InitializeComponent();
            LoadMovieFilter();
            LoadRentals();
        }

        private void InitializeComponent()
        {
            this.Text = "Rented Movies";
            this.Size = new System.Drawing.Size(950, 500);
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
            cboMovieFilter.SelectedIndexChanged += CboMovieFilter_SelectedIndexChanged;

            dgvRentals = new DataGridView();
            dgvRentals.Location = new System.Drawing.Point(20, 130);
            dgvRentals.Size = new System.Drawing.Size(900, 280);
            dgvRentals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRentals.AllowUserToAddRows = false;
            dgvRentals.AllowUserToDeleteRows = false;
            dgvRentals.ReadOnly = true;
            dgvRentals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRentals.BackgroundColor = System.Drawing.Color.White;
            dgvRentals.BorderStyle = BorderStyle.FixedSingle;
            dgvRentals.MultiSelect = false;

            btnReturn = new Button();
            btnReturn.Text = "RETURN SELECTED MOVIE";
            btnReturn.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnReturn.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnReturn.ForeColor = System.Drawing.Color.White;
            btnReturn.Location = new System.Drawing.Point(20, 425);
            btnReturn.Size = new System.Drawing.Size(180, 35);
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.Click += BtnReturn_Click;

            btnRefresh = new Button();
            btnRefresh.Text = "REFRESH";
            btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10, FontStyle.Bold);
            btnRefresh.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnRefresh.ForeColor = System.Drawing.Color.White;
            btnRefresh.Location = new System.Drawing.Point(820, 425);
            btnRefresh.Size = new System.Drawing.Size(100, 35);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Click += BtnRefresh_Click;

            this.Controls.Add(lblFilter);
            this.Controls.Add(cboMovieFilter);
            this.Controls.Add(dgvRentals);
            this.Controls.Add(btnReturn);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(panelHeader);
        }

        private void LoadMovieFilter()
        {
            var movies = Movie.GetAllMovies();
            var filterList = new List<MovieFilterItem>();
            filterList.Add(new MovieFilterItem { Id = 0, Name = "-- All Movies --" });
            foreach (var movie in movies)
            {
                filterList.Add(new MovieFilterItem { Id = movie.MovieId, Name = movie.MovieTitle });
            }

            int previousSelectedId = 0;
            if (cboMovieFilter.SelectedItem != null)
            {
                previousSelectedId = ((MovieFilterItem)cboMovieFilter.SelectedItem).Id;
            }

            cboMovieFilter.DataSource = null;
            cboMovieFilter.DataSource = filterList;
            cboMovieFilter.DisplayMember = "Name";
            cboMovieFilter.ValueMember = "Id";

            if (previousSelectedId > 0 && filterList.Any(f => f.Id == previousSelectedId))
            {
                cboMovieFilter.SelectedItem = filterList.FirstOrDefault(f => f.Id == previousSelectedId);
            }
            else
            {
                cboMovieFilter.SelectedIndex = 0;
            }
        }

        private void LoadRentals()
        {
            var rentals = Rental.RentalsList.Where(r => r.ReturnDate == null || r.ReturnDate > DateTime.Now).ToList();

            int filterId = 0;
            if (cboMovieFilter.SelectedItem != null)
            {
                var selectedItem = (MovieFilterItem)cboMovieFilter.SelectedItem;
                filterId = selectedItem.Id;
            }

            if (filterId > 0)
            {
                rentals = rentals.Where(r => r.MovieId == filterId).ToList();
            }

            var displayData = new List<RentedMovieView>();

            foreach (var rental in rentals)
            {
                Movie movie = Movie.GetAllMovies().FirstOrDefault(m => m.MovieId == rental.MovieId);
                Client client = Client.GetAllClients().FirstOrDefault(c => c.ClientId == rental.ClientId);

                if (movie != null && client != null)
                {
                    displayData.Add(new RentedMovieView
                    {
                        RentalId = rental.RentalId,
                        MovieTitle = movie.MovieTitle,
                        ClientName = $"{client.FirstName} {client.LastName}",
                        ClientEmail = client.Email,
                        RentalDate = rental.RentalDate,
                        ReturnDate = rental.ReturnDate.HasValue ? rental.ReturnDate.Value.ToString("yyyy-MM-dd") : "Not returned",
                        TotalPrice = rental.TotalPrice.ToString("F2") + " lei"
                    });
                }
            }

            dgvRentals.DataSource = null;
            dgvRentals.DataSource = displayData;

            if (dgvRentals.Columns.Contains("RentalId"))
                dgvRentals.Columns["RentalId"].Visible = false;
        }

        private void BtnReturn_Click(object sender, EventArgs e)
        {
            if (dgvRentals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a rental to return!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rentalId = (int)dgvRentals.SelectedRows[0].Cells["RentalId"].Value;
            var rental = Rental.RentalsList.FirstOrDefault(r => r.RentalId == rentalId);

            if (rental == null)
            {
                MessageBox.Show("Rental not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string movieTitle = dgvRentals.SelectedRows[0].Cells["MovieTitle"].Value.ToString();

            DialogResult result = MessageBox.Show($"Return '{movieTitle}'?", "Confirm Return",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                rental.ReturnDate = DateTime.Now;

                var movie = Movie.GetAllMovies().FirstOrDefault(m => m.MovieId == rental.MovieId);
                if (movie != null)
                {
                    movie.IsAvailable = true;
                }

                Rental.SaveToFile();
                Movie.SaveToFile();

                MessageBox.Show($"'{movieTitle}' returned successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMovieFilter();
                LoadRentals();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadMovieFilter();
            LoadRentals();
        }

        private void CboMovieFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRentals();
        }

        private class MovieFilterItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class RentedMovieView
        {
            public int RentalId { get; set; }
            public string MovieTitle { get; set; }
            public string ClientName { get; set; }
            public string ClientEmail { get; set; }
            public DateTime RentalDate { get; set; }
            public string ReturnDate { get; set; }
            public string TotalPrice { get; set; }
        }
    }
}