
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Response<bool>>> getWorkflow(DocumentRequestDto data)
        {
            Response<bool> response = new Response<bool>(); 

            try {
                response = await _workflowServices.SetupDocumentWorkflow(data);
            } catch (Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("closeworkflow")]
        public async Task<ActionResult<Response<bool>>> closeworkflow(CloseWorkflowRequestDto data)
        {
            Response<bool> response = new Response<bool>();

            try {
                response = await _workflowServices.CloseWorkflow(data);
            } catch (Exception ex) {
                response.Success = false;
                response.Message = ex.Message;
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
