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
using System.Net.Http;

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
                var dataDto = _mapper.Map<SearchEngineDTO>(query);
                return new OkObjectResult(_dealerAppConfigService.GetAll(dataDto));
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
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
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(DealerApplicationConfigurationViewModel data)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(data.DealerId) || string.IsNullOrWhiteSpace(data.Application))
                {
                    return new BadRequestObjectResult("Invalid data");
                }
                var dataDto = _mapper.Map<DealerApplicationConfigurationDTO>(data);
                var result = _dealerAppConfigService.Add(dataDto);
                if (!string.IsNullOrEmpty(result))
                    return new BadRequestObjectResult(result);
                return new OkResult();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) && ex.InnerException.Message.Contains("permission was denied"))
                {
                    return new BadRequestObjectResult("You don't have permission to do this");
                }
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPut("update/{key:int}")]
        public IActionResult Update(int key, DealerApplicationConfigurationViewModel data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(data.DealerId) || string.IsNullOrWhiteSpace(data.Application))
                {
                    return new BadRequestObjectResult("Invalid data");
                }

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
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message) && ex.InnerException.Message.Contains("permission was denied"))
                {
                    return new BadRequestObjectResult("You don't have permission to do this");
                }
                return new BadRequestObjectResult(ex.Message);
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
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("getDealers")]
        public IActionResult GetDealers()
        {
            try
            {
                return new OkObjectResult(_dealerAppConfigService.GetDealers());
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

    }
}
