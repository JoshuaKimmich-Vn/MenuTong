using System;
using System.Drawing;
using System.Windows.Forms;

namespace health
{
    public class WaterReminderPage : BasePage
    {
        private readonly HealthRecordService recordService;
        private readonly WaterReminderService waterReminderService;

        public WaterReminderPage(HealthDataContext data, HealthRecordService recordService, WaterReminderService waterReminderService) : base(data)
        {
            this.recordService = recordService;
            this.waterReminderService = waterReminderService;
            BuildUI();
        }

        private void BuildUI()
        {
            AddHeader("Nhắc uống nước", "Thiết lập thời điểm nhắc uống nước theo timeline sáng, trưa, chiều và tối.");
            Root.AutoScrollMinSize = new Size(0, 1200);

            GroupBox timelineBox = CreateSection("Thanh thời gian trong ngày", 220);
            timelineBox.Controls.Add(CreateWaterTimelinePanel());
            Root.Controls.Add(timelineBox);

            GroupBox settingBox = CreateSection("Ghim thời gian nhắc uống nước", 300);
            TableLayoutPanel form = CreateFormTable(5);

            CheckBox enableCheckBox = new CheckBox();
            enableCheckBox.Text = "Bật nhắc uống nước";
            enableCheckBox.Checked = Data.WaterReminder.Enabled;
            enableCheckBox.Font = new Font("Segoe UI", 10);
            enableCheckBox.Width = 280;

            DateTimePicker timePicker = new DateTimePicker();
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;
            timePicker.Width = 280;
            timePicker.Value = DateTime.Today.Add(new TimeSpan(8, 0, 0));

            NumericUpDown amountInput = CreateNumber(50, 2000, 250, 0);
            Button addSlotButton = CreateActionButton("Ghim thời gian");
            Button autoButton = CreateActionButton("Tự động chọn");
            Button clearButton = CreateActionButton("Xóa lịch");

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.Width = 650;
            buttonPanel.Height = 45;
            buttonPanel.Controls.Add(addSlotButton);
            buttonPanel.Controls.Add(autoButton);
            buttonPanel.Controls.Add(clearButton);

            AddInputRow(form, 0, "Trạng thái", enableCheckBox);
            AddInputRow(form, 1, "Thời điểm nhắc", timePicker);
            AddInputRow(form, 2, "Lượng nước (ml)", amountInput);
            form.Controls.Add(buttonPanel, 1, 3);

            settingBox.Controls.Add(form);
            Root.Controls.Add(settingBox);

            GroupBox reminderTableBox = CreateSection("Lịch nhắc uống nước đã ghim", 330);
            DataGridView reminderGrid = CreateReminderGrid();
            reminderTableBox.Controls.Add(reminderGrid);
            Root.Controls.Add(reminderTableBox);
            RefreshReminderGrid(reminderGrid);

            GroupBox quickWaterBox = CreateSection("Ghi nhận nhanh lượng nước đã uống", 170);
            TableLayoutPanel quickForm = CreateFormTable(2);
            NumericUpDown quickWaterInput = CreateNumber(0, 2000, 250, 0);
            Button addWaterButton = CreateActionButton("Ghi nhận nước");
            AddInputRow(quickForm, 0, "Nước đã uống", quickWaterInput);
            quickForm.Controls.Add(addWaterButton, 1, 1);
            quickWaterBox.Controls.Add(quickForm);
            Root.Controls.Add(quickWaterBox);

            addSlotButton.Click += delegate
            {
                Data.WaterReminder.Enabled = enableCheckBox.Checked;
                waterReminderService.AddSlot(timePicker.Value.TimeOfDay, (double)amountInput.Value);
                RefreshReminderGrid(reminderGrid);
            };

            autoButton.Click += delegate
            {
                Data.WaterReminder.Enabled = true;
                enableCheckBox.Checked = true;
                waterReminderService.AutoGenerateSlots();
                RefreshReminderGrid(reminderGrid);
                MessageBox.Show("Đã tự động tạo lịch nhắc uống nước theo mục tiêu nước trong ngày.");
            };

            clearButton.Click += delegate
            {
                waterReminderService.ClearSlots();
                RefreshReminderGrid(reminderGrid);
            };

            addWaterButton.Click += delegate
            {
                if (recordService.AddWater((double)quickWaterInput.Value, "Ghi nhận từ trang nhắc uống nước"))
                    MessageBox.Show("Đã ghi nhận " + quickWaterInput.Value.ToString("0") + " ml nước.");
            };
        }

        private Panel CreateWaterTimelinePanel()
        {
            Panel root = new Panel();
            root.Dock = DockStyle.Fill;
            root.BackColor = Color.White;
            root.Padding = new Padding(20);

            TableLayoutPanel table = new TableLayoutPanel();
            table.Dock = DockStyle.Fill;
            table.ColumnCount = 4;
            table.RowCount = 1;
            for (int i = 0; i < 4; i++) table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            table.Controls.Add(CreateTimeBlock("Sáng\n06:00 - 11:00", Color.FromArgb(191, 219, 254)), 0, 0);
            table.Controls.Add(CreateTimeBlock("Trưa\n11:00 - 13:00", Color.FromArgb(147, 197, 253)), 1, 0);
            table.Controls.Add(CreateTimeBlock("Chiều\n13:00 - 18:00", Color.FromArgb(96, 165, 250)), 2, 0);
            table.Controls.Add(CreateTimeBlock("Tối\n18:00 - 22:00", Color.FromArgb(59, 130, 246)), 3, 0);

            root.Controls.Add(table);
            return root;
        }

        private Panel CreateTimeBlock(string text, Color color)
        {
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            panel.BackColor = color;
            panel.Margin = new Padding(5);
            panel.Padding = new Padding(5);

            Label label = new Label();
            label.Text = text;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(15, 23, 42);
            label.AutoSize = false;

            panel.Controls.Add(label);
            return panel;
        }

        private DataGridView CreateReminderGrid()
        {
            DataGridView grid = CreateGrid();
            grid.Columns.Add("Time", "Thời điểm");
            grid.Columns.Add("Session", "Buổi");
            grid.Columns.Add("Amount", "Lượng nước");
            grid.Columns.Add("Status", "Trạng thái");
            return grid;
        }

        private void RefreshReminderGrid(DataGridView grid)
        {
            grid.Rows.Clear();
            foreach (WaterReminderSlot slot in Data.WaterReminder.Slots)
                grid.Rows.Add(slot.Time.ToString(@"hh\:mm"), GetSessionName(slot.Time), slot.AmountMl.ToString("0") + " ml", Data.WaterReminder.Enabled ? "Đang bật" : "Đang tắt");
        }

        private string GetSessionName(TimeSpan time)
        {
            if (time >= new TimeSpan(6, 0, 0) && time < new TimeSpan(11, 0, 0)) return "Sáng";
            if (time >= new TimeSpan(11, 0, 0) && time < new TimeSpan(13, 0, 0)) return "Trưa";
            if (time >= new TimeSpan(13, 0, 0) && time < new TimeSpan(18, 0, 0)) return "Chiều";
            return "Tối";
        }
    }
}
