using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WORKFLOW.Migrations
{
    public partial class add_view_selectedworkflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("SELECT twf.documentnumber,\r\n    twf.linegroup,\r\n    twf.workflowcode,\r\n    twf.rulecode,\r\n    twf.groupworkflowcode,\r\n\tCASE \r\n\t\tWHEN twf.groupworkflowcode = 'SELF' THEN 'SELF'::character varying(200)\r\n\t\tWHEN twf.groupworkflowcode = 'FINISH' THEN 'FINISH'::character varying(200)\r\n\t\tELSE msgwf.groupworkflowname::character varying(200)\r\n\tEND AS groupworkflowname,\r\n    twf.actworkflow,\r\n    twf.descworkflow,\r\n    CASE \r\n\t\tWHEN twf.groupworkflowcode = 'SELF' OR twf.groupworkflowcode = 'FINISH' THEN \r\n\t\t\t(SELECT closedby FROM tr_workflow AS tWfSELF WHERE tWfSELF.documentnumber = twf.documentnumber AND tWfSELF.rulecode = 'SELF')::character varying(100)\r\n\t\tELSE mdgwf.username \r\n\tEND AS username,\r\n    twf.closedby,\r\n    twf.closeddate\r\n   FROM tr_workflow twf\r\n     LEFT JOIN ms_groupworkflow msgwf ON msgwf.groupworkflowcode::text = twf.groupworkflowcode::text AND msgwf.isactive = true\r\n     LEFT JOIN md_groupworkflow mdgwf ON mdgwf.groupworkflowcode::text = msgwf.groupworkflowcode::text\r\n  ORDER BY twf.documentnumber, twf.linegroup;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
