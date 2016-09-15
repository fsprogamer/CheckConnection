using System;
using System.Collections.Generic;
using ConnectionWizard.Model;
using SQLite;

namespace ConnectionWizard.Methods
{
    public interface DBInterface
    {
        Forms GetFormsById(int idform);

        Form_Query GetQuery(int idquery);

        List<Form_Query> GetQueryTable();

        List<Form_Ans> GetQueryAnswer(int idquery);

        int GetQueryByAnswerId(int answerId);

        Form_Ans GetQueryAnswerByText(string answer);

        int GetNextQueryId(int idvisit, int idquery);

        Form_Query GetNextQuery(int idvisit, int idquery);

        List<Form_Ans_Abo> GetNextFormAnsAbo(int idvisit, int idquery);

        int SetFormAnsAbo(Form_Ans_Abo form_ans_abo);

        int Form_Next_Query(int idvisit, int idquery);

        int SetFormVisit(int pId_Form);

        bool isTableExists(String tableName, SQLiteConnection db);
    }
}
