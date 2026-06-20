using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace health
{
    public abstract class BasePage : UserControl
    {
        protected readonly HealthDataContext Data;
        protected readonly FlowLayoutPanel Root;

        protected BasePage(HealthDataContext data)
        {
            Data = data;
            Dock = DockStyle.Fill;
            BackColor = AppTheme.Background;

            Root = new FlowLayoutPanel();
            Root.Dock = DockStyle.Fill;
            Root.FlowDirection = FlowDirection.TopDown;
            Root.WrapContents = false;
            Root.AutoScroll = true;
            Root.AutoScrollMargin = new Size(0, 40);
            Root.Padding = new Padding(0, 0, 18, 0);
            Root.BackColor = AppTheme.Background;

            Controls.Add(Root);
            Resize += delegate { ResizePageChildren(); };
        }

        protected int PageWidth()
        {
            return Math.Max(850, ClientSize.Width - 40);
        }

        private void ResizePageChildren()
        {
            int width = PageWidth();
            foreach (Control control in Root.Controls)
            {
                if (control is Panel || control is GroupBox || control is TableLayoutPanel)
                    control.Width = width;
            }
        }

        protected void AddHeader(string title, string subtitle)
        {
            Panel header = new Panel();
            header.Width = PageWidth();
            header.Height = 80;
            header.BackColor = AppTheme.Background;
            header.Margin = new Padding(0, 0, 0, 15);

            Label titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            titleLabel.ForeColor = AppTheme.TextDark;
            titleLabel.Left = 0;
            titleLabel.Top = 0;
            titleLabel.Width = PageWidth();
            titleLabel.Height = 45;

            Label subtitleLabel = new Label();
            subtitleLabel.Text = subtitle;
            subtitleLabel.Font = new Font("Segoe UI", 10);
            subtitleLabel.ForeColor = AppTheme.TextGray;
            subtitleLabel.Left = 2;
            subtitleLabel.Top = 48;
            subtitleLabel.Width = PageWidth();
            subtitleLabel.Height = 25;

            header.Controls.Add(titleLabel);
            header.Controls.Add(subtitleLabel);
            Root.Controls.Add(header);
        }

        protected GroupBox CreateSection(string title, int height)
        {
            GroupBox group = new GroupBox();
            group.Text = title;
            group.Width = PageWidth();
            group.Height = height;
            group.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            group.BackColor = AppTheme.White;
            group.ForeColor = AppTheme.TextDark;
            group.Margin = new Padding(0, 0, 0, 18);
            group.Padding = new Padding(12);
            return group;
        }

        protected TableLayoutPanel CreateFormTable(int rows)
        {
            TableLayoutPanel table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.ColumnCount = 2;
            table.RowCount = rows;
            table.Padding = new Padding(10);
            table.BackColor = AppTheme.White;
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            for (int i = 0; i < rows; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));

            return table;
        }

        protected void AddInputRow(TableLayoutPanel table, int row, string labelText, Control input)
        {
            Label label = new Label();
            label.Text = labelText;
            label.Font = new Font("Segoe UI", 9);
            label.ForeColor = AppTheme.TextDark;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Dock = DockStyle.Fill;

            input.Margin = new Padding(3, 7, 3, 3);
            input.Anchor = AnchorStyles.Left;
            input.Width = 280;

            table.Controls.Add(label, 0, row);
            table.Controls.Add(input, 1, row);
        }

        protected NumericUpDown CreateNumber(decimal min, decimal max, decimal value, int decimalPlaces)
        {
            NumericUpDown number = new NumericUpDown();
            number.Minimum = min;
            number.Maximum = max;
            if (value < min) value = min;
            if (value > max) value = max;
            number.Value = value;
            number.DecimalPlaces = decimalPlaces;
            number.Width = 280;
            number.Increment = decimalPlaces == 0 ? 1 : 0.1m;
            return number;
        }

        protected Button CreateActionButton(string text)
        {
            Button button = new Button();
            button.Text = text;
            button.Width = 160;
            button.Height = 36;
            button.BackColor = AppTheme.Blue;
            button.ForeColor = Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            button.MouseEnter += delegate { button.BackColor = AppTheme.DarkBlue; };
            button.MouseLeave += delegate { button.BackColor = AppTheme.Blue; };
            return button;
        }

        protected Panel CreateMetricCard(string title, string value, string note)
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Margin = new Padding(0, 0, 15, 0);
            card.Padding = new Padding(18);
            card.Dock = DockStyle.Fill;

            Panel accent = new Panel();
            accent.BackColor = AppTheme.Blue;
            accent.Width = 6;
            accent.Dock = DockStyle.Left;

            Label titleLabel = new Label();
            titleLabel.Text = title;
            titleLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleLabel.ForeColor = AppTheme.TextGray;
            titleLabel.Left = 20;
            titleLabel.Top = 18;
            titleLabel.Width = 220;
            titleLabel.Height = 28;

            Label valueLabel = new Label();
            valueLabel.Text = value;
            valueLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            valueLabel.ForeColor = AppTheme.DarkBlue;
            valueLabel.Left = 20;
            valueLabel.Top = 55;
            valueLabel.Width = 260;
            valueLabel.Height = 45;

            Label noteLabel = new Label();
            noteLabel.Text = note;
            noteLabel.Font = new Font("Segoe UI", 9);
            noteLabel.ForeColor = AppTheme.TextGray;
            noteLabel.Left = 22;
            noteLabel.Top = 110;
            noteLabel.Width = 260;
            noteLabel.Height = 30;

            card.Controls.Add(noteLabel);
            card.Controls.Add(valueLabel);
            card.Controls.Add(titleLabel);
            card.Controls.Add(accent);
            return card;
        }

        protected DataGridView CreateGrid()
        {
            DataGridView grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.Font = new Font("Segoe UI", 10);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.DarkBlue;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.EnableHeadersVisualStyles = false;
            return grid;
        }

        protected DataGridView CreateMealSuggestionGrid()
        {
            DataGridView grid = CreateGrid();
            grid.Columns.Add("MealTime", "Buổi");
            grid.Columns.Add("FoodName", "Gợi ý món ăn");
            grid.Columns.Add("Calories", "Calo gợi ý");
            grid.Columns.Add("Note", "Ghi chú");
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            return grid;
        }

        protected void LoadMealSuggestions(DataGridView grid)
        {
            grid.Rows.Clear();
            List<MealSuggestion> suggestions = MealPlanService.GenerateSuggestionTable(Data.Profile);

            foreach (MealSuggestion item in suggestions)
                grid.Rows.Add(item.MealTime, item.FoodName, item.Calories.ToString("0") + " kcal", item.Note);
        }

        protected DataGridView CreateRecordGrid()
        {
            DataGridView grid = CreateGrid();
            grid.Columns.Add("Time", "Thời gian");
            grid.Columns.Add("Category", "Nhóm dữ liệu");
            grid.Columns.Add("Name", "Tên");
            grid.Columns.Add("Value", "Giá trị");
            grid.Columns.Add("Note", "Ghi chú");
            return grid;
        }

        protected void RefreshRecordGrid(DataGridView grid)
        {
            grid.Rows.Clear();
            foreach (HealthRecordEntry entry in Data.Today.ReportEntries)
                grid.Rows.Add(entry.Time.ToString("HH:mm:ss"), entry.Category, entry.Name, entry.Value, entry.Note);
        }

        protected string FormatGoal(HealthGoal goal)
        {
            if (goal == HealthGoal.LoseWeight) return "Giảm cân";
            if (goal == HealthGoal.GainWeight) return "Tăng cân";
            return "Duy trì cân nặng";
        }

        protected string GetBmiStatus(double bmi)
        {
            if (bmi < 18.5) return "Thiếu cân";
            if (bmi < 25) return "Bình thường";
            if (bmi < 30) return "Thừa cân";
            return "Béo phì";
        }
    }
}
