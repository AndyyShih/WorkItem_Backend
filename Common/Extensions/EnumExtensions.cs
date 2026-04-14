using System.ComponentModel;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(Enum value)
        {
            // 獲取 enum 的 Type
            var type = value.GetType();
            // 根據 enum 的值取得對應的成員資訊
            var memberInfo = type.GetMember(value.ToString());

            if (memberInfo.Length > 0)
            {
                // 獲取 Description 屬性
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    // 回傳 Description 的值
                    return ((DescriptionAttribute)attributes[0]).Description;
                }
            }

            // 如果沒有定義 Description 屬性，則回傳 enum 值的字串形式
            return value.ToString();
        }
    }
}
