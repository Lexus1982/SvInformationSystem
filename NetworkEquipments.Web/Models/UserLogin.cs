using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetworkEquipments.Web.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Не указан логин")]
        [StringLength(50, ErrorMessage = "Размер данных в поле {0} не должен превышать {1} символов")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(16, ErrorMessage = "Размер данных в поле {0} не должен превышать {1} символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}