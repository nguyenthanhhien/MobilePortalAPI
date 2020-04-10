using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobilePortalAPI.ViewModel;
using Business;
using Microsoft.AspNetCore.Http;
using DTO;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace MobilePortalAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DealerApplicationConfigurationController : BaseController
    {
        private readonly IMapper _mapper;
        private IDealerApplicationConfigurationService _dealerAppConfigService;
        private IConfiguration _configuration;
        public DealerApplicationConfigurationController(IDealerApplicationConfigurationService dealerAppConfigService, IHttpContextAccessor httpContextAccessor, IMapper mapper, IConfiguration configuration) : base((BaseService)dealerAppConfigService, httpContextAccessor)
        {
            _dealerAppConfigService = dealerAppConfigService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll([FromQuery]SearchEngineViewModel query)
        {
            try
            {
                return new OkObjectResult(_dealerAppConfigService.GetAll(query.page.Value, query.itemsPerPage.Value, query.sort, query.search));

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Can not get dealer application configuration");
            }
        }

        [HttpGet("getByKey")]
        public IActionResult GetByKey(int key)
        {
            try
            {

                return new OkObjectResult(_dealerAppConfigService.GetByKey(key));

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Can not get dealer application configuration");
            }
        }

        [HttpPost("add")]
        public IActionResult Add(DealerApplicationConfigurationViewModel data)
        {
            try
            {
                var dataDto = _mapper.Map<DealerApplicationConfigurationDTO>(data);
                var result = _dealerAppConfigService.Add(dataDto);
                if (!string.IsNullOrEmpty(result))
                    return new BadRequestObjectResult(result);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Can not get dealer application configuration");
            }
        }

        [HttpPut("update/{key:int}")]
        public IActionResult Update(int key, DealerApplicationConfigurationViewModel data)
        {
            try
            {
                var defaultLocations = _configuration.GetSection("ApplicationName").Get<List<string>>();
                if (key != data.DealerApplicationConfigurationKey)
                    return new BadRequestObjectResult("The key does not match");
                var dataDto = _mapper.Map<DealerApplicationConfigurationDTO>(data);
                var result = _dealerAppConfigService.Update(dataDto);
                if (!string.IsNullOrEmpty(result))
                    return new BadRequestObjectResult(result);
                return new OkResult();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Can not get dealer application configuration");
            }
        }

        [HttpGet("getApplicationNames")]
        public IActionResult GetApplicationNames()
        {
            try
            {
                return new OkObjectResult(_configuration.GetSection("ApplicationName").Get<List<ApplicationNameViewModel>>());

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("Can not get dealer application configuration");
            }
        }

    }
}
