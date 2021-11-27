using Microsoft.AspNetCore.Mvc;
using MyERP.Core.Abstractions;
using MyERP.Core.Models.Resources;
using System.Threading.Tasks;

namespace MyERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseManagementController : ControllerBase
    {
        private readonly IDatabaseManagementService _databaseManagementService;

        public DatabaseManagementController(IDatabaseManagementService databaseManagementService)
        {
            _databaseManagementService = databaseManagementService;
        }

        [HttpGet("{tableName}/GetColumns")]
        public async Task<IActionResult> GetColumnNamesOfCustomerTable(string tableName)
        {
            var result = await _databaseManagementService.GetColumnNamesOfTable(tableName); 
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddColumn([FromBody] AlterTableModel alterTableModel)
        {
            _databaseManagementService.AddFieldToCustomerTable(alterTableModel.ColumnName);
            return Ok();
        }
    }
}
