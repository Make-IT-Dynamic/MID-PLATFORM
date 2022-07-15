using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MID_PLATFORM.Models;
using System.Net.Http.Headers;

namespace MID_PLATFORM_CLIENT
{
    public partial class GetListWindow : Form
    {
        public GetListWindow(string option = null)
        {
            InitializeComponent();
            this.Text = option == null ? "Error" : option;
            if (option.Contains("_"))
                option = option.Replace("_", String.Empty);
        }

        private void createDGV(object type)
        {
            foreach (var property in type.GetType().GetProperties().Where(a => !a.GetGetMethod().GetParameters().Any()))
            {
                GetListDGV.Columns.Add(property.Name, property.Name);
                if (property.Name.ToLower() == "user" || property.Name.ToLower() == "user1")
                {
                    return;
                }
            }
            return;
        }


        private async void GetListWindow_Load(object sender, EventArgs e)
        {
            try
            {
                string table = this.Text;

                if (table.Contains(" "))
                    table = table.Replace(" ", String.Empty);

                var jsonData = await READ(table);

                switch (table)
                {
                    case "Users":
                        List<User> users = new List<User>();
                        users = JsonConvert.DeserializeObject<List<User>>(jsonData.ToString());

                        createDGV(new User());

                        foreach (var obj in users)
                        {
                            //long timestamp = BitConverter.ToInt64(obj.Timestamp, 0);
                            //byte[] byteValue = BitConverter.GetBytes(DateTime.Now.Ticks);


                            //DateTime t = new DateTime(1980, 1, 1).AddMilliseconds(timestamp);
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();
                            
                            row.Cells[0].Value = obj.Username;
                            row.Cells[1].Value = obj.Password;
                            row.Cells[2].Value = obj.Name;
                            row.Cells[3].Value = obj.Email;
                            row.Cells[4].Value = obj.Active;
                            row.Cells[5].Value = "";
                            row.Cells[6].Value = obj.User1;

                            GetListDGV.Rows.Add(row);
                        }


                        break;

                    case "Countries":
                        List<Country> country = new List<Country>();
                        country = JsonConvert.DeserializeObject<List<Country>>(jsonData.ToString());

                        createDGV(new Country());

                        foreach (var obj in country)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.CountryId;
                            row.Cells[1].Value = obj.CountryCode;
                            row.Cells[2].Value = obj.Name;
                            row.Cells[3].Value = obj.Active;
                            row.Cells[4].Value = obj.Timestamp;
                            row.Cells[5].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "Companies":
                        List<Company> company = new List<Company>();
                        company = JsonConvert.DeserializeObject<List<Company>>(jsonData.ToString());

                        createDGV(new Company());

                        foreach (var obj in company)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.CompanyId;
                            row.Cells[1].Value = obj.Code;
                            row.Cells[2].Value = obj.Name;
                            row.Cells[3].Value = obj.Country;
                            row.Cells[4].Value = obj.Active;
                            row.Cells[5].Value = obj.Timestamp;
                            row.Cells[6].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMContractTypes":
                        List<SmContractType> SmcontractType = new List<SmContractType>();
                        SmcontractType = JsonConvert.DeserializeObject<List<SmContractType>>(jsonData.ToString());

                        createDGV(new SmContractType());

                        foreach (var obj in SmcontractType)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.ContractTypeId;
                            row.Cells[1].Value = obj.Description;
                            row.Cells[2].Value = obj.AllowExeedHours;
                            row.Cells[3].Value = obj.BillableExceedHours;
                            row.Cells[4].Value = obj.Active;
                            row.Cells[5].Value = obj.Timestamp;
                            row.Cells[6].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "People":
                        List<Person> person = new List<Person>();
                        person = JsonConvert.DeserializeObject<List<Person>>(jsonData.ToString());

                        createDGV(new Person());

                        foreach (var obj in person)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.PersonId;
                            row.Cells[1].Value = obj.Company;
                            row.Cells[2].Value = obj.Name;
                            row.Cells[3].Value = obj.Email;
                            row.Cells[4].Value = obj.Active;
                            row.Cells[5].Value = obj.Timestamp;
                            row.Cells[6].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMTaskTypes":
                        List<SmTaskType> SmtaskType = new List<SmTaskType>();
                        SmtaskType = JsonConvert.DeserializeObject<List<SmTaskType>>(jsonData.ToString());

                        createDGV(new SmTaskType());

                        foreach (var obj in SmtaskType)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.TaskTypeId;
                            row.Cells[1].Value = obj.Description;
                            row.Cells[2].Value = obj.Active;
                            row.Cells[3].Value = obj.Timestamp;
                            row.Cells[4].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMAgents":
                        List<SmAgent> Smagent = new List<SmAgent>();
                        Smagent = JsonConvert.DeserializeObject<List<SmAgent>>(jsonData.ToString());

                        createDGV(new SmAgent());

                        foreach (var obj in Smagent)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.AgentId;
                            row.Cells[1].Value = obj.Code;
                            row.Cells[2].Value = obj.Username;
                            row.Cells[3].Value = obj.Name;
                            row.Cells[4].Value = obj.Email;
                            row.Cells[5].Value = obj.HourCost;
                            row.Cells[6].Value = obj.Active;
                            row.Cells[7].Value = obj.Timestamp;
                            row.Cells[8].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMPriorities":
                        List<SmPriority> Smpriority = new List<SmPriority>();
                        Smpriority = JsonConvert.DeserializeObject<List<SmPriority>>(jsonData.ToString());

                        createDGV(new SmPriority());

                        foreach (var obj in Smpriority)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.PriorityId;
                            row.Cells[1].Value = obj.Description;
                            row.Cells[2].Value = obj.Active;
                            row.Cells[3].Value = obj.Timestamp;
                            row.Cells[4].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMContractStatus":
                        List<SmContractStatus> SmcontractStatus = new List<SmContractStatus>();
                        SmcontractStatus = JsonConvert.DeserializeObject<List<SmContractStatus>>(jsonData.ToString());

                        createDGV(new SmContractStatus());

                        foreach (var obj in SmcontractStatus)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.StatusId;
                            row.Cells[1].Value = obj.Description;
                            row.Cells[2].Value = obj.Closed;
                            row.Cells[2].Value = obj.Active;
                            row.Cells[3].Value = obj.Timestamp;
                            row.Cells[4].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMTaskStatus":
                        List<SmTaskStatus> SmtaskStatus = new List<SmTaskStatus>();
                        SmtaskStatus = JsonConvert.DeserializeObject<List<SmTaskStatus>>(jsonData.ToString());

                        createDGV(new SmTaskStatus());

                        foreach (var obj in SmtaskStatus)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.StatusId;
                            row.Cells[1].Value = obj.Description;
                            row.Cells[2].Value = obj.Closed;
                            row.Cells[2].Value = obj.Active;
                            row.Cells[3].Value = obj.Timestamp;
                            row.Cells[4].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMWorkRecordTypes":
                        List<SmWorkRecordType> SmworkRecordType = new List<SmWorkRecordType>();
                        SmworkRecordType = JsonConvert.DeserializeObject<List<SmWorkRecordType>>(jsonData.ToString());

                        createDGV(new SmWorkRecordType());

                        foreach (var obj in SmworkRecordType)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.WorkRecordTypeId;
                            row.Cells[1].Value = obj.Description;
                            row.Cells[2].Value = obj.Billable;
                            row.Cells[3].Value = obj.Active;
                            row.Cells[4].Value = obj.Timestamp;
                            row.Cells[5].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "Periods":
                        List<Period> period = new List<Period>();
                        period = JsonConvert.DeserializeObject<List<Period>>(jsonData.ToString());

                        createDGV(new Period());

                        foreach (var obj in period)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.PeriodId;
                            row.Cells[1].Value = obj.Code;
                            row.Cells[2].Value = obj.StartDate;
                            row.Cells[3].Value = obj.EndDate;
                            row.Cells[4].Value = obj.ActiveForSm;
                            row.Cells[5].Value = obj.Active;
                            row.Cells[6].Value = obj.Timestamp;
                            row.Cells[7].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "Categories":
                        List<Category> category = new List<Category>();
                        category = JsonConvert.DeserializeObject<List<Category>>(jsonData.ToString());

                        createDGV(new Category());

                        foreach (var obj in category)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.CategoryId;
                            row.Cells[1].Value = obj.Parent;
                            row.Cells[2].Value = obj.Code;
                            row.Cells[3].Value = obj.LongCode;
                            row.Cells[4].Value = obj.Description;
                            row.Cells[5].Value = obj.LongDescription;
                            row.Cells[6].Value = obj.Title;
                            row.Cells[7].Value = obj.Active;
                            row.Cells[8].Value = obj.Timestamp;
                            row.Cells[9].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMContracts":
                        List<SmContract> Smcontract = new List<SmContract>();
                        Smcontract = JsonConvert.DeserializeObject<List<SmContract>>(jsonData.ToString());

                        createDGV(new SmContract());

                        foreach (var obj in Smcontract)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.ContractId;
                            row.Cells[1].Value = obj.Code;
                            row.Cells[2].Value = obj.Instance;
                            row.Cells[3].Value = obj.Type;
                            row.Cells[4].Value = obj.Company;
                            row.Cells[5].Value = obj.ContactPerson;
                            row.Cells[6].Value = obj.Date;
                            row.Cells[7].Value = obj.Name;
                            row.Cells[8].Value = obj.Category;
                            row.Cells[9].Value = obj.Description;
                            row.Cells[10].Value = obj.AllowExceededHours;
                            row.Cells[11].Value = obj.BillableExceededHours;
                            row.Cells[12].Value = obj.Status;
                            row.Cells[13].Value = obj.Active;
                            row.Cells[14].Value = obj.Timestamp;
                            row.Cells[15].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMContractLimits":
                        List<SmContractLimit> SmcontractLimit = new List<SmContractLimit>();
                        SmcontractLimit = JsonConvert.DeserializeObject<List<SmContractLimit>>(jsonData.ToString());

                        createDGV(new SmContractLimit());

                        foreach (var obj in SmcontractLimit)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.ContractLimitsId;
                            row.Cells[1].Value = obj.Contract;
                            row.Cells[2].Value = obj.Date;
                            row.Cells[3].Value = obj.Quantity;
                            row.Cells[4].Value = obj.Value;
                            row.Cells[5].Value = obj.Document;
                            row.Cells[6].Value = obj.Description;
                            row.Cells[7].Value = obj.Active;
                            row.Cells[8].Value = obj.Timestamp;
                            row.Cells[9].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMTasks":
                        List<SmTask> Smtask = new List<SmTask>();
                        Smtask = JsonConvert.DeserializeObject<List<SmTask>>(jsonData.ToString());

                        createDGV(new SmTask());

                        foreach (var obj in Smtask)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();

                            row.Cells[0].Value = obj.TaskId;
                            row.Cells[1].Value = obj.Contract;
                            row.Cells[2].Value = obj.Type;
                            row.Cells[3].Value = obj.Requester;
                            row.Cells[4].Value = obj.CreatedBy;
                            row.Cells[5].Value = obj.AssignedTo;
                            row.Cells[6].Value = obj.Subject;
                            row.Cells[7].Value = obj.Description;
                            row.Cells[8].Value = obj.Priority;
                            row.Cells[9].Value = obj.Status;
                            row.Cells[10].Value = obj.Category;
                            row.Cells[11].Value = obj.CreationDate;
                            row.Cells[12].Value = obj.ReplyDate;
                            row.Cells[13].Value = obj.ClosedDate;
                            row.Cells[14].Value = obj.TotalHoursEstimated;
                            row.Cells[15].Value = obj.RemainingHoursEstimaded;
                            row.Cells[16].Value = obj.Active;
                            row.Cells[17].Value = obj.Timestamp;
                            row.Cells[18].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;

                    case "SMWorkRecords":
                        List<SmWorkRecord> SmworkRecord = new List<SmWorkRecord>();
                        SmworkRecord = JsonConvert.DeserializeObject<List<SmWorkRecord>>(jsonData.ToString());

                        createDGV(new SmWorkRecord());

                        foreach (var obj in SmworkRecord)
                        {
                            DataGridViewRow row = (DataGridViewRow)GetListDGV.Rows[0].Clone();
                            
                            row.Cells[0].Value = obj.WorkRecordId;
                            row.Cells[1].Value = obj.Task;
                            row.Cells[2].Value = obj.Type;
                            row.Cells[3].Value = obj.Agent;
                            row.Cells[4].Value = obj.StartDate;
                            row.Cells[5].Value = obj.EndDate;
                            row.Cells[6].Value = obj.WorkedHours;
                            row.Cells[7].Value = obj.BillableHours;
                            row.Cells[8].Value = obj.NonBillableHours;
                            row.Cells[9].Value = obj.Description;
                            row.Cells[10].Value = obj.Active;
                            row.Cells[11].Value = obj.Timestamp;
                            row.Cells[12].Value = obj.User;

                            GetListDGV.Rows.Add(row);
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private static async Task<string> READ(string table)
        {
            try
            {
                if (LoginPopup.token == null)
                {
                    LoginPopup.NoLogin();
                    return null;
                }

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginPopup.token);
                HttpResponseMessage response = await client.GetAsync("https://localhost:7042/API/" + table);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                return null;
            }
        }
    }
}
