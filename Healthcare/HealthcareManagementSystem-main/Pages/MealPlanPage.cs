using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace health
{
    public class MealPlanPage : BasePage
    {
        private readonly HealthRecordService recordService;
        private int seedOffset = 0;
        private DataGridView planGrid;
        private List<MealSuggestion> currentPlan;

        public MealPlanPage(HealthDataContext data, HealthRecordService recordService) : base(data)
        {
            this.recordService = recordService;
            BuildUI();
        }

        private void BuildUI()
        {
            AddHeader("Thực đơn hôm nay", "Lên thực đơn theo calo mục tiêu. Người dùng cũng có thể tự nhập món ăn để hệ thống ước tính calo.");
            Root.AutoScrollMinSize = new Size(0, 1400);

            GroupBox infoBox = CreateSection("Mục tiêu dinh dưỡng", 130);
            RichTextBox infoText = new RichTextBox();
            infoText.Dock = DockStyle.Fill;
            infoText.ReadOnly = true;
            infoText.BorderStyle = BorderStyle.None;
            infoText.BackColor = Color.White;
            infoText.Font = new Font("Segoe UI", 11);
            infoText.Text = BuildNutritionInfoText();
            infoBox.Controls.Add(infoText);
            Root.Controls.Add(infoBox);

            GroupBox planBox = CreateSection("Thực đơn đề xuất trong ngày", 420);
            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;

            planGrid = CreateMealSuggestionGrid();
            planGrid.Dock = DockStyle.Top;
            planGrid.Height = 315;

            Button refreshButton = CreateActionButton("Tạo thực đơn mới");
            refreshButton.Width = 190;
            refreshButton.Top = 325;
            refreshButton.Left = 10;

            Button recordButton = CreateActionButton("Ghi nhận món đã ăn");
            recordButton.Width = 210;
            recordButton.Top = 325;
            recordButton.Left = 215;

            panel.Controls.Add(planGrid);
            panel.Controls.Add(refreshButton);
            panel.Controls.Add(recordButton);
            planBox.Controls.Add(panel);
            Root.Controls.Add(planBox);

            GroupBox manualBox = CreateSection("Tự nhập món ăn và ước tính calo", 360);
            TableLayoutPanel manualForm = CreateFormTable(6);

            TextBox manualFoodInput = new TextBox();
            manualFoodInput.Width = 350;
            manualFoodInput.Text = "";

            ComboBox mealTypeInput = new ComboBox();
            mealTypeInput.DropDownStyle = ComboBoxStyle.DropDownList;
            mealTypeInput.Width = 280;
            mealTypeInput.Items.AddRange(new object[] { "Bữa sáng", "Bữa trưa", "Bữa tối", "Bữa phụ" });
            mealTypeInput.SelectedIndex = 1;

            NumericUpDown servingInput = CreateNumber(1, 10, 1, 1);
            NumericUpDown estimatedCaloriesInput = CreateNumber(0, 5000, 0, 0);

            Button estimateButton = CreateActionButton("Ước tính calo");
            Button recordManualButton = CreateActionButton("Ghi nhận món này");
            recordManualButton.Width = 190;

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
            buttonPanel.Width = 460;
            buttonPanel.Height = 45;
            buttonPanel.Controls.Add(estimateButton);
            buttonPanel.Controls.Add(recordManualButton);

            AddInputRow(manualForm, 0, "Tên món ăn", manualFoodInput);
            AddInputRow(manualForm, 1, "Loại bữa", mealTypeInput);
            AddInputRow(manualForm, 2, "Số phần", servingInput);
            AddInputRow(manualForm, 3, "Calo ước tính", estimatedCaloriesInput);
            manualForm.Controls.Add(buttonPanel, 1, 4);

            manualBox.Controls.Add(manualForm);
            Root.Controls.Add(manualBox);

            LoadPlan();

            refreshButton.Click += delegate
            {
                seedOffset++;
                LoadPlan();
            };

            recordButton.Click += delegate
            {
                if (planGrid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Hãy chọn một món trong thực đơn.");
                    return;
                }

                int index = planGrid.SelectedRows[0].Index;
                if (index >= 0 && index < currentPlan.Count)
                {
                    recordService.AddSuggestedMeal(currentPlan[index]);
                    MessageBox.Show("Đã ghi nhận: " + currentPlan[index].FoodName);
                    infoText.Text = BuildNutritionInfoText();
                }
            };

            estimateButton.Click += delegate
            {
                string note;
                double calories = FoodCalorieEstimatorService.EstimateCalories(manualFoodInput.Text, (double)servingInput.Value, out note);
                if (calories > (double)estimatedCaloriesInput.Maximum)
                    calories = (double)estimatedCaloriesInput.Maximum;
                estimatedCaloriesInput.Value = (decimal)calories;
                MessageBox.Show(note, "Kết quả ước tính");
            };

            recordManualButton.Click += delegate
            {
                string note;
                double calories = (double)estimatedCaloriesInput.Value;
                if (calories <= 0)
                    calories = FoodCalorieEstimatorService.EstimateCalories(manualFoodInput.Text, (double)servingInput.Value, out note);
                else
                    note = "Người dùng xác nhận/điều chỉnh lượng calo.";

                if (recordService.AddManualMeal(mealTypeInput.Text, manualFoodInput.Text.Trim(), calories, note))
                {
                    MessageBox.Show("Đã ghi nhận món ăn: " + manualFoodInput.Text.Trim());
                    infoText.Text = BuildNutritionInfoText();
                    manualFoodInput.Text = "";
                    servingInput.Value = 1;
                    estimatedCaloriesInput.Value = 0;
                }
                else
                {
                    MessageBox.Show("Hãy nhập tên món ăn và calo hợp lệ.");
                }
            };
        }

        private string BuildNutritionInfoText()
        {
            return
                "Calo mục tiêu/ngày: " + CalorieCalculator.CalculateTargetCalories(Data.Profile).ToString("0") + " kcal\n" +
                "Calo đã nạp hôm nay: " + Data.Today.TotalCaloriesIn.ToString("0") + " kcal\n" +
                "Calo ròng: " + Data.Today.NetCalories.ToString("0") + " kcal";
        }

        private void LoadPlan()
        {
            currentPlan = MealPlanService.GenerateDailyMealPlan(Data.Profile, seedOffset);
            planGrid.Rows.Clear();

            foreach (MealSuggestion item in currentPlan)
                planGrid.Rows.Add(item.MealTime, item.FoodName, item.Calories.ToString("0") + " kcal", item.Note);
        }
    }
}
