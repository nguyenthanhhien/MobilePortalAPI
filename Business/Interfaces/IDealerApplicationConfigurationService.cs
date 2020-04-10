using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace Business
{
    public interface IDealerApplicationConfigurationService
    {
        BaseObjectManagementDTO<DealerApplicationConfigurationDTO> GetAll(int page, int itemPerPage, string sort, string search);
        string Add(DealerApplicationConfigurationDTO data);
        string Update(DealerApplicationConfigurationDTO data);
        DealerApplicationConfigurationDTO GetByKey(int key);
    }
}
