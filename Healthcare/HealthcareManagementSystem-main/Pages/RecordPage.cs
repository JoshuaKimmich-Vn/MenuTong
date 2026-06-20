using System.Drawing;
using System.Windows.Forms;

namespace health
{
    public class RecordPage : BasePage
    {
        private readonly HealthRecordService recordService;

        public RecordPage(HealthDataContext data, HealthRecordService recordService) : base(data)
        {
            this.recordService = recordService;
            BuildUI();
        }

        private void BuildUI()
        {
            AddHeader("Ghi nhận dữ liệu", "Ghi nhận dữ liệu sức khỏe cơ bản. Hoạt động và thực đơn đã được tách thành trang riêng.");
            Root.AutoScrollMinSize = new Size(0, 1200);

            GroupBox inputBox = CreateSection("Nhập dữ liệu sức khỏe", 390);
            TableLayoutPanel form = CreateFormTable(7);

            NumericUpDown waterInput = CreateNumber(0, 5000, 0, 0);
            NumericUpDown sleepHourInput = CreateNumber(0, 24, 0, 1);
            NumericUpDown weightInput = CreateNumber(0, 250, 0, 1);
            NumericUpDown heartRateInput = CreateNumber(0, 220, 0, 0);
            NumericUpDown temperatureInput = CreateNumber(0, 45, 0, 1);
            Button submitButton = CreateActionButton("Ghi nhận tất cả");
            submitButton.Width = 200;
            submitButton.Height = 40;

            AddInputRow(form, 0, "Lượng nước (ml)", waterInput);
            AddInputRow(form, 1, "Số giờ ngủ", sleepHourInput);
            AddInputRow(form, 2, "Cân nặng mới", weightInput);
            AddInputRow(form, 3, "Nhịp tim", heartRateInput);
            AddInputRow(form, 4, "Nhiệt độ cơ thể", temperatureInput);
            form.Controls.Add(submitButton, 1, 5);

            inputBox.Controls.Add(form);
            Root.Controls.Add(inputBox);

            GroupBox bmiBox = CreateSection("Đánh giá BMI và hướng cải thiện", 260);
            RichTextBox bmiText = new RichTextBox();
            bmiText.Dock = DockStyle.Fill;
            bmiText.ReadOnly = true;
            bmiText.BorderStyle = BorderStyle.None;
            bmiText.BackColor = Color.White;
            bmiText.Font = new Font("Segoe UI", 10);
            bmiText.Text = HealthPlanningService.BuildBmiImprovementAdvice(Data.Profile);
            bmiBox.Controls.Add(bmiText);
            Root.Controls.Add(bmiBox);

            GroupBox tableBox = CreateSection("Bảng thống kê dữ liệu đã ghi nhận", 390);
            DataGridView recordGrid = CreateRecordGrid();
            tableBox.Controls.Add(recordGrid);
            Root.Controls.Add(tableBox);
            RefreshRecordGrid(recordGrid);

            submitButton.Click += delegate
            {
                int addedCount = 0;
                if (recordService.AddWater((double)waterInput.Value, "Ghi nhận từ trang nhập dữ liệu")) addedCount++;
                if (recordService.AddSleep((double)sleepHourInput.Value)) addedCount++;
                if (recordService.AddBodyMetrics((double)weightInput.Value, (int)heartRateInput.Value, 0, 0, (double)temperatureInput.Value)) addedCount++;

                if (addedCount == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu nào được nhập. Hãy nhập ít nhất một trường có giá trị khác 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                RefreshRecordGrid(recordGrid);
                bmiText.Text = HealthPlanningService.BuildBmiImprovementAdvice(Data.Profile);
                waterInput.Value = 0;
                sleepHourInput.Value = 0;
                weightInput.Value = 0;
                heartRateInput.Value = 0;
                temperatureInput.Value = 0;
                MessageBox.Show("Đã ghi nhận " + addedCount + " nhóm dữ liệu.");
            };
        }
    }
}
