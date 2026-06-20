using System.Collections.Generic;

namespace health
{
    public static class FoodCatalogService
    {
        public static List<FoodItem> GetAllFoods()
        {
            List<FoodItem> foods = new List<FoodItem>();

            Add(foods, "Bánh mì trứng ốp la", "Sáng", "Bánh mì", "Gain", 520, "Giàu năng lượng, dễ ăn buổi sáng");
            Add(foods, "Bánh mì thịt nướng", "Sáng", "Bánh mì", "Gain", 610, "Nhiều calo, phù hợp tăng cân");
            Add(foods, "Bánh mì nguyên cám + trứng luộc", "Sáng", "Bánh mì", "Lose", 380, "No lâu, ít dầu mỡ");
            Add(foods, "Yến mạch + sữa chua không đường", "Sáng", "Yến mạch", "Lose", 320, "Ít calo, nhiều chất xơ");
            Add(foods, "Yến mạch + chuối + sữa", "Sáng", "Yến mạch", "Maintain", 430, "Cân bằng năng lượng");
            Add(foods, "Phở bò nhỏ", "Sáng", "Phở", "Maintain", 480, "Bữa sáng phổ biến, vừa năng lượng");
            Add(foods, "Phở gà", "Sáng", "Phở", "Maintain", 450, "Dễ ăn, vừa calo");
            Add(foods, "Bún bò Huế tô nhỏ", "Sáng", "Bún", "Gain", 620, "Nhiều năng lượng");
            Add(foods, "Hủ tiếu gà", "Sáng", "Hủ tiếu", "Maintain", 500, "Bữa sáng đủ năng lượng");
            Add(foods, "Cháo thịt bằm", "Sáng", "Cháo", "Lose", 330, "Nhẹ bụng, dễ tiêu");
            Add(foods, "Cháo gà + trứng", "Sáng", "Cháo", "Maintain", 420, "Đạm vừa phải");
            Add(foods, "Xôi gà", "Sáng", "Xôi", "Gain", 650, "Nhiều năng lượng");
            Add(foods, "Xôi đậu xanh", "Sáng", "Xôi", "Gain", 580, "Giàu tinh bột");
            Add(foods, "Bún riêu", "Sáng", "Bún", "Maintain", 470, "Calo trung bình");
            Add(foods, "Miến gà", "Sáng", "Miến", "Lose", 390, "Nhẹ hơn bún/phở");
            Add(foods, "Sữa tươi + chuối + bánh mì", "Sáng", "Combo", "Gain", 560, "Phù hợp tăng cân");
            Add(foods, "Trứng luộc + khoai lang", "Sáng", "Healthy", "Lose", 350, "Ít calo, nhiều chất xơ");
            Add(foods, "Bánh cuốn", "Sáng", "Bánh", "Maintain", 430, "Calo vừa phải");
            Add(foods, "Bánh bao trứng cút", "Sáng", "Bánh", "Gain", 520, "Nhiều năng lượng");
            Add(foods, "Sữa chua Hy Lạp + granola", "Sáng", "Healthy", "Maintain", 410, "Đạm và carb vừa phải");
            Add(foods, "Cơm gạo lứt + ức gà + rau luộc", "Trưa", "Cơm", "Lose", 620, "Kiểm soát calo, giàu đạm");
            Add(foods, "Cơm cá hấp + canh rau", "Trưa", "Cơm", "Lose", 590, "Ít dầu mỡ");
            Add(foods, "Cơm cá kho + rau luộc", "Trưa", "Cơm", "Maintain", 720, "Bữa chính cân bằng");
            Add(foods, "Cơm thịt bò xào rau", "Trưa", "Cơm", "Maintain", 780, "Giàu đạm và sắt");
            Add(foods, "Cơm bò lúc lắc", "Trưa", "Cơm", "Gain", 920, "Nhiều năng lượng");
            Add(foods, "Cơm gà xối mỡ", "Trưa", "Cơm", "Gain", 980, "Calo cao, dùng khi cần tăng cân");
            Add(foods, "Cơm gà luộc + rau", "Trưa", "Cơm", "Maintain", 710, "Calo vừa");
            Add(foods, "Cơm sườn nướng", "Trưa", "Cơm", "Gain", 900, "Nhiều calo");
            Add(foods, "Cơm trứng + thịt nạc", "Trưa", "Cơm", "Maintain", 760, "Dễ chuẩn bị");
            Add(foods, "Cơm cá hồi + salad", "Trưa", "Cơm", "Maintain", 820, "Chất béo tốt");
            Add(foods, "Bún thịt nướng", "Trưa", "Bún", "Maintain", 780, "Đổi món");
            Add(foods, "Bún chả", "Trưa", "Bún", "Maintain", 760, "Calo trung bình cao");
            Add(foods, "Mì Ý sốt bò bằm", "Trưa", "Mì", "Gain", 900, "Nhiều năng lượng");
            Add(foods, "Nui xào bò", "Trưa", "Nui", "Gain", 850, "Tăng calo và đạm");
            Add(foods, "Salad ức gà", "Trưa", "Salad", "Lose", 480, "Ít calo, nhiều protein");
            Add(foods, "Cơm đậu hũ sốt cà + rau", "Trưa", "Cơm", "Lose", 560, "Nhẹ và ít dầu");
            Add(foods, "Cơm tôm rim + canh rau", "Trưa", "Cơm", "Maintain", 700, "Bữa cân bằng");
            Add(foods, "Cơm thịt kho trứng", "Trưa", "Cơm", "Gain", 880, "Nhiều calo");
            Add(foods, "Cơm rang dương châu", "Trưa", "Cơm", "Gain", 920, "Calo cao");
            Add(foods, "Cơm gà sốt nấm", "Trưa", "Cơm", "Maintain", 780, "Đủ đạm và carb");
            Add(foods, "Cá hấp + salad rau + khoai lang", "Tối", "Healthy", "Lose", 520, "Nhẹ bụng buổi tối");
            Add(foods, "Đậu hũ sốt cà + rau luộc", "Tối", "Healthy", "Lose", 430, "Ít calo");
            Add(foods, "Ức gà áp chảo + khoai lang", "Tối", "Healthy", "Lose", 560, "Giàu đạm");
            Add(foods, "Miến gà", "Tối", "Miến", "Lose", 420, "Nhẹ hơn cơm");
            Add(foods, "Bún cá", "Tối", "Bún", "Maintain", 580, "Calo vừa phải");
            Add(foods, "Cơm vừa + cá thu + rau", "Tối", "Cơm", "Maintain", 680, "Bữa tối cân bằng");
            Add(foods, "Cơm tôm + rau xào", "Tối", "Cơm", "Maintain", 650, "Đạm vừa");
            Add(foods, "Bún thịt nạc + rau", "Tối", "Bún", "Maintain", 620, "Dễ ăn");
            Add(foods, "Mì Ý bò bằm + salad", "Tối", "Mì", "Gain", 820, "Nhiều năng lượng");
            Add(foods, "Cơm bò xào + trứng", "Tối", "Cơm", "Gain", 850, "Tăng cân");
            Add(foods, "Cơm cá hồi + khoai", "Tối", "Cơm", "Gain", 780, "Chất béo tốt");
            Add(foods, "Cơm gà + canh rau", "Tối", "Cơm", "Maintain", 700, "Bữa tối phổ biến");
            Add(foods, "Súp gà rau củ", "Tối", "Súp", "Lose", 360, "Rất nhẹ");
            Add(foods, "Lẩu rau + thịt nạc ít bún", "Tối", "Lẩu", "Lose", 520, "Ít tinh bột");
            Add(foods, "Cơm đậu hũ + nấm", "Tối", "Cơm", "Maintain", 600, "Thanh đạm");
            Add(foods, "Khoai lang + trứng + sữa", "Tối", "Combo", "Gain", 720, "Bổ sung năng lượng");
            Add(foods, "Bánh mì bò + sữa", "Tối", "Bánh mì", "Gain", 780, "Dễ ăn khi tăng cân");
            Add(foods, "Cơm gạo lứt + cá basa", "Tối", "Cơm", "Lose", 540, "Kiểm soát calo");
            Add(foods, "Bún gạo lứt + ức gà", "Tối", "Bún", "Lose", 500, "Ít calo");
            Add(foods, "Cháo cá", "Tối", "Cháo", "Lose", 390, "Nhẹ bụng");
            Add(foods, "Táo", "Phụ", "Trái cây", "Lose", 95, "Bữa phụ ít calo");
            Add(foods, "Chuối", "Phụ", "Trái cây", "Maintain", 110, "Bổ sung năng lượng nhẹ");
            Add(foods, "Sữa chua không đường", "Phụ", "Sữa", "Lose", 90, "Ít calo");
            Add(foods, "Sữa chua Hy Lạp", "Phụ", "Sữa", "Maintain", 150, "Giàu protein");
            Add(foods, "Hạt hạnh nhân 20g", "Phụ", "Hạt", "Maintain", 130, "Chất béo tốt");
            Add(foods, "Sinh tố chuối sữa", "Phụ", "Sinh tố", "Gain", 320, "Tăng cân");
            Add(foods, "Sinh tố bơ", "Phụ", "Sinh tố", "Gain", 380, "Calo cao");
            Add(foods, "Khoai lang nhỏ", "Phụ", "Khoai", "Lose", 130, "No lâu");
            Add(foods, "Bánh protein", "Phụ", "Snack", "Maintain", 220, "Bổ sung protein");
            Add(foods, "Sữa tươi", "Phụ", "Sữa", "Gain", 180, "Dễ bổ sung calo");
            Add(foods, "Phô mai lát", "Phụ", "Sữa", "Gain", 120, "Calo cao hơn");
            Add(foods, "Trái cây hỗn hợp", "Phụ", "Trái cây", "Maintain", 160, "Vitamin");
            Add(foods, "Bánh mì nhỏ + sữa", "Phụ", "Bánh mì", "Gain", 350, "Bữa phụ tăng cân");
            Add(foods, "Granola + sữa chua", "Phụ", "Snack", "Gain", 300, "Năng lượng cao");
            Add(foods, "Đậu nành rang", "Phụ", "Đậu", "Maintain", 200, "Giàu đạm thực vật");
            Add(foods, "Trứng luộc", "Phụ", "Trứng", "Lose", 75, "Đạm ít calo");
            Add(foods, "Sữa hạt không đường", "Phụ", "Sữa", "Lose", 100, "Ít calo");
            Add(foods, "Bánh yến mạch", "Phụ", "Snack", "Maintain", 210, "No lâu");
            Add(foods, "Bơ đậu phộng + bánh mì", "Phụ", "Snack", "Gain", 360, "Tăng calo");
            Add(foods, "Nho / dưa hấu", "Phụ", "Trái cây", "Lose", 100, "Ít calo");
            Add(foods, "Sữa cacao", "Phụ", "Sữa", "Gain", 250, "Dễ uống");
            return foods;
        }

        private static void Add(List<FoodItem> foods, string name, string mealType, string category, string goalTag, double calories, string description)
        {
            foods.Add(new FoodItem
            {
                Name = name,
                MealType = mealType,
                Category = category,
                GoalTag = goalTag,
                Calories = calories,
                Description = description
            });
        }
    }
}
