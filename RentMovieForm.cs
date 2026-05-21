using System;
using System.Linq;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class RentMovieForm : Form
    {
        private ComboBox cboMovie;
        private ComboBox cboClient;
        private DateTimePicker dtpRentalDate;
        private NumericUpDown numDays;
        private TextBox txtTotalPrice;
        private Button btnRent;
        private Button btnCancel;
        private Panel panelHeader;
        private Label lblHeader;

        public RentMovieForm()
        {
            InitializeComponent();
            LoadMovies();
            LoadClients();
        }

        private void InitializeComponent()
        {
            this.Text = "Rent Movie";
            this.Size = new System.Drawing.Size(550, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 60;
            panelHeader.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);

            lblHeader = new Label();
            lblHeader.Text = "RENT MOVIE";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 16, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            Label lblMovie = new Label();
            lblMovie.Text = "Select Movie:";
            lblMovie.Font = new System.Drawing.Font("Segoe UI", 11);
            lblMovie.Location = new System.Drawing.Point(40, 90);
            lblMovie.Size = new System.Drawing.Size(120, 30);

            cboMovie = new ComboBox();
            cboMovie.Font = new System.Drawing.Font("Segoe UI", 11);
            cboMovie.Location = new System.Drawing.Point(170, 87);
            cboMovie.Size = new System.Drawing.Size(330, 28);
            cboMovie.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMovie.SelectedIndexChanged += CalculatePrice;

            Label lblClient = new Label();
            lblClient.Text = "Select Client:";
            lblClient.Font = new System.Drawing.Font("Segoe UI", 11);
            lblClient.Location = new System.Drawing.Point(40, 140);
            lblClient.Size = new System.Drawing.Size(120, 30);

            cboClient = new ComboBox();
            cboClient.Font = new System.Drawing.Font("Segoe UI", 11);
            cboClient.Location = new System.Drawing.Point(170, 137);
            cboClient.Size = new System.Drawing.Size(330, 28);
            cboClient.DropDownStyle = ComboBoxStyle.DropDownList;

            Label lblDate = new Label();
            lblDate.Text = "Rental Date:";
            lblDate.Font = new System.Drawing.Font("Segoe UI", 11);
            lblDate.Location = new System.Drawing.Point(40, 190);
            lblDate.Size = new System.Drawing.Size(120, 30);

            dtpRentalDate = new DateTimePicker();
            dtpRentalDate.Font = new System.Drawing.Font("Segoe UI", 11);
            dtpRentalDate.Location = new System.Drawing.Point(170, 187);
            dtpRentalDate.Size = new System.Drawing.Size(200, 28);
            dtpRentalDate.Value = DateTime.Now;

            Label lblDays = new Label();
            lblDays.Text = "Days:";
            lblDays.Font = new System.Drawing.Font("Segoe UI", 11);
            lblDays.Location = new System.Drawing.Point(40, 240);
            lblDays.Size = new System.Drawing.Size(120, 30);

            numDays = new NumericUpDown();
            numDays.Font = new System.Drawing.Font("Segoe UI", 11);
            numDays.Location = new System.Drawing.Point(170, 237);
            numDays.Size = new System.Drawing.Size(100, 28);
            numDays.Minimum = 1;
            numDays.Maximum = 30;
            numDays.Value = 3;
            numDays.ValueChanged += CalculatePrice;

            Label lblPrice = new Label();
            lblPrice.Text = "Total Price:";
            lblPrice.Font = new System.Drawing.Font("Segoe UI", 11);
            lblPrice.Location = new System.Drawing.Point(40, 290);
            lblPrice.Size = new System.Drawing.Size(120, 30);

            txtTotalPrice = new TextBox();
            txtTotalPrice.Font = new System.Drawing.Font("Segoe UI", 11);
            txtTotalPrice.Location = new System.Drawing.Point(170, 287);
            txtTotalPrice.Size = new System.Drawing.Size(150, 28);
            txtTotalPrice.ReadOnly = true;
            txtTotalPrice.Text = "0";

            btnRent = new Button();
            btnRent.Text = "RENT";
            btnRent.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
            btnRent.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnRent.ForeColor = System.Drawing.Color.White;
            btnRent.Location = new System.Drawing.Point(170, 350);
            btnRent.Size = new System.Drawing.Size(130, 40);
            btnRent.FlatStyle = FlatStyle.Flat;
            btnRent.Click += BtnRent_Click;

            btnCancel = new Button();
            btnCancel.Text = "CANCEL";
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 11);
            btnCancel.BackColor = System.Drawing.Color.LightGray;
            btnCancel.Location = new System.Drawing.Point(320, 350);
            btnCancel.Size = new System.Drawing.Size(130, 40);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.Add(panelHeader);
            this.Controls.Add(lblMovie);
            this.Controls.Add(cboMovie);
            this.Controls.Add(lblClient);
            this.Controls.Add(cboClient);
            this.Controls.Add(lblDate);
            this.Controls.Add(dtpRentalDate);
            this.Controls.Add(lblDays);
            this.Controls.Add(numDays);
            this.Controls.Add(lblPrice);
            this.Controls.Add(txtTotalPrice);
            this.Controls.Add(btnRent);
            this.Controls.Add(btnCancel);
        }

        private void LoadMovies()
        {
            var rentedMovieIds = Rental.RentalsList.Where(r => r.ReturnDate == null || r.ReturnDate > DateTime.Now).Select(r => r.MovieId).ToList();
            var availableMovies = Movie.GetAllMovies().Where(m => m.IsAvailable && !rentedMovieIds.Contains(m.MovieId)).ToList();

            var displayList = availableMovies.Select(m => new {
                Display = $"[ID: {m.MovieId}] {m.MovieTitle} ({m.Year})",
                m.MovieId,
                m.MovieTitle,
                m.PricePerDay,
                m.Genre,
                m.Year
            }).ToList();

            cboMovie.DataSource = null;
            cboMovie.DataSource = displayList;
            cboMovie.DisplayMember = "Display";
            cboMovie.ValueMember = "MovieId";
        }

        private void LoadClients()
        {
            var displayList = Client.GetAllClients().Select(c => new {
                Display = $"[ID: {c.ClientId}] {c.FirstName} {c.LastName}",
                c.ClientId,
                c.FirstName,
                c.LastName
            }).ToList();

            cboClient.DataSource = null;
            cboClient.DataSource = displayList;
            cboClient.DisplayMember = "Display";
            cboClient.ValueMember = "ClientId";
        }

        private void CalculatePrice(object sender, EventArgs e)
        {
            if (cboMovie.SelectedItem != null)
            {
                dynamic selected = cboMovie.SelectedItem;
                double pricePerDay = selected.PricePerDay;
                int days = (int)numDays.Value;
                double price = pricePerDay * days;
                txtTotalPrice.Text = price.ToString("F2") + " lei";
            }
        }

        private void BtnRent_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboMovie.SelectedItem == null)
                {
                    MessageBox.Show("Select a movie!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboClient.SelectedItem == null)
                {
                    MessageBox.Show("Select a client!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dynamic selectedMovie = cboMovie.SelectedItem;
                dynamic selectedClient = cboClient.SelectedItem;

                int movieId = selectedMovie.MovieId;
                int clientId = selectedClient.ClientId;
                string movieTitle = selectedMovie.MovieTitle;
                string clientName = selectedClient.FirstName;
                double pricePerDay = selectedMovie.PricePerDay;
                int days = (int)numDays.Value;
                double totalPrice = pricePerDay * days;
                DateTime returnDate = dtpRentalDate.Value.AddDays(days);

                Rental newRental = new Rental(movieId, clientId, dtpRentalDate.Value, returnDate, totalPrice);

                Movie movie = Movie.GetAllMovies().FirstOrDefault(m => m.MovieId == movieId);
                if (movie != null)
                {
                    movie.IsAvailable = false;
                    Movie.SaveToFile();
                }

                MessageBox.Show($"Movie '{movieTitle}' rented to {clientName}!\nReturn Date: {returnDate:yyyy-MM-dd}\nTotal: {totalPrice:F2} lei",
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}