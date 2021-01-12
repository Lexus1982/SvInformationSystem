using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Web.Models.Network
{
    public class NetworkModel
    {
        public int Id { get; set; }

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

        [Display(Name = "№ сегмента сети")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Значение поля {0} должно находиться в диапазоне от {1} до {2}")]
        public int? SegmentNumber { get; set; }

        [Display(Name = "VLAN управления")]
        public string VlanManage { get; set; }

        [Display(Name = "VLAN Интернет")]
        public string VlanInternet { get; set; }

        [Display(Name = "Диапазон IP адресов")]
        [Remote("ValidateIpInterval", "Network", AdditionalFields = "Id")]
        [RegularExpression(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$",ErrorMessage = "Формат данных должен соответствовать шаблону XXX.XXX.XXX.XXX")]
        //[RegularExpression(@"^((\\d{1,2}|1\\d{2}|2[0-4]\\d|25[0-5])\.){3}(\\d{1,2}|1\\d{2}|2[0-4]\\d|25[0-5])$", ErrorMessage = "Формат данных должен соответствовать шаблону XXX.XXX.XXX.XXX")]
        public string IpInterval { get; set; }

        [Display(Name = "Примечание")]
        [StringLength(255, ErrorMessage = "Размер данных в поле {0} не должен превышать {1} символов")]
        public string Commentary { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Display(Name = "Всего оборудования")]
        public int EquipmentsCount { get; set; }
    }
}