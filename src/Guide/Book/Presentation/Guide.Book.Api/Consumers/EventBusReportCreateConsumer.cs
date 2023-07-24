using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using Guide.Book.Data.LocalStorage.Repositories.Interfaces;
using Guide.Book.Data.Services.Interfaces;
using Guide.Book.Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Guide.Book.Api.Consumers
{
    public class EventBusReportCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _persistantConnection;
        private readonly IReportService _reportService;
        private readonly IBookReportRepository _bookReportRepository;
        private readonly IMapper _mapper;

        public EventBusReportCreateConsumer(IRabbitMQPersistentConnection persistantConnection, IReportService reportService, IBookReportRepository bookReportRepository, IMapper mapper)
        {
            _persistantConnection = persistantConnection;
            _reportService = reportService;
            _bookReportRepository = bookReportRepository;
            _mapper = mapper;
        }

        public void Consume()
        {
            if (!_persistantConnection.IsConnected)
            {
                _persistantConnection.TryConnect();
            }
            var channel = _persistantConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.ReportCreateQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.ReportCreateQueue, autoAck: true, consumer: consumer);


        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.Span);
            var @event = JsonConvert.DeserializeObject<ReportCreateEvent>(message);

            if (e.RoutingKey == EventBusConstants.ReportCreateQueue)
            {
                ReportEntity _reportRecord = await _bookReportRepository.GetReportById(@event.Id);
                var _reportData = await _reportService.CalculateCountReportByLocation(@event.Location);

                if (_reportData != null)
                {
                    List<ReportDetailEntity> reportDetail = new List<ReportDetailEntity>();
                    reportDetail.Add(new ReportDetailEntity()
                    {
                        LocationInfo = @event.Location,
                        PersonCount = _reportData.PersonCount,
                        PhoneNumberCount = _reportData.PhoneCount

                    });
                    _reportRecord.ReportStatus = Domain.Common.TReportStatus.Ready;
                    _reportRecord.Details = reportDetail;
                    await _bookReportRepository.Update(_reportRecord);
                }
                //@event.
                //_personService
            }
        }
        public void Disconnect()
        {
            _persistantConnection.Dispose();
        }
    }
}
