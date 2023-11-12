using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NewProjectAPI.Data;
using System.Collections.Specialized;
using System.Data;

namespace NewProjectAPI.Controllers
{
    //[Route("api/[controller]")]

    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Route("api/[controller]")]
    public class FormsController : ControllerBase
    {
        private readonly DAL _dAL;
        private readonly IConfiguration _config;



        public FormsController(DAL dAL, IConfiguration configuration) 
        {
            _dAL = dAL;
            _config = configuration;
        }

        
        [HttpGet]
        public IActionResult GetBranchZone()
        {
            DataTable dt = new DataTable();
            try
            {
                NameValueCollection? nv = new NameValueCollection();

                dt = _dAL.GetData("sp_select_branchzone", nv, _config["ConnectionStrings:Addcon"]);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(dt);

        }
        [HttpGet]

        public IActionResult GetBranchZoneID(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                NameValueCollection? nv = new NameValueCollection();
                nv.Clear();
                nv.Add("ID-INT", id);

                dt = _dAL.GetData("sp_get_branchzone", nv, _config["ConnectionStrings:Addcon"]);
                nv = null;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }   

            return Ok(dt);

        }

        [HttpPost]
        public IActionResult SaveBranchZone([FromBody] BranchZone BranchZone_)
        {

            bool Result = false;
            NameValueCollection? nv = new NameValueCollection();
            try
            {
                nv.Add("ZoneName-VARCHAR", BranchZone_.zone_name);

                Result = _dAL.InserData("sp_insert_branchzone", nv, _config["ConnectionStrings:Addcon"]);
                nv = null;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(Result);

        }

        [HttpPost]
        public IActionResult UpdateBranchZone([FromBody] BranchZone BranchZone_)
        {

            bool Result = false;
           
            try
            {
                NameValueCollection? nv = new NameValueCollection();
                nv.Add("ID-INT", BranchZone_.id);
                nv.Add("ZoneName-VARCHAR", BranchZone_.zone_name);

                Result = _dAL.InserData("sp_update_branchzone", nv, _config["ConnectionStrings:Addcon"]);
                nv = null;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(Result);

        }

        [HttpPost]
        public IActionResult DeleteCities([FromBody] BrnchDelete BrnchDelete_)
        {
            bool Result = false;
            NameValueCollection? nv = new NameValueCollection();
            try
            {
                nv.Add("ID-INT", BrnchDelete_.id);
                Result = _dAL.InserData("sp_delete_branchzone", nv, _config["ConnectionStrings:Addcon"]);
                nv = null;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(Result);
        }
    }



    public class BranchZone
    {
        public string? id { get; set; }
        public string? zone_name { get; set; }
        
    }

    public class BrnchDelete
    {
        public string? id { get; set; }
    }
    
}

