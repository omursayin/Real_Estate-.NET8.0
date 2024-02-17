using Dapper;
using RealEstate_Dapper_Api.Dtos.EmployeDtos;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.EmployeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;
        public EmployeeRepository(Context context)
        {
            _context = context;
        }
        public async void CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            string query = "insert into Employee (Name, Title, Mail, PhoneNumber, ImageUrl, Status) values (@name, @title, @mail, @phoneNumber, @imageUrl, @status)";
            var paramaters = new DynamicParameters();
            paramaters.Add("@name", createEmployeeDto.Name);
            paramaters.Add("@title", createEmployeeDto.Title);
            paramaters.Add("@mail", createEmployeeDto.Mail);
            paramaters.Add("@phoneNumber", createEmployeeDto.PhoneNumber);
            paramaters.Add("@imageUrl", createEmployeeDto.ImageUrl);
            paramaters.Add("@status", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }

        public async void DeleteEmployee(int id)
        {
            string query = "Delete From Employee Where EmployeeID=@employeeID";
            var paramaters = new DynamicParameters();
            paramaters.Add("@employeeID", id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }

        public async Task<List<ResultEmployeeDto>> GetAllEmployeeAsync()
        {
            string query = "Select * From Employee";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultEmployeeDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDEmployeeDto> GetEmployee(int id)
        {
            string query = "Select * From Employee Where EmployeeID=@employeeID";
            var paramaters = new DynamicParameters();
            paramaters.Add("@EmployeeID", id);
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryFirstOrDefaultAsync<GetByIDEmployeeDto>(query, paramaters);
                return values;
            }
        }

        public async void UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            string query = "Update Employee Set Name=@name,Title=@title,Mail=@mail,PhoneNumber=@phoneNumber,ImageUrl=@imageUrl,Status=@status Where EmployeeID=@employeeID";
            var paramaters = new DynamicParameters();
            paramaters.Add("@name", updateEmployeeDto.Name);
            paramaters.Add("@title", updateEmployeeDto.Title);
            paramaters.Add("@mail", updateEmployeeDto.Mail);
            paramaters.Add("@phoneNumber", updateEmployeeDto.PhoneNumber);
            paramaters.Add("@imageUrl", updateEmployeeDto.ImageUrl);
            paramaters.Add("@status", true);
            paramaters.Add("@employeeID", updateEmployeeDto.EmployeeID);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, paramaters);
            }
        }
    }
}
