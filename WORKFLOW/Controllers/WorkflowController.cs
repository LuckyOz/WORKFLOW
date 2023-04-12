using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WORKFLOW.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowServices _workflowServices;

        public WorkflowController(IWorkflowServices workflowServices)
        {
            _workflowServices = workflowServices;
        }

        [HttpPost("getWorkflow")]
        public async Task<ActionResult<Response<List<RuleResultTree>>>> getWorkflow(DocumentRequestDto data)
        {
            Response<List<RuleResultTree>> response = new Response<List<RuleResultTree>>(); 

            try {
                response = await _workflowServices.FindWorkflow(data);
            } catch (Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("insertDataDefault")]
        public async Task<ActionResult<bool>> insertDataDefault()
        {
            try {
                return await _workflowServices.insertDefault();
            } catch {
                return false;
            }
        }
    }
}
