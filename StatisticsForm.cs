using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MovieRental
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
            this.Paint += StatisticsForm_Paint;
        }

        private void InitializeComponent()
        {
            this.Text = "Movie Rental Statistics";
            this.Size = new System.Drawing.Size(850, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.White;
            this.DoubleBuffered = true;
        }

        private void StatisticsForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var movies = Movie.GetAllMovies();
            var rentals = Rental.RentalsList;

            if (movies == null || movies.Count == 0)
            {
                Font messageFont = new Font("Segoe UI", 16, FontStyle.Bold);
                Font subFont = new Font("Segoe UI", 12, FontStyle.Regular);

                g.DrawString("🎬 NO MOVIES IN DATABASE", messageFont, Brushes.DarkSlateBlue, 280, 200);
                g.DrawString("Go to 'Add Movie' and add some movies first!", subFont, Brushes.Gray, 270, 250);

                g.DrawLine(new Pen(Brushes.LightGray, 2), 150, 290, 700, 290);

                g.DrawString("Tip: Add movies, then rent them to see statistics", new Font("Segoe UI", 10, FontStyle.Italic), Brushes.DimGray, 260, 320);
                return;
            }

            if (rentals == null || rentals.Count == 0)
            {
                Font messageFont = new Font("Segoe UI", 16, FontStyle.Bold);
                Font subFont = new Font("Segoe UI", 12, FontStyle.Regular);

                g.DrawString("📀 NO RENTALS YET", messageFont, Brushes.DarkSlateBlue, 300, 200);
                g.DrawString("Rent some movies to see statistics here!", subFont, Brushes.Gray, 290, 250);

                g.DrawLine(new Pen(Brushes.LightGray, 2), 150, 290, 700, 290);

                g.DrawString("Tip: Go to 'Rent Movie' and rent a few movies", new Font("Segoe UI", 10, FontStyle.Italic), Brushes.DimGray, 280, 320);
                return;
            }

            var rentalCounts = rentals
                .GroupBy(r => r.MovieId)
                .Select(g => new
                {
                    MovieId = g.Key,
                    Count = g.Count(),
                    Movie = movies.FirstOrDefault(m => m.MovieId == g.Key)
                })
                .Where(x => x.Movie != null)
                .OrderByDescending(x => x.Count)
                .Take(3)
                .ToList();

            if (rentalCounts.Count == 0)
            {
                Font messageFont = new Font("Segoe UI", 14, FontStyle.Bold);
                g.DrawString("No rental statistics available", messageFont, Brushes.Gray, 300, 250);
                return;
            }

            int maxCount = rentalCounts.Max(x => x.Count);
            if (maxCount == 0) maxCount = 1;

            int chartWidth = 450;
            int chartHeight = 300;
            int startX = 120;
            int startY = 130;
            int barWidth = 70;
            int spacing = 50;

            Font titleFont = new Font("Segoe UI", 14, FontStyle.Bold);
            g.DrawString("TOP 3 MOST RENTED MOVIES", titleFont, Brushes.DarkSlateBlue, 280, 30);

            Font labelFont = new Font("Segoe UI", 9, FontStyle.Regular);

            for (int i = 0; i < rentalCounts.Count; i++)
            {
                var item = rentalCounts[i];
                int barHeight = (int)((double)item.Count / maxCount * chartHeight);
                if (barHeight < 5 && item.Count > 0) barHeight = 5;
                int x = startX + i * (barWidth + spacing);
                int y = startY + chartHeight - barHeight;

                using (SolidBrush barBrush = new SolidBrush(Color.SteelBlue))
                {
                    g.FillRectangle(barBrush, x, y, barWidth, barHeight);
                }
                g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight);

                string movieName = item.Movie.MovieTitle;
                if (movieName.Length > 12) movieName = movieName.Substring(0, 10) + "...";
                g.DrawString(movieName, labelFont, Brushes.Black, x + 5, startY + chartHeight + 5);

                g.DrawString(item.Count.ToString(), new Font("Segoe UI", 8, FontStyle.Bold), Brushes.Black, x + barWidth / 2 - 8, y - 18);
            }

            g.DrawLine(Pens.Black, startX - 10, startY + chartHeight, startX + chartWidth + 30, startY + chartHeight);
            g.DrawLine(Pens.Black, startX - 10, startY, startX - 10, startY + chartHeight);

            for (int i = 0; i <= 4; i++)
            {
                int yLabel = startY + chartHeight - (i * chartHeight / 4);
                int countValue = (int)(i * maxCount / 4.0);
                g.DrawString(countValue.ToString(), labelFont, Brushes.Black, startX - 35, yLabel - 6);
                g.DrawLine(Pens.LightGray, startX - 5, yLabel, startX + chartWidth + 30, yLabel);
            }

            g.DrawString("Movies", labelFont, Brushes.Black, startX + chartWidth / 2 - 20, startY + chartHeight + 35);
            g.DrawString("Times Rented", labelFont, Brushes.Black, startX - 80, startY + chartHeight / 2, new StringFormat() { FormatFlags = StringFormatFlags.DirectionVertical });

            int legendX = startX + chartWidth + 60;
            int legendY = startY;
            Font legendTitleFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font legendFont = new Font("Segoe UI", 9, FontStyle.Regular);

            g.DrawString("Legend:", legendTitleFont, Brushes.Black, legendX, legendY);

            for (int i = 0; i < rentalCounts.Count; i++)
            {
                var item = rentalCounts[i];
                int colorBoxY = legendY + 25 + (i * 22);
                g.FillRectangle(Brushes.SteelBlue, legendX, colorBoxY, 14, 14);
                g.DrawRectangle(Pens.Black, legendX, colorBoxY, 14, 14);
                g.DrawString($"{item.Movie.MovieTitle} - {item.Count} time(s)", legendFont, Brushes.Black, legendX + 20, colorBoxY + 2);
            }
        }
    }
}