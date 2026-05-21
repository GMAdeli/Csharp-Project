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
        private Button btnClose;
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
            cboMovieFilter.SelectedIndexChanged += CboMovieFilter_SelectedIndexChanged;

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

        private void LoadMovieFilter()
        {
            var movies = Movie.GetAllMovies();
            var filterList = new List<MovieFilterItem>();
            filterList.Add(new MovieFilterItem { Id = 0, Name = "-- All Movies --" });
            foreach (var movie in movies)
            {
                filterList.Add(new MovieFilterItem { Id = movie.MovieId, Name = movie.MovieTitle });
            }
            cboMovieFilter.DataSource = null;
            cboMovieFilter.DataSource = filterList;
            cboMovieFilter.DisplayMember = "Name";
            cboMovieFilter.ValueMember = "Id";
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
        }

        private void CboMovieFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRentals();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
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
            public string MovieTitle { get; set; }
            public string ClientName { get; set; }
            public string ClientEmail { get; set; }
            public DateTime RentalDate { get; set; }
            public string ReturnDate { get; set; }
            public string TotalPrice { get; set; }
        }
    }
}