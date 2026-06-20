using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace health
{
    public static class FoodCalorieEstimatorService
    {
        public static double EstimateCalories(string foodName, double servings, out string note)
        {
            string normalized = Normalize(foodName);
            double parsedServing = ParseServingFromText(normalized);

            if (servings <= 0) servings = 1;
            if (parsedServing > 1 && Math.Abs(servings - 1) < 0.01)
                servings = parsedServing;

            if (string.IsNullOrWhiteSpace(normalized))
            {
                note = "Chưa nhập tên món, dùng mặc định 400 kcal/phần.";
                return 400 * servings;
            }

            double direct;
            if (TryEstimateCommonFood(normalized, out direct, out note))
                return direct * servings;

            List<FoodItem> foods = FoodCatalogService.GetAllFoods();
            FoodItem exact = foods.FirstOrDefault(x => Normalize(x.Name) == normalized);
            if (exact != null)
            {
                note = "Khớp chính xác với kho dữ liệu món ăn.";
                return exact.Calories * servings;
            }

            if (normalized.Length > 6)
            {
                FoodItem contains = foods.FirstOrDefault(x => normalized.Contains(Normalize(x.Name)) || Normalize(x.Name).Contains(normalized));
                if (contains != null)
                {
                    note = "Ước tính theo món gần giống trong kho dữ liệu: " + contains.Name;
                    return contains.Calories * servings;
                }
            }

            double calories = EstimateByKeyword(normalized, out string keywordNote);
            note = keywordNote;
            return calories * servings;
        }

        private static bool TryEstimateCommonFood(string text, out double calories, out string note)
        {
            calories = 0;
            note = "Ước tính theo bảng từ khóa món phổ biến.";

            if (text.Contains("tra sua") || text.Contains("trà sữa")) { calories = 500; return true; }
            if (text.Contains("pho bo") || text.Contains("phở bò")) { calories = 550; return true; }
            if (text.Contains("pho ga") || text.Contains("phở gà")) { calories = 480; return true; }
            if (text.Contains("bun bo") || text.Contains("bún bò")) { calories = 600; return true; }
            if (text.Contains("bun thit nuong") || text.Contains("bún thịt nướng")) { calories = 700; return true; }
            if (text.Contains("banh mi trung") || text.Contains("bánh mì trứng")) { calories = 430; return true; }
            if (text.Contains("banh mi thit") || text.Contains("bánh mì thịt")) { calories = 520; return true; }
            if (text.Contains("com ga") || text.Contains("cơm gà")) { calories = 720; return true; }
            if (text.Contains("com bo") || text.Contains("cơm bò")) { calories = 820; return true; }
            if (text.Contains("com suon") || text.Contains("cơm sườn")) { calories = 850; return true; }
            if (text.Contains("trung luoc") || text.Contains("trứng luộc")) { calories = 75; return true; }
            if (text == "rau" || text.Contains("rau luoc") || text.Contains("rau luộc")) { calories = 50; return true; }
            if (text.Contains("salad")) { calories = 180; return true; }
            if (text.Contains("chuoi") || text.Contains("chuối")) { calories = 110; return true; }
            if (text.Contains("tao") || text.Contains("táo")) { calories = 95; return true; }
            if (text.Contains("sua chua") || text.Contains("sữa chua")) { calories = 100; return true; }
            if (text.Contains("sua tuoi") || text.Contains("sữa tươi")) { calories = 180; return true; }
            if (text.Contains("khoai lang")) { calories = 130; return true; }

            return false;
        }

        private static double EstimateByKeyword(string text, out string note)
        {
            double calories = 0;
            note = "Ước tính theo thành phần món ăn, có thể sai lệch so với thực tế.";

            if (text.Contains("com") || text.Contains("cơm")) calories += 300;
            if (text.Contains("pho") || text.Contains("phở")) calories += 420;
            if (text.Contains("bun") || text.Contains("bún")) calories += 380;
            if (text.Contains("mi") || text.Contains("mì")) calories += 420;
            if (text.Contains("banh mi") || text.Contains("bánh mì")) calories += 280;
            if (text.Contains("xoi") || text.Contains("xôi")) calories += 500;
            if (text.Contains("chao") || text.Contains("cháo")) calories += 250;
            if (text.Contains("rau")) calories += 50;
            if (text.Contains("salad")) calories += 180;
            if (text.Contains("khoai")) calories += 140;

            if (text.Contains("ga") || text.Contains("gà")) calories += 220;
            if (text.Contains("bo") || text.Contains("bò")) calories += 260;
            if (text.Contains("heo") || text.Contains("thit") || text.Contains("thịt")) calories += 260;
            if (text.Contains("ca") || text.Contains("cá")) calories += 200;
            if (text.Contains("tom") || text.Contains("tôm")) calories += 150;
            if (text.Contains("trung") || text.Contains("trứng")) calories += 75;
            if (text.Contains("dau hu") || text.Contains("đậu hũ")) calories += 150;

            if (text.Contains("chien") || text.Contains("chiên") || text.Contains("xao") || text.Contains("xào")) calories += 160;
            if (text.Contains("hap") || text.Contains("hấp") || text.Contains("luoc") || text.Contains("luộc")) calories -= 50;
            if (text.Contains("sua") || text.Contains("sữa")) calories += 150;
            if (text.Contains("chuoi") || text.Contains("chuối")) calories += 110;
            if (text.Contains("bo dau phong") || text.Contains("bơ đậu phộng")) calories += 180;

            if (calories <= 0) calories = 350;
            if (calories < 50) calories = 50;
            return calories;
        }

        private static double ParseServingFromText(string text)
        {
            Match match = Regex.Match(text, @"^\s*(\d+)");
            if (match.Success)
            {
                double value;
                if (double.TryParse(match.Groups[1].Value, out value))
                    return value;
            }
            return 1;
        }

        private static string Normalize(string value)
        {
            if (value == null) return "";
            return value.Trim().ToLower();
        }
    }
}
