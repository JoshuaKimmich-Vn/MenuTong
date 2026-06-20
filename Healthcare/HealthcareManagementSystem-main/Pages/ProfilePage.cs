using System.Drawing;
using System.Windows.Forms;

namespace health
{
    public class ProfilePage : BasePage
    {
        public ProfilePage(HealthDataContext data) : base(data)
        {
            BuildUI();
        }

        private void BuildUI()
        {
            AddHeader("Hồ sơ sức khỏe", "Cập nhật thông tin cá nhân, mục tiêu và mức độ vận động.");

            GroupBox profileBox = CreateSection("Thông tin cá nhân", 430);
            TableLayoutPanel form = CreateFormTable(8);

            TextBox nameInput = new TextBox { Width = 280, Text = Data.Profile.FullName };
            NumericUpDown ageInput = CreateNumber(10, 100, Data.Profile.Age, 0);

            ComboBox genderInput = new ComboBox();
            genderInput.DropDownStyle = ComboBoxStyle.DropDownList;
            genderInput.Width = 280;
            genderInput.Items.AddRange(new object[] { "Nam", "Nữ" });
            genderInput.SelectedIndex = Data.Profile.Gender == Gender.Male ? 0 : 1;

            NumericUpDown heightInput = CreateNumber(100, 230, (decimal)Data.Profile.HeightCm, 1);
            NumericUpDown weightInput = CreateNumber(30, 250, (decimal)Data.Profile.WeightKg, 1);

            ComboBox goalInput = new ComboBox();
            goalInput.DropDownStyle = ComboBoxStyle.DropDownList;
            goalInput.Width = 280;
            goalInput.Items.AddRange(new object[] { "Giảm cân", "Duy trì cân nặng", "Tăng cân" });
            goalInput.SelectedIndex = GoalToIndex(Data.Profile.Goal);

            ComboBox activityInput = new ComboBox();
            activityInput.DropDownStyle = ComboBoxStyle.DropDownList;
            activityInput.Width = 280;
            activityInput.Items.AddRange(new object[]
            {
                "Ít vận động - 1.2",
                "Vận động nhẹ - 1.375",
                "Vận động vừa - 1.55",
                "Vận động nhiều - 1.725"
            });
            activityInput.SelectedIndex = ActivityFactorToIndex(Data.Profile.ActivityFactor);

            Button saveButton = CreateActionButton("Lưu hồ sơ");
            saveButton.Width = 180;
            saveButton.Height = 40;

            AddInputRow(form, 0, "Họ tên", nameInput);
            AddInputRow(form, 1, "Tuổi", ageInput);
            AddInputRow(form, 2, "Giới tính", genderInput);
            AddInputRow(form, 3, "Chiều cao (cm)", heightInput);
            AddInputRow(form, 4, "Cân nặng (kg)", weightInput);
            AddInputRow(form, 5, "Mục tiêu", goalInput);
            AddInputRow(form, 6, "Mức vận động", activityInput);
            form.Controls.Add(saveButton, 1, 7);

            saveButton.Click += delegate
            {
                Data.Profile.FullName = nameInput.Text;
                Data.Profile.Age = (int)ageInput.Value;
                Data.Profile.Gender = genderInput.SelectedIndex == 0 ? Gender.Male : Gender.Female;
                Data.Profile.HeightCm = (double)heightInput.Value;
                Data.Profile.WeightKg = (double)weightInput.Value;
                Data.Profile.Goal = IndexToGoal(goalInput.SelectedIndex);
                Data.Profile.ActivityFactor = IndexToActivityFactor(activityInput.SelectedIndex);

                MessageBox.Show("Đã lưu hồ sơ sức khỏe.", "Thông báo");
                Root.Controls.Clear();
                BuildUI();
            };

            profileBox.Controls.Add(form);
            Root.Controls.Add(profileBox);
        }

        private int GoalToIndex(HealthGoal goal)
        {
            if (goal == HealthGoal.LoseWeight) return 0;
            if (goal == HealthGoal.MaintainWeight) return 1;
            return 2;
        }

        private HealthGoal IndexToGoal(int index)
        {
            if (index == 0) return HealthGoal.LoseWeight;
            if (index == 2) return HealthGoal.GainWeight;
            return HealthGoal.MaintainWeight;
        }

        private int ActivityFactorToIndex(double factor)
        {
            if (factor <= 1.2) return 0;
            if (factor <= 1.375) return 1;
            if (factor <= 1.55) return 2;
            return 3;
        }

        private double IndexToActivityFactor(int index)
        {
            if (index == 0) return 1.2;
            if (index == 1) return 1.375;
            if (index == 2) return 1.55;
            return 1.725;
        }
    }
}
