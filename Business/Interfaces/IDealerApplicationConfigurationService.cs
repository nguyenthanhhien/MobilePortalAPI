using System;
using System.Collections.Generic;
using System.Text;
using DTO;

namespace Business
{
    public interface IDealerApplicationConfigurationService
    {
        BaseObjectManagementDTO<DealerApplicationConfigurationDTO> GetAll(SearchEngineDTO filter);
        string Add(DealerApplicationConfigurationDTO data);
        string Update(DealerApplicationConfigurationDTO data);
        DealerApplicationConfigurationDTO GetByKey(int key);
        List<string> GetDealers();
    }
}
