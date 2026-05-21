using System;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class MenuForm : Form
    {
        private Button btnAddMovie;
        private Button btnAddClient;
        private Button btnRentMovie;
        private Button btnRentedMovies;
        private Button btnExit;
        private Panel panelHeader;
        private Label lblHeader;

        public MenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Movie Rental Store";
            this.Size = new System.Drawing.Size(500, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = System.Drawing.Color.WhiteSmoke;

            panelHeader = new Panel();
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 80;
            panelHeader.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);

            lblHeader = new Label();
            lblHeader.Text = "MOVIE RENTAL STORE";
            lblHeader.ForeColor = System.Drawing.Color.White;
            lblHeader.Font = new System.Drawing.Font("Segoe UI", 18, FontStyle.Bold);
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Dock = DockStyle.Fill;
            panelHeader.Controls.Add(lblHeader);

            btnAddMovie = new Button();
            btnAddMovie.Text = "Add Movie";
            btnAddMovie.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnAddMovie.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnAddMovie.ForeColor = System.Drawing.Color.White;
            btnAddMovie.Location = new System.Drawing.Point(125, 100);
            btnAddMovie.Size = new System.Drawing.Size(250, 50);
            btnAddMovie.FlatStyle = FlatStyle.Flat;
            btnAddMovie.Click += BtnAddMovie_Click;

            btnAddClient = new Button();
            btnAddClient.Text = "Add Client";
            btnAddClient.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnAddClient.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnAddClient.ForeColor = System.Drawing.Color.White;
            btnAddClient.Location = new System.Drawing.Point(125, 170);
            btnAddClient.Size = new System.Drawing.Size(250, 50);
            btnAddClient.FlatStyle = FlatStyle.Flat;
            btnAddClient.Click += BtnAddClient_Click;

            btnRentMovie = new Button();
            btnRentMovie.Text = "Rent Movie";
            btnRentMovie.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnRentMovie.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnRentMovie.ForeColor = System.Drawing.Color.White;
            btnRentMovie.Location = new System.Drawing.Point(125, 240);
            btnRentMovie.Size = new System.Drawing.Size(250, 50);
            btnRentMovie.FlatStyle = FlatStyle.Flat;
            btnRentMovie.Click += BtnRentMovie_Click;

            btnRentedMovies = new Button();
            btnRentedMovies.Text = "Rented Movies";
            btnRentedMovies.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnRentedMovies.BackColor = System.Drawing.Color.FromArgb(54, 70, 214);
            btnRentedMovies.ForeColor = System.Drawing.Color.White;
            btnRentedMovies.Location = new System.Drawing.Point(125, 310);
            btnRentedMovies.Size = new System.Drawing.Size(250, 50);
            btnRentedMovies.FlatStyle = FlatStyle.Flat;
            btnRentedMovies.Click += BtnRentedMovies_Click;

            btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold);
            btnExit.BackColor = System.Drawing.Color.DarkRed;
            btnExit.ForeColor = System.Drawing.Color.White;
            btnExit.Location = new System.Drawing.Point(125, 380);
            btnExit.Size = new System.Drawing.Size(250, 50);
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Click += (s, e) => Application.Exit();

            this.Controls.Add(panelHeader);
            this.Controls.Add(btnAddMovie);
            this.Controls.Add(btnAddClient);
            this.Controls.Add(btnRentMovie);
            this.Controls.Add(btnRentedMovies);
            this.Controls.Add(btnExit);
        }

        private void BtnAddMovie_Click(object sender, EventArgs e)
        {
            NewMovieForm movieForm = new NewMovieForm();
            movieForm.ShowDialog();
        }

        private void BtnAddClient_Click(object sender, EventArgs e)
        {
            NewClientForm clientForm = new NewClientForm();
            clientForm.ShowDialog();
        }

        private void BtnRentMovie_Click(object sender, EventArgs e)
        {
            RentMovieForm rentForm = new RentMovieForm();
            rentForm.ShowDialog();
        }

        private void BtnRentedMovies_Click(object sender, EventArgs e)
        {
            RentedMoviesForm rentedForm = new RentedMoviesForm();
            rentedForm.ShowDialog();
        }
    }
}