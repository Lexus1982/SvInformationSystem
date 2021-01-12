using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEquipments.Services.DTO;

namespace NetworkEquipments.Services.Interfaces
{
    public interface ISectionService : IDisposable
    {
        SectionDto GetById(int? sectionId);

        IEnumerable<SectionDto> GetUserSections(string login, int groupId);
    }
}
