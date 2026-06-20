using System;

namespace health
{
    public static class DailyTipService
    {
        private static readonly string[] Tips = new string[]
        {
            "Hôm nay nên uống nước đều trong ngày thay vì uống dồn một lần.",
            "Đi bộ 20 phút có thể giúp cải thiện sức khỏe tim mạch.",
            "Không nên ăn quá nhiều đồ ngọt vào buổi tối.",
            "Ngủ đủ giúp cơ thể phục hồi và kiểm soát cân nặng tốt hơn.",
            "Bữa sáng giàu đạm giúp no lâu hơn.",
            "Khi giảm cân, không nên cắt calo quá mạnh trong thời gian ngắn.",
            "Khi tăng cân, nên tăng calo từ thực phẩm giàu dinh dưỡng thay vì đồ ngọt.",
            "Vận động nhẹ hằng ngày tốt hơn là chỉ tập nặng một ngày rồi nghỉ lâu.",
            "Rau xanh giúp tăng cảm giác no và bổ sung chất xơ.",
            "Theo dõi cân nặng mỗi tuần sẽ ổn định hơn theo dõi từng ngày."
        };

        public static string GetTodayTip()
        {
            int index = DateTime.Today.DayOfYear % Tips.Length;
            return Tips[index];
        }
    }
}
