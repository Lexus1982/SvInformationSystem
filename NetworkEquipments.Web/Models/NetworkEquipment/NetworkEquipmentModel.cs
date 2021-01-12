using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetworkEquipments.Web.Models.NetworkEquipment
{
    public class NetworkEquipmentModel
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public int NetworkId { get; set; }

        [ScaffoldColumn(false)]
        public int AddressId { get; set; }

        [ScaffoldColumn(false)]
        public int TownId { get; set; }

        [ScaffoldColumn(false)]
        public int StreetId { get; set; }

        [Display(Name = "Населенный пункт")]
        public string TownName { get; set; }

        [Display(Name = "Улица")]
        public string StreetName { get; set; }

        [Display(Name = "№ дома")]
        [StringLength(150, ErrorMessage = "Размер данных в поле {0} не должен превышать {1} символов")]
        public string ComplexHouse { get; set; }

        [Display(Name = "№ подъезда")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Значение поля {0} должно находиться в диапазоне от {1} до {2}")]
        public int? Entrance { get; set; }

        [ScaffoldColumn(false)]
        public int EquipmentTypeId { get; set; }

        [Display(Name = "Тип оборудования")]
        public string EquipmentTypeName { get; set; }

        [Display(Name = "IP адрес")]
        [Remote("ValidateIpAddress", "NetworkEquipment", AdditionalFields = "Id")]
        [RegularExpression(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$", ErrorMessage = "Формат данных должен соответствовать шаблону XXX.XXX.XXX.XXX")]
        public string Ip { get; set; }

        [Display(Name = "Примечание")]
        [StringLength(255, ErrorMessage = "Размер данных в поле {0} не должен превышать {1} символов")]
        public string Commentary { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }
    }
}