﻿using Dapper;
using RealEstate_Dapper_Api.Dtos.PopularLocationDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.PopularLocationRepositories
{
    public class PopularLocationRepository : IPopularLocationRepository
    {
        private readonly Context _context;
        public PopularLocationRepository(Context context)
        {
            _context = context;
        }

        public async void CreatePopularLocation(CreatePopularLocationDto createPopularLocationDto)
        {
            string query = "insert into PopularLocation (CityName, ImageUrl) values (@cityName, @imageUrl)";
            var paramaters = new DynamicParameters();
            paramaters.Add("@cityName", createPopularLocationDto.CityName);
            paramaters.Add("@imageUrl", createPopularLocationDto.ImageUrl);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }

        public async void DeletePopularLocation(int id)
        {
            string query = "Delete From PopularLocation Where LocationID=@locationID";
            var paramaters = new DynamicParameters();
            paramaters.Add("@locationID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }

        public async Task<List<ResultPopularLocationDto>> GetAllPopularLocationAsync()
        {
            string query = "Select * From PopularLocation";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultPopularLocationDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDPopularLocationDto> GetPopularLocation(int id)
        {
            string query = "Select * From PopularLocation Where LocationID=@locationID";
            var paramaters = new DynamicParameters();
            paramaters.Add("@locationID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDPopularLocationDto>(query, paramaters);
                return values;
            }
        }

        public async void UpdatePopularLocation(UpdatePopularLocationDto updatePopularLocationDto)
        {
            string query = "Update PopularLocation Set CityName=@cityName,ImageUrl=@imageUrl Where LocationID=@locationID";
            var paramaters = new DynamicParameters();
            paramaters.Add("@cityName", updatePopularLocationDto.CityName);
            paramaters.Add("@imageUrl", updatePopularLocationDto.ImageUrl);
            paramaters.Add("@locationID", updatePopularLocationDto.LocationID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }
    }
}
