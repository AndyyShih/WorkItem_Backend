using System.Collections.Generic;

namespace DataAccess.DTOs.User
{
    public class GetUserOutputDto
    {
        /// <summary>
        /// 識別編碼
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 使用者帳號
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        public List<UserWorkItemStatusDto> UserWorkItemStatuses { get; set; }
    }
}
