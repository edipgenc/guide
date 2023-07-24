using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Guide.Book.Application.Dto.CommonDto;
using Guide.Report.Data.Repositories.Interfaces;
using Guide.Report.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Guide.Report.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly ILogger<ReportController> _logger;
        public ReportController(IReportRepository reportRepository, EventBusRabbitMQProducer eventBus, ILogger<ReportController> logger)
        {
            _reportRepository  = reportRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        /// <summary>
        /// This methot returns Report list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRepostList")]
        public async Task<IActionResult> GetRepostList()
        {
            try
            {
                var data = await _reportRepository.GetReports();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }

        /// <summary>
        /// This methot returns only one Report Info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetReportById")]
        public async Task<IActionResult> GetReportById(string Id)
        {
            try
            {
                var data = await _reportRepository.GetReportById(Id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }
        /// <summary>
        /// This methot returns Report's Detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetReportDetail")]
        public async Task<IActionResult> GetReportDetail(string Id)
        {
            try
            {
                var data = await _reportRepository.GetReportDetail(Id);
                return Ok(data);               
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }


        /// <summary>
        /// This methot add a report request to queue
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPersonReportByLocation")]
        public async Task<IActionResult> GetPersonReportByLocation()
        {
            try
            {
                var data = await _reportRepository.Create(new ReportEntity() {ReportStatus=Domain.Comon.TReportStatus.Preparing,ReportRequestDate=DateTime.Now});

                // Queue ya bir rapor isteği gönderiyoruz
 
                ReportCreateEvent eventMessage = new ReportCreateEvent()
                {
                    Id = data.Id,
                    ReportStatus=EventBusRabbitMQ.Comon.TReportStatus.Preparing,
                    ReportType = "GetPersonReportByLocation", 
                    ReportRequestDate = data.ReportRequestDate
                };
                try
                {
                    // Event Bus Queue Message publish ediyoruz, Controller aynı zamanda bir publisher olarak çalışıyor
                    _eventBus.Publish(EventBusConstants.ReportCreateQueue, eventMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id, "Sourcing");
                    throw;
                }
                return Accepted(eventMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel() { Message = "Guide Book has been error please try again after a while", Detail = ex.Message });
            }
        }

    }
}
