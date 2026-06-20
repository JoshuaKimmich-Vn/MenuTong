using System.Drawing;
using System.Windows.Forms;

namespace health
{
    public class GoalPage : BasePage
    {
        public GoalPage(HealthDataContext data) : base(data)
        {
            BuildUI();
        }

        private void BuildUI()
        {
            AddHeader("Mục tiêu sức khỏe", "Thiết lập mục tiêu cá nhân: cân nặng, BMI, calo, bước chân, nước và giấc ngủ.");
            Root.AutoScrollMinSize = new Size(0, 1000);

            GroupBox formBox = CreateSection("Thiết lập mục tiêu", 500);
            TableLayoutPanel form = CreateFormTable(9);

            NumericUpDown startWeightInput = CreateNumber(20, 250, (decimal)Data.GoalPlan.StartWeightKg, 1);
            NumericUpDown targetWeightInput = CreateNumber(20, 250, (decimal)Data.GoalPlan.TargetWeightKg, 1);
            NumericUpDown targetBmiInput = CreateNumber(10, 40, (decimal)Data.GoalPlan.TargetBmi, 1);
            NumericUpDown calorieInput = CreateNumber(800, 5000, (decimal)Data.GoalPlan.DailyCalorieGoal, 0);
            NumericUpDown stepsInput = CreateNumber(0, 50000, Data.GoalPlan.DailyStepsGoal, 0);
            NumericUpDown waterInput = CreateNumber(500, 6000, (decimal)Data.GoalPlan.DailyWaterMlGoal, 0);
            NumericUpDown sleepInput = CreateNumber(0, 16, (decimal)Data.GoalPlan.DailySleepHoursGoal, 1);
            DateTimePicker targetDateInput = new DateTimePicker();
            targetDateInput.Width = 280;
            targetDateInput.Value = Data.GoalPlan.TargetDate;

            Button saveButton = CreateActionButton("Lưu mục tiêu");
            saveButton.Width = 180;

            AddInputRow(form, 0, "Cân nặng bắt đầu", startWeightInput);
            AddInputRow(form, 1, "Cân nặng mục tiêu", targetWeightInput);
            AddInputRow(form, 2, "BMI mong muốn", targetBmiInput);
            AddInputRow(form, 3, "Calo/ngày", calorieInput);
            AddInputRow(form, 4, "Bước/ngày", stepsInput);
            AddInputRow(form, 5, "Nước/ngày", waterInput);
            AddInputRow(form, 6, "Ngủ/ ngày", sleepInput);
            AddInputRow(form, 7, "Ngày mục tiêu", targetDateInput);
            form.Controls.Add(saveButton, 1, 8);

            formBox.Controls.Add(form);
            Root.Controls.Add(formBox);

            GroupBox progressBox = CreateSection("Tiến độ mục tiêu", 300);
            RichTextBox progressText = new RichTextBox();
            progressText.Dock = DockStyle.Fill;
            progressText.ReadOnly = true;
            progressText.BorderStyle = BorderStyle.None;
            progressText.BackColor = Color.White;
            progressText.Font = new Font("Segoe UI", 11);
            progressText.Text = HealthGoalService.BuildGoalProgressText(Data.Profile, Data.GoalPlan);
            progressBox.Controls.Add(progressText);
            Root.Controls.Add(progressBox);

            saveButton.Click += delegate
            {
                Data.GoalPlan.StartWeightKg = (double)startWeightInput.Value;
                Data.GoalPlan.TargetWeightKg = (double)targetWeightInput.Value;
                Data.GoalPlan.TargetBmi = (double)targetBmiInput.Value;
                Data.GoalPlan.DailyCalorieGoal = (double)calorieInput.Value;
                Data.GoalPlan.DailyStepsGoal = (int)stepsInput.Value;
                Data.GoalPlan.DailyWaterMlGoal = (double)waterInput.Value;
                Data.GoalPlan.DailySleepHoursGoal = (double)sleepInput.Value;
                Data.GoalPlan.TargetDate = targetDateInput.Value.Date;
                progressText.Text = HealthGoalService.BuildGoalProgressText(Data.Profile, Data.GoalPlan);
                MessageBox.Show("Đã lưu mục tiêu sức khỏe.");
            };
        }
    }
}
