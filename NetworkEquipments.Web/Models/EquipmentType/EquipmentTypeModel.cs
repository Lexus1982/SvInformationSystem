using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkEquipments.Web.Models.EquipmentType
{
    public class EquipmentTypeModel
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Не указан тип оборудования")]
        [Remote("ValidateEquipmentTypeName", "EquipmentType", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Display(Name = "Позиция")]
        [Required(ErrorMessage = "Не указана позиция в списке")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Значение поля {0} должно находиться в диапазоне от {1} до {2}")]
        public int Position { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }

    }
}