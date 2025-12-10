using BLL.Services;
using BotApi.Models.Requests;
using BotApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BotApi.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsController
    {
        private readonly StatisticsService statisticsService;

        public StatisticsController(StatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpPost("getByUser")]
        public async Task<List<GetStatisticsResponse>> GetStatisticsByUser(GetStatisticsByUserRequest request)
        {
            var response = await statisticsService.GetStatisticsByUserAsync(request.SessionId, request.StartDate, request.EndDate);

            var list = new List<GetStatisticsResponse>();

            foreach (var item in response)
            {
                list.Add(new GetStatisticsResponse
                {
                    DetectionCount = item.DetectionCount,
                    DetectionDate = item.DetectionDate,
                });
            }

            return list;
        }

        [HttpPost("getForAdmin")]
        public async Task<List<GetStatisticsResponse>> GetStatisticsForAdmin(GetStatisticsByUserRequest request)
        {
            var response = await statisticsService.GetStatisticsForAdminAsync(request.StartDate, request.EndDate);

            var list = new List<GetStatisticsResponse>();

            foreach (var item in response)
            {
                list.Add(new GetStatisticsResponse
                {
                    DetectionCount = item.DetectionCount,
                    DetectionDate = item.DetectionDate,
                });
            }

            return list;
        }

        [HttpPost("getByUserId")]
        public async Task<List<GetStatisticsResponse>> GetStatisticsByUserId(GetStatisticsByUserRequest request)
        {
            var response = await statisticsService.GetStatisticsByUserId(request.UserId.Value, request.StartDate, request.EndDate);

            var list = new List<GetStatisticsResponse>();

            foreach (var item in response)
            {
                list.Add(new GetStatisticsResponse
                {
                    DetectionCount = item.DetectionCount,
                    DetectionDate = item.DetectionDate,
                });
            }

            return list;
        }
    }
}
