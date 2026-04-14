namespace DataAccess.DTOs.User
{
    public class GetUserInputDto
    {
        /// <summary>
        /// 識別編碼
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 手機
        /// </summary>
        public string Tel { get; set; }
    }
}
