namespace WORKFLOW.Model.Testing
{
    public class MyDb
    {
        public List<ms_workflow> listmsworkflow = new List<ms_workflow>()
        {
            new ms_workflow()
            {
                workflowCode = "PR",
                workflowname = "Purchase Request",
                isactive = true,
                createdate = DateTime.UtcNow,
                md_workflows = new List<md_workflow>()
                {

                },
                ms_rules = new List<ms_rule>()
                {
                    new ms_rule()
                    {
                        workflowcode = "PR",
                        rulecode = "WF001",
                        rulename = "Approve Div IT",
                        startdate = DateTime.UtcNow,
                        enddate = DateTime.UtcNow.AddYears(1),
                        isactive = true,
                        createdate = DateTime.UtcNow,
                        md_rule_vars = new List<md_rule_var>()
                        {

                        },
                        md_rule_exps = new List<md_rule_exp>()
                        {
                            new md_rule_exp()
                            {
                                workflowcode = "PR",
                                rulecode = "WF001",
                                linenum = 1,
                                groupline = 0,
                                linkexp = "",
                                paramcode = "cekDept",
                                paramname = "Cek Department",
                                paramsexpression = "paramsWorkflow.userAccess.dept == \"IT\""
                            }
                        },
                        md_rule_rslts = new List<md_rule_rslt>()
                        {
                            new md_rule_rslt()
                            {
                                workflowcode = "PR",
                                rulecode = "WF001",
                                linenum = 1,
                                linegroup = 1,
                                groupworkflowcode = "PR_Review_IT",
                                actworkflow = "Review",
                                descworkflow = "Review PR IT"
                            },
                            new md_rule_rslt()
                            {
                                workflowcode = "PR",
                                rulecode = "WF001",
                                linenum = 2,
                                linegroup = 2,
                                groupworkflowcode = "PR_App_IT",
                                actworkflow = "Approve",
                                descworkflow = "Approve PR IT"
                            }
                        }
                    },
                    new ms_rule()
                    {
                        workflowcode = "PR",
                        rulecode = "WF002",
                        rulename = "Approve Div FIN",
                        startdate = DateTime.UtcNow,
                        enddate = DateTime.UtcNow.AddYears(1),
                        isactive = true,
                        createdate = DateTime.UtcNow,
                        md_rule_vars = new List<md_rule_var>()
                        {

                        },
                        md_rule_exps = new List<md_rule_exp>()
                        {
                            new md_rule_exp()
                            {
                                workflowcode = "PR",
                                rulecode = "WF002",
                                linenum = 1,
                                groupline = 0,
                                linkexp = "",
                                paramcode = "cekDept",
                                paramname = "Cek Department",
                                paramsexpression = "paramsWorkflow.userAccess.dept == \"FIN\""
                            }
                        },
                        md_rule_rslts = new List<md_rule_rslt>()
                        {
                            new md_rule_rslt()
                            {
                                workflowcode = "PR",
                                rulecode = "WF002",
                                linenum = 1,
                                linegroup = 1,
                                groupworkflowcode = "PR_Review_FIN",
                                actworkflow = "Review",
                                descworkflow = "Review PR FIN"
                            },
                            new md_rule_rslt()
                            {
                                workflowcode = "PR",
                                rulecode = "WF002",
                                linenum = 2,
                                linegroup = 2,
                                groupworkflowcode = "PR_App_FIN",
                                actworkflow = "Approve",
                                descworkflow = "Approve PR FIN IT"
                            }
                        }
                    }
                }
            }
        };

        public List<ms_groupworkflow> listmsgroupworkflow = new List<ms_groupworkflow>()
        {
            new ms_groupworkflow()
            {
                groupworkflowcode = "PR_Review_IT",
                groupworkflowname = "PR Review IT",
                isactive = true,
                createdate = DateTime.UtcNow,
                md_groupworkflows = new List<md_groupworkflow>
                {
                    new md_groupworkflow()
                    {
                        groupworkflowcode = "PR_Review_IT",
                        username = "userIT01"
                    },
                    new md_groupworkflow()
                    {
                        groupworkflowcode = "PR_Review_IT",
                        username = "userIT02"
                    }
                }
            },
            new ms_groupworkflow()
            {
                groupworkflowcode = "PR_App_IT",
                groupworkflowname = "PR Approval IT",
                isactive = true,
                createdate = DateTime.UtcNow,
                md_groupworkflows = new List<md_groupworkflow>
                {
                    new md_groupworkflow()
                    {
                        groupworkflowcode = "PR_App_IT",
                        username = "userIT03"
                    },
                    new md_groupworkflow()
                    {
                        groupworkflowcode = "PR_App_IT",
                        username = "userIT04"
                    }
                }
            },
            new ms_groupworkflow()
            {
                groupworkflowcode = "PR_Review_FIN",
                groupworkflowname = "PR Review FIN",
                isactive = true,
                createdate = DateTime.UtcNow,
                md_groupworkflows = new List<md_groupworkflow>
                {
                    new md_groupworkflow()
                    {
                        groupworkflowcode = "PR_Review_FIN",
                        username = "userFIN01"
                    },
                    new md_groupworkflow()
                    {
                        groupworkflowcode = "PR_Review_FIN",
                        username = "userFIN02"
                    }
                }
            },
            new ms_groupworkflow()
            {
                groupworkflowcode = "PR_App_FIN",
                groupworkflowname = "PR Approval FIN",
                isactive = true,
                createdate = DateTime.UtcNow,
                md_groupworkflows = new List<md_groupworkflow>
                {
                    new md_groupworkflow()
                    {
                        groupworkflowcode = "PR_App_FIN",
                        username = "userFIN03"
                    },
                    new md_groupworkflow()
                    {
                        groupworkflowcode = "PR_App_FIN",
                        username = "userFIN04"
                    }
                }
            }
        };

        public List<ms_user> listmsuser = new List<ms_user>()
        {
            new ms_user()
            {
                username = "userIT01",
                name = "userIT01",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userIT02",
                name = "userIT02",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userIT03",
                name = "userIT03",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userIT04",
                name = "userIT04",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userIT05",
                name = "userIT05",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userFIN01",
                name = "userFIN01",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userFIN02",
                name = "userFIN02",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userFIN03",
                name = "userFIN03",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userFIN04",
                name = "userFIN04",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            },
            new ms_user()
            {
                username = "userFIN05",
                name = "userFIN05",
                email = "fauzipratama11@gmail.com",
                isactive = true,
                createdate = DateTime.UtcNow
            }
        };
    }
}
