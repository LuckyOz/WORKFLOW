using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WORKFLOW.Migrations
{
    public partial class add_view : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE OR REPLACE VIEW public.v_selectedworkflow\r\nAS\r\nSELECT twf.documentnumber,\r\n    twf.linegroup,\r\n    twf.workflowcode,\r\n    twf.rulecode,\r\n    twf.groupworkflowcode,\r\n        CASE\r\n            WHEN twf.groupworkflowcode::text = 'SELF'::text THEN 'SELF'::character varying(200)\r\n            WHEN twf.groupworkflowcode::text = 'FINISH'::text THEN 'FINISH'::character varying(200)\r\n            ELSE msgwf.groupworkflowname\r\n        END AS groupworkflowname,\r\n    twf.actworkflow,\r\n    twf.descworkflow,\r\n        CASE\r\n            WHEN twf.groupworkflowcode::text = 'SELF'::text OR twf.groupworkflowcode::text = 'FINISH'::text THEN ( SELECT twfself.closedby AS closedby\r\n               FROM tr_workflow twfself\r\n              WHERE twfself.documentnumber::text = twf.documentnumber::text AND twfself.rulecode::text = 'SELF'::text)\r\n            ELSE mdgwf.username\r\n        END AS username,\r\n    twf.closedby AS closedby,\r\n    twf.closeddate AS closeddate,\r\n\ttwf.rejectedby AS rejectedby,\r\n    twf.rejecteddate AS rejecteddate\r\n   FROM tr_workflow twf\r\n     LEFT JOIN ms_groupworkflow msgwf ON msgwf.groupworkflowcode::text = twf.groupworkflowcode::text AND msgwf.isactive = true\r\n     LEFT JOIN md_groupworkflow mdgwf ON mdgwf.groupworkflowcode::text = msgwf.groupworkflowcode::text\r\n  ORDER BY twf.documentnumber, twf.linegroup;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
