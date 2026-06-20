using System.Drawing;
using System.Windows.Forms;

namespace health
{
    public class ActivityPage : BasePage
    {
        private readonly HealthRecordService recordService;

        public ActivityPage(HealthDataContext data, HealthRecordService recordService) : base(data)
        {
            this.recordService = recordService;
            BuildUI();
        }

        private void BuildUI()
        {
            AddHeader("Hoạt động", "Ghi nhận vận động riêng, tính calo tiêu hao và theo dõi chuỗi vận động liên tiếp.");
            Root.AutoScrollMinSize = new Size(0, 1200);

            TableLayoutPanel cards = new TableLayoutPanel();
            cards.Width = PageWidth();
            cards.Height = 180;
            cards.ColumnCount = 4;
            cards.RowCount = 1;
            cards.Margin = new Padding(0, 0, 0, 18);
            for (int i = 0; i < 4; i++) cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            int streak = ActivityService.CalculateActivityStreak(Data.Today);
            cards.Controls.Add(CreateMetricCard("Chuỗi vận động", streak + " ngày", "Liên tiếp"), 0, 0);
            cards.Controls.Add(CreateMetricCard("Tổng km", Data.Today.TotalActivityDistanceKm.ToString("0.##") + " km", "Hôm nay"), 1, 0);
            cards.Controls.Add(CreateMetricCard("Calo tiêu hao", Data.Today.TotalCaloriesBurned.ToString("0") + " kcal", "Ước tính"), 2, 0);
            cards.Controls.Add(CreateMetricCard("Số hoạt động", Data.Today.ActivityRecords.Count.ToString(), "Đã ghi nhận"), 3, 0);
            Root.Controls.Add(cards);

            GroupBox inputBox = CreateSection("Ghi nhận hoạt động", 300);
            TableLayoutPanel form = CreateFormTable(4);
            TextBox activityNameInput = new TextBox { Width = 280, Text = "Đi bộ" };
            NumericUpDown distanceInput = CreateNumber(0, 100, 0, 2);
            NumericUpDown durationInput = CreateNumber(0, 500, 0, 0);
            Button submitButton = CreateActionButton("Ghi nhận hoạt động");
            submitButton.Width = 210;

            AddInputRow(form, 0, "Tên hoạt động", activityNameInput);
            AddInputRow(form, 1, "Số km", distanceInput);
            AddInputRow(form, 2, "Thời gian (phút)", durationInput);
            form.Controls.Add(submitButton, 1, 3);
            inputBox.Controls.Add(form);
            Root.Controls.Add(inputBox);

            GroupBox historyBox = CreateSection("Lịch sử vận động", 430);
            DataGridView grid = CreateActivityGrid();
            historyBox.Controls.Add(grid);
            Root.Controls.Add(historyBox);
            RefreshActivityGrid(grid);

            submitButton.Click += delegate
            {
                if (recordService.AddActivity(activityNameInput.Text.Trim(), (double)distanceInput.Value, (double)durationInput.Value))
                {
                    MessageBox.Show("Đã ghi nhận hoạt động.");
                    distanceInput.Value = 0;
                    durationInput.Value = 0;
                    RefreshActivityGrid(grid);
                    Root.Controls.Clear();
                    BuildUI();
                }
                else
                {
                    MessageBox.Show("Số km phải lớn hơn 0.");
                }
            };
        }

        private DataGridView CreateActivityGrid()
        {
            DataGridView grid = CreateGrid();
            grid.Columns.Add("Time", "Thời gian");
            grid.Columns.Add("Name", "Hoạt động");
            grid.Columns.Add("Distance", "Km");
            grid.Columns.Add("Duration", "Phút");
            grid.Columns.Add("Calories", "Calo tiêu hao");
            return grid;
        }

        private void RefreshActivityGrid(DataGridView grid)
        {
            grid.Rows.Clear();
            foreach (ActivityRecord item in Data.Today.ActivityRecords)
                grid.Rows.Add(item.Time.ToString("HH:mm:ss"), item.Name, item.DistanceKm.ToString("0.##"), item.DurationMinutes.ToString("0"), item.CaloriesBurned.ToString("0") + " kcal");
        }
    }
}
